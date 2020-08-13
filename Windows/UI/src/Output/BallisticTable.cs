using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class BallisticTable : UserControl, IMeasurementSystemListener
    {

        private BallisticModel mModel = new BallisticModel();

        public BallisticInfoCollection BallisticInfo
        {
            get
            {
                return mModel.BallisticInfo;
            }
            set
            {
                listViewData.Items.Clear();
                mModel.BallisticInfo = value;
                FillTable();
            }
        }

      
        public MeasurementSystem MeasurementSystem
        {
            get
            {
                
                return mModel.MeasurementSystem;
            }
            set
            {
                bool changed = mModel.MeasurementSystem != value;
                mModel.MeasurementSystem = value;
                if (changed)
                {
                    string[] arr = null;
                    mModel.GetHeaderDataExport(ref arr);
                    listViewData.Columns[1].Text = arr[(uint)ColumnData.Range];
                    listViewData.Columns[2].Text = arr[(uint)ColumnData.Velocity];
                    listViewData.Columns[3].Text = arr[(uint)ColumnData.Mach];
                    listViewData.Columns[4].Text = arr[(uint)ColumnData.Energy];
                    listViewData.Columns[5].Text = arr[(uint)ColumnData.Path];
                    listViewData.Columns[6].Text = arr[(uint)ColumnData.Hold];
                    listViewData.Columns[7].Text = arr[(uint)ColumnData.VClick];
                    listViewData.Columns[8].Text = arr[(uint)ColumnData.Windage];
                    listViewData.Columns[9].Text = arr[(uint)ColumnData.WindageAdj];
                    listViewData.Columns[10].Text = arr[(uint)ColumnData.HClick];
                    listViewData.Columns[11].Text = arr[(uint)ColumnData.FlightTime];
                    listViewData.Columns[12].Text = arr[(uint)ColumnData.OGW];
                }
                if (mModel.BallisticInfo != null && changed)
                    FillTable();

            }
        }

        public Angle.Unit AngleUnits
        {
            get
            {
                return mModel.AngleUnits;
            }
            set
            {
                bool changed = mModel.AngleUnits != value;
                mModel.AngleUnits = value;
                string[] arr = null;
                if (changed)
                {
                    mModel.GetHeaderDataExport(ref arr);
                    listViewData.Columns[6].Text = arr[(uint)ColumnData.Hold];
                    listViewData.Columns[9].Text = arr[(uint)ColumnData.WindageAdj];
                }

                if (mModel.BallisticInfo != null && changed)
                    FillTable();
            }
        }

        public BallisticTable()
        {
            InitializeComponent();
            listViewData.Columns[0].TextAlign = HorizontalAlignment.Right;
        }

        private void FillTable()
        {
            if (mModel.BallisticInfo == null)
                return;
            string[] arr = new string[(uint)ColumnData.__MAX];
            if (mModel.BallisticInfo.Count == listViewData.Items.Count)
            {
                for (int i = 0; i < mModel.BallisticInfo.Count; i++)
                {
                    ListViewItem lvi = listViewData.Items[i];
                    mModel.GetOneRowDataDisplay(i, ref arr);
                    lvi.SubItems[1].Text = arr[(uint)ColumnData.Range];
                    lvi.SubItems[2].Text = arr[(uint)ColumnData.Velocity];
                    lvi.SubItems[3].Text = arr[(uint)ColumnData.Mach];
                    lvi.SubItems[4].Text = arr[(uint)ColumnData.Energy];
                    lvi.SubItems[5].Text = arr[(uint)ColumnData.Path];
                    lvi.SubItems[6].Text = arr[(uint)ColumnData.Hold];
                    lvi.SubItems[7].Text = arr[(uint)ColumnData.VClick];
                    lvi.SubItems[8].Text = arr[(uint)ColumnData.Windage];
                    lvi.SubItems[9].Text = arr[(uint)ColumnData.WindageAdj];
                    lvi.SubItems[10].Text = arr[(uint)ColumnData.HClick];
                    lvi.SubItems[11].Text = arr[(uint)ColumnData.FlightTime];
                    lvi.SubItems[12].Text = arr[(uint)ColumnData.OGW];
                }
            }
            else
            {
                for (int i = 0; i < mModel.BallisticInfo.Count; i++)
                {
                    ListViewItem lvi = listViewData.Items.Add("");
                    mModel.GetOneRowDataDisplay(i, ref arr);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Range]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Velocity]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Mach]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Energy]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Path]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Hold]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.VClick]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.Windage]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.WindageAdj]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.HClick]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.FlightTime]);
                    lvi.SubItems.Add(arr[(uint)ColumnData.OGW]);
                }
            }
        }

        public bool CanExportCsv
        {
            get
            {
                return mModel.BallisticInfo != null;
            }
        }

        private string ArrToString(string[] arr)
        {
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != 0)
                    b.Append(',');
                if (arr[i] != null)
                {
                    bool quote = false;
                    if (arr[i].Contains(","))
                        quote = true;
                    if (quote)
                        b.Append('"');
                    b.Append(arr[i]);
                    if (quote)
                        b.Append('"');
                }
                else
                    b.Append("null");
            }
            return b.ToString();
        }

        public void ExportCsv(Form parent)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Csv files (*.csv)|*.csv|All files|*.*";
            dlg.AddExtension = true;
            dlg.DefaultExt = "csv";
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;

            if (dlg.ShowDialog(parent) == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(dlg.FileName, false, Encoding.ASCII))
                    {
                        string[] arr = null;

                        mModel.GetHeaderDataExport(ref arr);
                        writer.WriteLine(ArrToString(arr));

                        for (int i = 0; i < mModel.BallisticInfo.Count; i++ )
                        {
                            mModel.GetOneRowDataExport(i, ref arr);
                            writer.WriteLine(ArrToString(arr));
                        }

                        writer.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parent, ex.ToString(), "Save CSV error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void ExportExcel(Form parent)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel files (*.xls)|*.xls|All files|*.*";
            dlg.AddExtension = true;
            dlg.DefaultExt = "xls";
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;

            if (dlg.ShowDialog(parent) == DialogResult.OK)
            {
                try
                {
                    Spreadsheet sp = PrintPreviewFactory.CreatePrintPreview(mModel);
                    sp.Raw.Save(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parent, ex.ToString(), "Save Excel error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void PrintPreview(Form parent)
        {
            try
            {
                Spreadsheet sp = PrintPreviewFactory.CreatePrintPreview(mModel);
                PrintPreviewForm frm = new PrintPreviewForm(sp);
                frm.ShowDialog(parent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(parent, ex.ToString(), "Save Excel error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
