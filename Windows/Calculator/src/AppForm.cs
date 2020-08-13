using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.JBM;
using MathEx.ExternalBallistic.Units;
using Gehtsoft.BallisticCalculator.Connectivity;


namespace Gehtsoft.BallisticCalculator
{
    public partial class AppForm : Form
    {
        private ComparisonForm mComparison;

        public AppForm()
        {
            InitializeComponent();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItemWindowsTile_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void menuItemWindowsCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void menuItemNewImperial_Click(object sender, EventArgs e)
        {
            TraceForm form = new TraceForm();
            form.SetDefault(UI.MeasurementSystem.Imperial);
            form.MdiParent = this;
            form.Show();
        }

        private void menuItemNewMetric_Click(object sender, EventArgs e)
        {
            TraceForm form = new TraceForm();
            form.SetDefault(UI.MeasurementSystem.Metric);
            form.MdiParent = this;
            form.Show();
        }

        public void AddToComparsion(int seria, BallisticInfoCollection collection)
        {
            if (mComparison == null)
            {
                mComparison = new ComparisonForm();
                mComparison.MdiParent = this;
                mComparison.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Comparison_FormClosed);
            }

            mComparison.AddSeria(seria, collection);

            mComparison.Activate();
            mComparison.Show();
        }

        private void Comparison_FormClosed(object sender, FormClosedEventArgs e)
        {
            mComparison = null;
        }

        private void menuItemOpenTrace_Click(object sender, EventArgs e)
        {
            TraceForm form = new TraceForm();
            form.SetDefault(UI.MeasurementSystem.Metric);
            form.MdiParent = this;
            form.RestoreTrace();
            form.Show();
        }

        public void OpenTraceInfo(TraceInfo traceInfo)
        {
            TraceForm form = new TraceForm();
            form.MdiParent = this;
            form.RestoreTraceInfo(traceInfo);
            form.Show();
        }

        private void menuPopupFile_DropDownOpening(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is TraceForm)
            {
                TraceForm ts = ActiveMdiChild as TraceForm;
                menuItemSave.Enabled = ts.CanSaveTrace();
                menuItemSaveAs.Enabled = true;
            }
            else
            {
                menuItemSave.Enabled = false;
                menuItemSaveAs.Enabled = false;
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is TraceForm)
            {
                TraceForm ts = ActiveMdiChild as TraceForm;
                ts.SaveTrace();
            }

        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null && ActiveMdiChild is TraceForm)
            {
                TraceForm ts = ActiveMdiChild as TraceForm;
                ts.SaveAsTrace();
            }

        }

        WebServerForm mWebServer = null;

        private void menuItemStartServer_Click(object sender, EventArgs e)
        {
            if (mWebServer == null)
            {
                mWebServer = new WebServerForm();
                mWebServer.FormClosed += new FormClosedEventHandler(this.WebServer_FormClosed);
                mWebServer.MdiParent = this;
            }
            mWebServer.Activate();
            mWebServer.Show();

        }

        private void WebServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            mWebServer = null;
        }

        public void AddTraceInfo(TraceInfo traceInfo)
        {
            if (mWebServer == null)
                menuItemStartServer_Click(this, null);
            mWebServer.AddTrace(traceInfo);
        }

    }
}
