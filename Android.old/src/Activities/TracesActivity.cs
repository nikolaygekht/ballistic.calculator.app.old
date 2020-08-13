
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

using Gehtsoft.BallisticCalculator.Connectivity;
using BallisticCalculator.Utils;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Select Trace")]
    public class TracesActivity : Activity
    {
        Button selectButton;
        Button addButton;
        Button editButton;
        Button cancelButton;

        ListView tracesListView;

        TraceInfoCollection traceInfoCollection;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Traces);

            traceInfoCollection = ApplicationData.Instance.TraceInfoCollection;

            selectButton = FindViewById<Button>(Resource.Id.buttonSelect);
            addButton = FindViewById<Button>(Resource.Id.buttonAdd);
            editButton = FindViewById<Button>(Resource.Id.buttonEdit);
            cancelButton = FindViewById<Button>(Resource.Id.buttonCancel);

            Utilities.SetButtonState(selectButton, false);
            Utilities.SetButtonState(editButton, false);

            tracesListView = FindViewById<ListView>(Resource.Id.listViewTraces);

            InitListView();
            tracesListView.ItemClick += tracesListView_ItemClick;

            cancelButton.Click += cancelButton_Click;
            addButton.Click += addButton_Click;
            editButton.Click += editButton_Click;
            selectButton.Click += selectButton_Click;

        }

        private void tracesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Utilities.SetButtonState(selectButton, true);
            Utilities.SetButtonState(editButton, true);
        }

        void editButton_Click(object sender, EventArgs e)
        {
            if (tracesListView.CheckedItemPosition < 0)
                return;
            Intent editTraceActivityIntent = new Intent(this, typeof(EditTraceActivity));
            editTraceActivityIntent.PutExtra("SelectedTraceID", tracesListView.CheckedItemPosition);
            StartActivityForResult(editTraceActivityIntent, 1);
        }

        void selectButton_Click(object sender, EventArgs e)
        {
            if (tracesListView.CheckedItemPosition < 0)
                return;
            ApplicationData.Instance.SelectedTraceInfo = traceInfoCollection[tracesListView.CheckedItemPosition];
            base.OnBackPressed();
        }

        void addButton_Click(object sender, EventArgs e)
        {
            Intent editTraceActivityIntent = new Intent(this, typeof(EditTraceActivity));
            StartActivityForResult(editTraceActivityIntent, 1);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private List<string> getTracesListViewData()
        {
            List<string> listItems = new List<string>();

            if (traceInfoCollection != null)
            {
                foreach (TraceInfo traceInfo in traceInfoCollection)
                {
                    listItems.Add(traceInfo.TraceName);
                }
            }
            return listItems;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                if (resultCode == Result.Ok)
                {
                    ApplicationData.Instance.SaveTracesToFile();
                    InitListView();
                }
            }
        }

        private void InitListView()
        {
            tracesListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, getTracesListViewData());
            tracesListView.ChoiceMode = ChoiceMode.Single;
            if (ApplicationData.Instance.SelectedTraceInfo != null)
            {
                int selectedTraceIndex = traceInfoCollection.Find(ApplicationData.Instance.SelectedTraceInfo.TraceName);
                if (selectedTraceIndex != -1)
                {
                    tracesListView.PerformItemClick(
                        tracesListView.Adapter.GetView(selectedTraceIndex, null, null),
                        selectedTraceIndex,
                        tracesListView.Adapter.GetItemId(selectedTraceIndex));
                    Utilities.SetButtonState(selectButton, true);
                    Utilities.SetButtonState(editButton, true);
                }
            }
        }
    }
}

