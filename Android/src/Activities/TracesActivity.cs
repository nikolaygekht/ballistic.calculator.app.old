using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Gehtsoft.BallisticCalculator.DataProviders;
using System;
using System.Collections.Generic;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Traces")]
    public class TracesActivity : Activity
    {
        private const string TRACE_NAME_PROPERTY = "TraceName";

        private Button _buttonSelectTrace;
        private Button _buttonAddTrace;
        private Button _buttonEditTrace;
        private Button _buttonCancel;

        private ListView _ListViewTraces;
        private List<string> _listViewItems;

        private BallisticDataProvider _dataProvider;
        private bool _traceSelected;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Traces);

            _dataProvider = BallisticDataProvider.Instance;

            CraeteButtons();
            CreateListView();
        }

        private void CraeteButtons()
        {
            _buttonSelectTrace = FindViewById<Button>(Resource.Id.buttonSelect);
            _buttonAddTrace = FindViewById<Button>(Resource.Id.buttonAdd);
            _buttonEditTrace = FindViewById<Button>(Resource.Id.buttonEdit);
            _buttonCancel = FindViewById<Button>(Resource.Id.buttonCancel);

            _buttonEditTrace.Enabled = false;

            _buttonSelectTrace.Click += buttonSelectTrace_Click;
            _buttonAddTrace.Click += buttonAddTrace_Click; 
            _buttonEditTrace.Click += buttonEditTrace_Click;
            _buttonCancel.Click += buttonCancel_Click;
        }

        private void CreateListView()
        {
            if (_ListViewTraces == null)
            {
                _ListViewTraces = FindViewById<ListView>(Resource.Id.listViewTraces);
                _ListViewTraces.ItemClick += ltvTraces_ItemClick;
            }
            _listViewItems = new List<string>();

            var traces = _dataProvider.TraceData.TraceInfoCollection;

            if (traces != null)
            {
                for (int i = 0; i < traces.Count; i++)
                    _listViewItems.Add(traces[i].TraceName);
            }

            _ListViewTraces.Adapter =
               new ArrayAdapter<string>(
                                   this,
                                   Android.Resource.Layout.SimpleListItemActivated1,
                                   _listViewItems
                                   );

            _ListViewTraces.ChoiceMode = ChoiceMode.Single;
        }

        void ltvTraces_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _buttonEditTrace.Enabled = true;
        }

        void buttonCancel_Click(object sender, EventArgs e)
        {
            SetResult(Result.Canceled, new Intent());
            base.OnBackPressed();            
        }

        void buttonEditTrace_Click(object sender, EventArgs e)
        {
            if (_ListViewTraces.CheckedItemCount > 0)
            {
                Intent intentEditTrace = new Intent(this, typeof(EditTraceActivity));
                intentEditTrace.PutExtra("SelectedTracePosition", _ListViewTraces.CheckedItemPosition);
                StartActivityForResult(intentEditTrace, 1);
            }
        }

        void buttonAddTrace_Click(object sender, EventArgs e)
        {
            Intent intentEditTrace = new Intent(this, typeof(EditTraceActivity));
            StartActivityForResult(intentEditTrace, 1);
        }

        void buttonSelectTrace_Click(object sender, EventArgs e)
        {
            if (_ListViewTraces.CheckedItemCount > 0)
            {
                string traceName = _listViewItems[_ListViewTraces.CheckedItemPosition];
                _dataProvider.SetSelectedTraceByName(traceName);
                SetResult(Result.Ok, new Intent());
                Finish();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1 && resultCode == Result.Ok)
            {
                _dataProvider.SaveTraces();
                CreateListView();
            }
        }
    }
}

