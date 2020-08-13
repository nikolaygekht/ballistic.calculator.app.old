using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Gehtsoft.BallisticCalculator.Utils;
using MathEx.ExternalBallistic.Units;
using System;
using System.Collections.Generic;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Beaufort Scale")]
    public class WindSpeedListActivity : Activity
    {
        private static double[] _windSpeedMin;
        private static double[] _windSpeedMax;
        private const string FORMAT_RANGE_STRING = "{0:0.00} {1} - {2:0.00} {3} \n{4}";
        private ListView _listViewWindSpeed;
        private Velocity.Unit _windSpeedDisplayUnits;
        private Velocity.Unit _windSppedScaleUnits;
        private List<string> _listViewItems;
        private string[] _windScaleDescriptions;
        
        static WindSpeedListActivity()
        {
            _windSpeedMin = new double[] { 0, 1, 4, 8, 13, 19, 25, 32, 39, 47, 55, 64, 73 };
            _windSpeedMax = new double[]  { 1, 3, 7, 12, 18, 24, 31, 38, 46, 54, 63, 72, Double.PositiveInfinity };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WindSpeedList);

            var selectedUnitsStr = Intent.GetStringExtra("SelectedUnits");
            if (!string.IsNullOrEmpty(selectedUnitsStr))
                _windSpeedDisplayUnits = Velocity.NameToUnit(selectedUnitsStr);
            else
                _windSpeedDisplayUnits = DefaultUnits.Wind.Velocity;

            _windScaleDescriptions = Resources.GetStringArray(Resource.Array.dd_lblsBeaufortWindScaleDescription);
            _windSppedScaleUnits = Velocity.Unit.MilesPerHour;

            CreateControls();
            InitControls();
        }
        
        private void CreateControls()
        {
            _listViewWindSpeed = FindViewById<ListView>(Resource.Id.windSpeedLsit);
            _listViewWindSpeed.ItemClick += windSpeedView_ItemClick;
        }

        private void windSpeedView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (_listViewWindSpeed.CheckedItemCount > 0)
            {
                double windSpeed;
                var pos = _listViewWindSpeed.CheckedItemPosition;
                var data = new Intent();

                if (pos != _windSpeedMin.Length - 1)
                    windSpeed = (_windSpeedMin[pos] + _windSpeedMax[pos]) / 2;
                else
                    windSpeed = _windSpeedMin[_windSpeedMin.Length - 1];

                var windSpeedVelocity = Velocity.Convert(windSpeed, _windSppedScaleUnits, _windSpeedDisplayUnits);
                data.PutExtra("WindSpeedValue", windSpeedVelocity);
                data.PutExtra("SelectedUnits",Velocity.UnitToName(_windSpeedDisplayUnits));
                SetResult(Result.Ok, data);
                Finish();
            }
        }

        private void InitControls()
        {
            _listViewItems = new List<string>();

            for (int i = 0; i < _windSpeedMin.Length; i++)
            {
                _listViewItems.Add(CreateWindScaleRowString(i));
            }

            _listViewWindSpeed.Adapter = new ArrayAdapter<string> (
                                   this,
                                   Android.Resource.Layout.SimpleListItemActivated1,
                                   _listViewItems
                                   );
            _listViewWindSpeed.ChoiceMode = ChoiceMode.Single;
        }

        private string CreateWindScaleRowString(int index)
        {
            var unit = Velocity.UnitToName(_windSpeedDisplayUnits);
            var velocityItem1 = Velocity.Convert(_windSpeedMin[index], _windSppedScaleUnits, _windSpeedDisplayUnits);
            var velocityItem2 = Velocity.Convert(_windSpeedMax[index], _windSppedScaleUnits, _windSpeedDisplayUnits);

            var result = string.Format(FORMAT_RANGE_STRING, 
                                       velocityItem1, 
                                       unit, 
                                       velocityItem2, 
                                       unit,
                                       _windScaleDescriptions[index]);

            return result;
            
        }
    }
}