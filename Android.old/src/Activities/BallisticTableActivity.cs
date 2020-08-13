using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using MathEx.ExternalBallistic;
using Gehtsoft.BallisticCalculator.Connectivity;
using BallisticCalculator.Utils;
using Android.Graphics;
using MathEx.ExternalBallistic.Units;
using System.Collections.Generic;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Ballistic Table")]
    public class BallisticTableActivity : Activity
    {
        private TableLayout tableHeaderLayout;
        private TableLayout tableDataLayout;
        private const int columnsCount = 13;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BallisticTable);


            if (ApplicationData.Instance.SelectedTraceInfo == null)
            {
                Toast.MakeText(this, Resource.String.msg_TraceNotSelected, ToastLength.Long).Show();
                Finish();
                return;
            }

            tableDataLayout = FindViewById<TableLayout>(Resource.Id.tableLayoutBallisticTable);
            tableHeaderLayout = FindViewById<TableLayout>(Resource.Id.tableLayoutBallisticTableHeader);

            ApplicationData.Instance.calculateBallisticInfo(initTable);
        }

        private void initTable()
        {
            bool rowColorShuffleFlag = false;

            TableRow tableHeaderRow = createRow(createHeaderRowData(), Color.DarkRed);
            RunOnUiThread(() => tableHeaderLayout.AddView(tableHeaderRow));

            foreach (BallisticInfo info in ApplicationData.Instance.BallisticInfoCollection)
            {
                string[] rowData = createRowData(info);
                TableRow tableRow = createRow(rowData, rowColorShuffleFlag ? Color.Black : Color.DarkGray);
                rowColorShuffleFlag = !rowColorShuffleFlag;

                RunOnUiThread(() => tableDataLayout.AddView(tableRow));
            }

            // Calculate widest column widths
            int[] tableWidths = new int[columnsCount - 1];
            observeTableLayoutCells(tableHeaderLayout, (TextView tv, int i, int j) =>
            {
                if (tv.MeasuredWidth == 0)
                    RunOnUiThread(() => tv.Measure(0, 0));
                tableWidths[j] = Math.Max(tableWidths[j], tv.MeasuredWidth);
            });
            observeTableLayoutCells(tableDataLayout, (TextView tv, int i, int j) =>
            {
                if (tv.MeasuredWidth == 0)
                    RunOnUiThread(() => tv.Measure(0, 0));
                tableWidths[j] = Math.Max(tableWidths[j], tv.MeasuredWidth);
            });

            // Apply column widths
            observeTableLayoutCells(tableHeaderLayout, (TextView tv, int i, int j) =>
                RunOnUiThread(() =>
                    tv.LayoutParameters = new TableRow.LayoutParams(tableWidths[j], TableRow.LayoutParams.WrapContent)));
            observeTableLayoutCells(tableDataLayout, (TextView tv, int i, int j) =>
                RunOnUiThread(() =>
                    tv.LayoutParameters = new TableRow.LayoutParams(tableWidths[j], TableRow.LayoutParams.WrapContent)));
        }

        private static void observeTableLayoutCells(TableLayout layout, Action<TextView, int, int> observer)
        {
            int cell_i = 0;

            for (int i = 0; i < layout.ChildCount; ++i)
            {
                if (layout.GetChildAt(i).GetType() == typeof(TableRow))
                {
                    TableRow row = (TableRow)layout.GetChildAt(i);
                    int cell_j = 0;
                    for (int j = 0; j < row.ChildCount; ++j)
                    {
                        if (row.GetChildAt(j).GetType() == typeof(TextView))
                        {
                            TextView cell = (TextView)row.GetChildAt(j);

                            observer(cell, cell_i, cell_j);
                            ++cell_j;
                        }
                    }
                    ++cell_i;
                }
            }
        }

        private string[] createRowData(BallisticInfo ballisticInfo)
        {
            string[] data = new string[columnsCount - 1];
            bool isFirstRow = ballisticInfo.Range.Get(DefaultUnits.Range) == 0;

            data[0] = ballisticInfo.Range.Get(DefaultUnits.Range).ToString("f0");
            data[1] = ballisticInfo.BulletVelocity.Get(DefaultUnits.Bullet.Velocity).ToString("f1");
            data[2] = ballisticInfo.Mach.ToString("f2");
            data[3] = ballisticInfo.BulletEnergy.Get(DefaultUnits.Bullet.Energy).ToString("f0");
            data[4] = ballisticInfo.Path.Get(DefaultUnits.Drop).ToString("f2");
            data[5] = isFirstRow ? "" : ballisticInfo.Hold.Get(DefaultUnits.Reticle.Adjustment).ToString("f2");
            data[6] = isFirstRow ? "" : ballisticInfo.HoldClicks.ToString("+#;-#;0");
            data[7] = ballisticInfo.Windage.Get(DefaultUnits.Windage).ToString("f2");
            data[8] = isFirstRow ? "" : ballisticInfo.WindageCorrection.Get(DefaultUnits.Reticle.Adjustment).ToString("f2");
            data[9] = isFirstRow ? "" : ballisticInfo.WindageClicks.ToString("+#;-#;0");
            data[10] = ballisticInfo.Time.ToString(@"mm\:ss\.fff");
            data[11] = ballisticInfo.OptimalGameWeight.Get(DefaultUnits.Target.Weight).ToString("f0");

            return data;
        }

        private string[] createHeaderRowData()
        {
            string[] data = new string[columnsCount - 1];

            data[0] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_Range), Distance.UnitToName(DefaultUnits.Range));
            data[1] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_Velocity), Velocity.UnitToName(DefaultUnits.Bullet.Velocity));
            data[2] = String.Format("{0}\n", GetString(Resource.String.tv_lbl_Mach));
            data[3] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_Energy), Energy.UnitToName(DefaultUnits.Bullet.Energy));
            data[4] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_Path), Distance.UnitToName(DefaultUnits.Drop));
            data[5] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_Hold), Angle.UnitToName(DefaultUnits.Reticle.Adjustment));
            data[6] = String.Format("{0}\n", GetString(Resource.String.tv_lbl_Clicks));
            data[7] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_Windage), Distance.UnitToName(DefaultUnits.Windage));
            data[8] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_WindAdjustment), Angle.UnitToName(DefaultUnits.Reticle.Adjustment));
            data[9] = String.Format("{0}\n", GetString(Resource.String.tv_lbl_Clicks));
            data[10] = String.Format("{0}\n", GetString(Resource.String.tv_lbl_FlightTime));
            data[11] = String.Format("{0}\n({1})", GetString(Resource.String.tv_lbl_OGW), Weight.UnitToName(DefaultUnits.Target.Weight));

            return data;
        }

        private TableRow createRow(string[] columnData, Color rowColor)
        {
            TableRow tableRow = new TableRow(this);
            tableRow.SetBackgroundColor(rowColor);
            tableRow.LayoutParameters = new TableLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent);

            for (int i = 0; i < columnData.Length; ++i)
            {
                TextView tv = new TextView(this);
                tv.SetPadding(5, 5, 5, 5);
                tv.LayoutParameters = new TableRow.LayoutParams();
                tv.Text = columnData[i];
                tv.Gravity = GravityFlags.CenterHorizontal;
                tableRow.AddView(tv);
            }

            return tableRow;
        }
    }
}
