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
using BallisticCalculator.Utils;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);

            Button buttonUnits = FindViewById<Button>(Resource.Id.buttonUnits);
            buttonUnits.Click += buttonUnits_Click;
            string[] unitLabels = Resources.GetStringArray(Resource.Array.dd_lbls_Units);
            buttonUnits.Text += String.Format(" ({0})",
                ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial ?
                unitLabels[0] : unitLabels[1]);
        }

        void buttonUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);
            alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Units, ListClicked);
            alertDialogBuilder.Show();
        }

        private void spinnerUnits_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ApplicationData.Instance.PrefferedUnits = (ApplicationData.EPrefferedUnits)e.Position;
        }

        private void ListClicked(object sender, DialogClickEventArgs e)
        {
            ApplicationData.Instance.PrefferedUnits = (ApplicationData.EPrefferedUnits)e.Which;

            ISharedPreferencesEditor editor = GetPreferences(FileCreationMode.Private).Edit();
            editor.PutInt("preffered_units", (int)ApplicationData.Instance.PrefferedUnits);
            editor.Commit();

            Button buttonUnits = FindViewById<Button>(Resource.Id.buttonUnits);
            string[] unitLabels = Resources.GetStringArray(Resource.Array.dd_lbls_Units);
            buttonUnits.Text = String.Format("{0} ({1})", Resources.GetString(Resource.String.dd_prompt_Units),
                ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Imperial ?
                unitLabels[0] : unitLabels[1]);
        }
    }
}