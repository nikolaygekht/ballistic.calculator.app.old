using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Model;
using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Connectivity;
using Android.Graphics;
using Gehtsoft.BallisticCalculator.Utils;
using MathEx.ExternalBallistic.Units;
using System.Threading;
using static Android.Views.ViewTreeObserver;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Ballistic Table")]
    public class BallisticTableActivity : Activity, ICalculatorDelegate, IOnGlobalLayoutListener
    {
        private BallisticDataProvider _dataProvider;
        private Calculator _ballisticCalculator;
        private TraceInfo _traceInfo;

        private const int _columnCount = 13;

        private TableLayout _tableData;
        private TableLayout _tableHeader;

        private ProgressDialog _progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BallisticTable);

            _progressDialog = new ProgressDialog(this);
            _progressDialog.SetMessage("Calculating, please wait...");
            _progressDialog.SetCancelable(false);
            _progressDialog.Show();

            _dataProvider = BallisticDataProvider.Instance;
            _traceInfo = _dataProvider.TraceData.SelectedTraceInfo;
            _ballisticCalculator = new Calculator();
            _ballisticCalculator.Delegate = this;

            if (_traceInfo != null)
            {
                CreateControls();
                _ballisticCalculator.CalculateBallisticInfo();
            }
            else
                finishActivity();
        }

        public void OnBallisticInfoCalculated()
        {
            if (_dataProvider.BallisticInfo == null ||
                _dataProvider.BallisticInfo.Count == 0)
            {
                Toast.MakeText(this, Resource.String.msg_InternalError, ToastLength.Long).Show();
                Finish();
                return;
            }

            var tableHeaderRow = createTableRow(fillTableHeaderData(), Color.DarkBlue);
             _tableHeader.AddView(tableHeaderRow);

            bool rowColorFlag = true;
            foreach (var info in _dataProvider.BallisticInfo)
            {
                rowColorFlag = !rowColorFlag;
                var rowData = fillTableRowData(info);
                var tableRow = createTableRow(rowData, rowColorFlag ? Color.Black : Color.DarkGray);
                _tableData.AddView(tableRow);
            }

            ApplyColumnWidth();

           _tableData.ViewTreeObserver.AddOnGlobalLayoutListener(this);
        }

        public void OnSingleShotCalculated(BallisticInfo info)
        {
        }

        private void CreateControls()
        {
            _tableData = FindViewById<TableLayout>(Resource.Id.tableLayoutBallisticTable);
            _tableHeader = FindViewById<TableLayout>(Resource.Id.tableLayoutBallisticTableHeader);
        }

        private void finishActivity()
        {
            if (_progressDialog != null)
            {
                _progressDialog.Dismiss();
                _progressDialog = null;
            }
            Toast.MakeText(this, Resource.String.msg_TraceNotSelected, ToastLength.Long).Show();
            Finish();
        }

        private TableRow createTableRow(List<string> columns, Color color)
        {
            var tableRow = new TableRow(this);
            foreach (var data in columns)
            {
                var textView = new TextView(this);
                textView.SetPadding(5, 5, 5, 5);
                textView.Text = data;
                textView.Gravity = GravityFlags.CenterHorizontal;
                tableRow.AddView(textView);
            }

            tableRow.SetBackgroundColor(color);
            tableRow.LayoutParameters = new TableRow.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.WrapContent
                     );

            return tableRow;
        }

        private static List<string> fillTableRowData(BallisticInfo info)
        {
            List<string> result = new List<string>();
            bool isfirstRow = info.Range.Get(DefaultUnits.Range) == 0;

            result.Add(info.Range.Get(DefaultUnits.Range).ToString("f0"));
            result.Add(info.BulletVelocity.Get(DefaultUnits.Bullet.Velocity).ToString("f1"));
            result.Add(info.Mach.ToString("f2"));
            result.Add(info.BulletEnergy.Get(DefaultUnits.Bullet.Energy).ToString("f0"));
            result.Add(info.Path.Get(DefaultUnits.Drop).ToString("f2"));
            result.Add(isfirstRow ? "" : info.Hold.Get(DefaultUnits.Reticle.Adjustment).ToString("f2"));
            result.Add(isfirstRow ? "" : info.HoldClicks.ToString("+#;-#;0"));
            result.Add(info.Windage.Get(DefaultUnits.Windage).ToString("f2"));
            result.Add(isfirstRow ? "" : info.WindageCorrection.Get(DefaultUnits.Reticle.Adjustment).ToString("f2"));
            result.Add(isfirstRow ? "" : info.WindageClicks.ToString("+#;-#;0"));
            result.Add(info.Time.ToString(@"mm\:ss\.fff"));
            result.Add(info.OptimalGameWeight.Get(DefaultUnits.Target.Weight).ToString("f0"));
            return result;
        }

        private static void createObservableTableLayout(TableLayout layout, Action<TextView, int, int> observer)
        {
            int cellIndexRow = 0;
            int cellIndexColumn;
            var typeTebleRow = typeof(TableRow);
            for (int i = 0; i < layout.ChildCount; ++i)
            {
                if (layout.GetChildAt(i).GetType() != typeTebleRow)
                    continue;
                cellIndexColumn = 0;
                var tableRow = (TableRow)layout.GetChildAt(i);
                for (int j = 0; j < tableRow.ChildCount; ++j)
                {
                    var textViewInCell = (TextView)tableRow.GetChildAt(j);
                    observer(textViewInCell, cellIndexRow, cellIndexColumn);
                }
            } 
        }

        private List<string> fillTableHeaderData()
        {
            List<string> result = new List<string>();

            result.Add(
                string.Format("{0}\n({1})",
                    GetString(Resource.String.tv_lbl_Range), 
                    Distance.UnitToName(DefaultUnits.Range))
            );
            result.Add(
                string.Format("{0}\n({1})", 
                    GetString(Resource.String.tv_lbl_Velocity), 
                    Velocity.UnitToName(DefaultUnits.Bullet.Velocity))
            );
            result.Add(
                string.Format("{0}\n", 
                    GetString(Resource.String.tv_lbl_Mach))
            );
            result.Add(
                string.Format("{0}\n({1})", 
                    GetString(Resource.String.tv_lbl_Energy), 
                    Energy.UnitToName(DefaultUnits.Bullet.Energy))
            );
            result.Add(
                string.Format("{0}\n({1})", 
                    GetString(Resource.String.tv_lbl_Path), 
                    Distance.UnitToName(DefaultUnits.Drop))
            );
            result.Add(
                string.Format("{0}\n({1})", 
                    GetString(Resource.String.tv_lbl_Hold), 
                    Angle.UnitToName(DefaultUnits.Reticle.Adjustment))
            );
            result.Add(
                string.Format("{0}\n", 
                    GetString(Resource.String.tv_lbl_Clicks))
            );
            result.Add(
                string.Format("{0}\n({1})", 
                    GetString(Resource.String.tv_lbl_Windage), 
                    Distance.UnitToName(DefaultUnits.Windage))
            );
            result.Add(
                string.Format("{0}\n({1})",
                    GetString(Resource.String.tv_lbl_WindAdjustment), 
                    Angle.UnitToName(DefaultUnits.Reticle.Adjustment))
            );
            result.Add(
                string.Format("{0}\n",
                    GetString(Resource.String.tv_lbl_Clicks))
            );
            result.Add
                (string.Format("{0}\n", 
                    GetString(Resource.String.tv_lbl_FlightTime))
            );
            result.Add(
                string.Format("{0}\n({1})", 
                    GetString(Resource.String.tv_lbl_OGW), 
                    Weight.UnitToName(DefaultUnits.Target.Weight))
            );

            return result;
        }

        private void ApplyColumnWidth()
        {
            int[] tableWidth = new int[_columnCount - 1];
            createObservableTableLayout(_tableHeader, (TextView textView, int i, int j) =>
            {
                if (textView.MeasuredWidth == 0)
                    textView.Measure(0, 0);
                tableWidth[j] = Math.Max(tableWidth[j], textView.MeasuredWidth);
            });

            createObservableTableLayout(_tableData, (TextView textView, int i, int j) =>
            {
                if (textView.MeasuredWidth == 0)
                    textView.Measure(0, 0);
                tableWidth[j] = Math.Max(tableWidth[j], textView.MeasuredWidth);
            });

            createObservableTableLayout(_tableHeader, (TextView textView, int i, int j) => 
                    textView.LayoutParameters = new TableRow.LayoutParams(tableWidth[j], 
                        TableRow.LayoutParams.WrapContent));

            createObservableTableLayout(_tableData, (TextView textView, int i, int j) =>
                    textView.LayoutParameters = new TableRow.LayoutParams(tableWidth[j], 
                        TableRow.LayoutParams.WrapContent));
        }

        public void OnGlobalLayout()
        {
            _tableData.ViewTreeObserver.RemoveGlobalOnLayoutListener(this);

            if (_progressDialog != null)
            {
                _progressDialog.Dismiss();
                _progressDialog = null;
            }
        }
    }
}