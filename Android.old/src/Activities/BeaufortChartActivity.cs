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
using MathEx.ExternalBallistic.Units;
using BallisticCalculator.Utils;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Beaufort Wind Scale")]
    public class BeaufortChartActivity : Activity
    {
        ListView beaufortListView;
        BeaufortWindScale beaufortWindScale;
        Velocity.Unit selectedUnits;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BeaufortChart);

            string selectedUnitsString = Intent.GetStringExtra("SelectedUnits");
            try
            {
                selectedUnits = Velocity.NameToUnit(selectedUnitsString);
            }
            catch (ArgumentException)
            {
                selectedUnits = DefaultUnits.Wind.Velocity;
            }

            beaufortListView = FindViewById<ListView>(Resource.Id.listViewBeaufortChartEffects);
            beaufortWindScale = new BeaufortWindScale(this, selectedUnits);

            beaufortListView.Adapter = beaufortWindScale.CreateTwoLineAdapter(this);
            beaufortListView.ItemClick += beaufortListView_ItemClick;
        }

        void beaufortListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(beaufortWindScale.GetScaleName(e.Position));
            StringBuilder message = new StringBuilder();
            message.AppendLine(beaufortWindScale.GetScaleDescription(e.Position));
            if (e.Position == 12)
                message.AppendLine(string.Format("{0:0.00} {1} +",
                    beaufortWindScale.GetMinWindSpeed(e.Position).Get(selectedUnits), Velocity.UnitToName(selectedUnits)));
            else
                message.AppendLine(string.Format("{0:0.00} {1} - {2:0.00} {3}",
                    beaufortWindScale.GetMinWindSpeed(e.Position).Get(selectedUnits), Velocity.UnitToName(selectedUnits),
                    beaufortWindScale.GetMaxWindSpeed(e.Position).Get(selectedUnits), Velocity.UnitToName(selectedUnits)));
            alertDialogBuilder.SetMessage(message.ToString());
            alertDialogBuilder.SetPositiveButton(Resource.String.btn_lbl_Select, (s, a) =>
            {
                if (e.Position == 12)
                {
                    SelectWindSpeed(beaufortWindScale.GetMinWindSpeed(e.Position));
                }
                else
                {
                    double mediumSpeed = (beaufortWindScale.GetMinWindSpeed(e.Position).Get(selectedUnits) +
                                         beaufortWindScale.GetMaxWindSpeed(e.Position).Get(selectedUnits)) / 2;
                    SelectWindSpeed(new Velocity(mediumSpeed, selectedUnits));
                }
            });
            alertDialogBuilder.Show();
        }

        void SelectWindSpeed(Velocity windSpeed)
        {
            Bundle resultData = new Bundle();
            resultData.PutDouble("speed_value", windSpeed.Get(selectedUnits));
            resultData.PutString("speed_units", Velocity.UnitToName(selectedUnits));

            Intent result = new Intent();
            result.PutExtras(resultData);

            SetResult(Result.Ok, result);
            Finish();
        }
    }
}