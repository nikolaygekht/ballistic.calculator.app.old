using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Gehtsoft.BallisticCalculator.Connectivity;
using MathEx.ExternalBallistic;

namespace Gehtsoft.BallisticCalculator
{
    public partial class WebServerForm : Form
    {
        private const string SERVICE_NAME = "BallisticCalculatorService";

        WebServerState mState = new WebServerState();
        ConnectivityHttpServer mServer;
        ServiceAnnouncer mServiceAnnouncer;

        public WebServerForm()
        {
            InitializeComponent();
            WebServerStateController.Serialization.Load(mState);
            foreach (TraceInfo ti in mState.Traces)
                AddTraceToList(ti, false);
            numericPort.Value = (decimal)mState.Port;
            checkBoxAutoStart.Checked = mState.Autostart;
            if (mState.Autostart)
                buttonStart_Click(this, null);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            mServer = new ConnectivityHttpServer(mState.Traces, (int)numericPort.Value);
            mOnServerEvent = new HttpServerEventDelegate(this.OnServerEvent);
            mServer.OnEvent += mOnServerEvent;
            mServer.Start();
            textBoxServerName.Text = mServer.Name;

            if (mServiceAnnouncer != null)
                mServiceAnnouncer.stop();
            mServiceAnnouncer = new ServiceAnnouncer(SERVICE_NAME, textBoxServerName.Text.Split(':')[0], numericPort.Value.ToString());
            mServiceAnnouncer.start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (mServer != null)
                mServer.Terminate();
            if (mServiceAnnouncer != null)
                mServiceAnnouncer.stop();
            mServer = null;
            mServiceAnnouncer = null;
            textBoxServerName.Text = "";
        }

        HttpServerEventDelegate mOnServerEvent;

        public void OnServerEvent(object sender, HttpServerEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(mOnServerEvent, new object[] { sender, e });
            }
            else
            {
                buttonStart.Enabled = (mServer == null ? true : !mServer.IsRunning);
                buttonStop.Enabled = (mServer == null ? false : mServer.IsRunning);

                if (mServer != null && !mServer.IsRunning)
                    mServer = null;

                ListViewItem lvi = listViewLog.Items.Add(e.Time.ToString());
                lvi.SubItems.Add(e.Severity.ToString());
                lvi.SubItems.Add(e.Message);
                lvi.Focused = true;
                lvi.Selected = true;
                lvi.EnsureVisible();

                if (e.Severity == HttpServerEventType.Error)
                    tabControl1.SelectedIndex = 1;
            }
        }

        private void WebServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                WebServerStateController.Serialization.Save(mState);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Web Server state save failed\r\n" + ex.ToString(), "Web Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (mServer != null)
            {
                mServer.OnEvent -= mOnServerEvent;
                mOnServerEvent = null;
                mServer.Terminate();
                while (mServer.IsRunning)
                    Thread.Sleep(1);
            }
        }

        public void AddTrace(TraceInfo trace)
        {
            int index = mState.Traces.Find(trace.TraceName);
            if (index != -1)
            {
                if (MessageBox.Show("The trace with name " + trace.TraceName + " already exists.\r\nDo you want to overwrite it?", "Connectivity", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    mState.Traces.RemoveAt(index);
                    listViewTraces.Items.RemoveAt(index);
                }
                else
                    return;
            }
            mState.Traces.Add(trace);
            AddTraceToList(trace, true);
        }

        private void AddTraceToList(TraceInfo trace, bool select)
        {
            ListViewItem lvi = listViewTraces.Items.Add(trace.TraceName);
            lvi.SubItems.Add(string.Format("{0:0.000}{1}", trace.BallisticCoefficient, DragTableFactory.DragTableName(trace.DrageTable)));
            lvi.SubItems.Add(trace.BulletWeight.ToString(trace.BulletWeight.SetUnit));
            lvi.SubItems.Add(trace.MuzzleVelocity.ToString(trace.MuzzleVelocity.SetUnit));
            lvi.SubItems.Add(trace.SightHeight.ToString(trace.SightHeight.SetUnit));
            lvi.SubItems.Add(trace.ZeroDistance.ToString(trace.ZeroDistance.SetUnit, true, 0));
            if (select)
            {
                lvi.Focused = true;
                lvi.Selected = true;
                lvi.EnsureVisible();
            }
        }

        private void listViewTraces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTraces.SelectedIndices.Count > 0)
            {
                toolStripButtonOpenTrace.Enabled = true;
                toolStripButtonRemoveTrace.Enabled = true;
            }
            else
            {
                toolStripButtonOpenTrace.Enabled = false;
                toolStripButtonRemoveTrace.Enabled = false;
            }
        }

        private void toolStripButtonRemoveTrace_Click(object sender, EventArgs e)
        {
            if (listViewTraces.SelectedIndices.Count > 0)
            {
                int index = listViewTraces.SelectedIndices[0];
                if (index >= 0 && index < mState.Traces.Count)
                {
                    mState.Traces.RemoveAt(index);
                    listViewTraces.Items.RemoveAt(index);
                }
            }
        }

        private void numericPort_ValueChanged(object sender, EventArgs e)
        {
            mState.Port = (int)numericPort.Value;
        }

        private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            mState.Autostart = checkBoxAutoStart.Checked;
        }

        private void toolStripButtonOpenTrace_Click(object sender, EventArgs e)
        {
            int index = listViewTraces.SelectedIndices[0];
            if (index >= 0 && index < mState.Traces.Count)
                Program.MainForm.OpenTraceInfo(mState.Traces[index]);
        }

        private class ServiceAnnouncer
        {
            private const int UDP_PORT = 15873;
            private const int WAIT_TIME = 2000;

            private string _serviceName;
            private string _serviceAddress;
            private string _servicePort;
            private Thread _thread;
            private UdpClient _udpClient;
            private bool _isStopRequested;

            public ServiceAnnouncer(string serviceName, string serviceAddress, string servicePort)
            {
                _serviceName = serviceName;
                _serviceAddress = serviceAddress;
                _servicePort = servicePort;
            }

            public void start()
            {
                _thread = new Thread(() =>
                {
                    try
                    {
                        _udpClient = new UdpClient();
                        string requestString = _serviceName + ";" + _serviceAddress + ";" + _servicePort;

                        while (!_isStopRequested)
                        {
                            _udpClient.EnableBroadcast = true;
                            var endpoint = new IPEndPoint(IPAddress.Broadcast, UDP_PORT);
                            var message = Encoding.ASCII.GetBytes(requestString);
                            _udpClient.Send(message, message.Length, endpoint);
                            Thread.Sleep(WAIT_TIME);
                        }
                    }
                    catch (ThreadAbortException) { }
                    catch (ThreadInterruptedException) { }
                    finally
                    {
                        try { _udpClient.Close(); } catch (Exception) { }
                    }
                });

                _thread.Start();
            }

            public void stop()
            {
                if (_thread == null)
                    return;

                if (_isStopRequested)
                    return;
      
                _isStopRequested = true;
                _thread.Abort();
                _thread = null;
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (mServiceAnnouncer != null)
                mServiceAnnouncer.stop();
            mServiceAnnouncer = null;
        }
    }
}
