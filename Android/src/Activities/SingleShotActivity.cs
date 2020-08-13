using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Gehtsoft.BallisticCalculator.DataProviders;
using MathEx.ExternalBallistic.Units;
using Gehtsoft.BallisticCalculator.Utils;
using Gehtsoft.BallisticCalculator.Model;
using MathEx.ExternalBallistic;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Single Shot")]
    public class SingleShotActivity : Activity, ICalculatorDelegate
    {
        private static SingleShotResultHolder _singleShotResult;

        private BallisticDataProvider _dataProvider;
        private Calculator _ballisticCalculator;

        private TextView _textViewRange;
        private TextView _textViewWind;
        private TextView _textViewWindAngleUnit;
        private TextView _textViewCalculatedAngle;
        private TextView _textViewCalculationAngleClicks;
        private TextView _textViewCalculationWindAngle;
        private TextView _textViewCalculatedWindClicks;

        private Button _buttonCalculate;

        private NumberPicker _numberPickerRnage;
        private NumberPicker _numberPickerWind;
        private NumberPicker _numberPickerDirection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SingleShot);

            _dataProvider = BallisticDataProvider.Instance;
            _ballisticCalculator = new Calculator();
            _ballisticCalculator.Delegate = this;

            var traceInfo = _dataProvider.TraceData.SelectedTraceInfo;

            if (_singleShotResult == null)
            {
                _singleShotResult = new SingleShotResultHolder();
            }

            if (traceInfo == null)
            {
                finishActivity();
                return;
            }

            CreateControls();
            InitControls();
            InitUnits();
        }

        public void OnSingleShotCalculated(BallisticInfo ballistIcInfo)
        {
            if (ballistIcInfo == null)
            {
                _textViewCalculatedAngle.Text = "";
                _textViewCalculationAngleClicks.Text = "";
                _textViewCalculationWindAngle.Text = "";
                _textViewCalculatedWindClicks.Text = "";
            }
            else
            {
                _textViewCalculatedAngle.Text = ballistIcInfo.Hold.Get(DefaultUnits.Reticle.Adjustment).ToString("f2");
                _textViewCalculationAngleClicks.Text = ballistIcInfo.HoldClicks.ToString();
                _textViewCalculationWindAngle.Text = ballistIcInfo.WindageCorrection.Get(DefaultUnits.Reticle.Adjustment).ToString("f2");
                _textViewCalculatedWindClicks.Text = ballistIcInfo.WindageClicks.ToString();
            }
        }

        public void OnBallisticInfoCalculated()
        {
        }

        private void InitUnits()
        {
            _textViewWindAngleUnit.Text = 
                Angle.UnitToName(DefaultUnits.Reticle.Adjustment);
        }

        private void CreateControls()
        {
            _buttonCalculate = FindViewById<Button>(Resource.Id.buttonCalculateShot);
            _textViewWind = FindViewById<TextView>(Resource.Id.textViewWind);
            _textViewRange = FindViewById<TextView>(Resource.Id.textViewRange);
            _numberPickerRnage = FindViewById<NumberPicker>(Resource.Id.numberPickerRange);
            _numberPickerWind = FindViewById<NumberPicker>(Resource.Id.numberPickerWind);
            _numberPickerDirection = FindViewById<NumberPicker>(Resource.Id.numberPickerWindDirection);
            _textViewWindAngleUnit = FindViewById<TextView>(Resource.Id.textViewCorrectionAngleUnits);

            _textViewCalculatedAngle = FindViewById<TextView>(Resource.Id.textViewElevationCorrectionAngle);
            _textViewCalculationAngleClicks = FindViewById<TextView>(Resource.Id.textViewElevationCorrectionClicks);
            _textViewCalculationWindAngle = FindViewById<TextView>(Resource.Id.textViewWindCorrectionAngle);
            _textViewCalculatedWindClicks = FindViewById<TextView>(Resource.Id.textViewWindCorrectionClicks);

            FindViewById<Button>(Resource.Id.buttonCalculateShot).Click += SingleShotActivity_Click;
        }

        private void SingleShotActivity_Click(object sender, EventArgs e)
        {
            var shotData = _dataProvider.ShotData;

            var range = new Distance(_numberPickerRnage.Value * shotData.iStepForSingleShot, DefaultUnits.Range);
            var rangeStep = new Distance(shotData.iStepForSingleShot, DefaultUnits.Range);
            var windSpeed = new Velocity(_numberPickerWind.Value, DefaultUnits.Wind.Velocity);
            var windAngle = Utilities.AngleFromHour(_numberPickerDirection.Value);

            _ballisticCalculator.CalculateSingleShotInfo(range, rangeStep, windSpeed, windAngle);
        }

        private void InitControls()
        {
            var atmoData = _dataProvider.AtmosphereData;
            var shotData = _dataProvider.ShotData;

            _textViewRange.Text = _textViewRange.Text + "\n(" + Distance.UnitToName(DefaultUnits.Range) +")";
            _textViewWind.Text = _textViewWind.Text + "\n(" + Velocity.UnitToName(DefaultUnits.Wind.Velocity) +")";

            int windSpeed = (int)(atmoData.WindInfo.Speed.Get(DefaultUnits.Wind.Velocity) + 0.5);
            int windDirection = Utilities.HourFromAngle(atmoData.WindInfo.Direction);
            double maxDistance = shotData.MaxDistance.Get(DefaultUnits.Range);

            _numberPickerRnage.MinValue = 0;      
            _numberPickerRnage.MaxValue = (int)(maxDistance / shotData.iStepForSingleShot);
            _numberPickerRnage.SetFormatter(new NuberPickerFormatter(shotData.iStepForSingleShot));
            _numberPickerRnage.Value = shotData.iStepForSingleShot;

            _numberPickerWind.MinValue = 0;
            _numberPickerWind.MaxValue = atmoData.MaxWindSpeed;
            _numberPickerWind.Value = Math.Min(windSpeed, atmoData.MaxWindSpeed);

            _numberPickerDirection.MinValue = atmoData.WindDirectionMin;
            _numberPickerDirection.MaxValue = atmoData.WindDirectionMax;
            _numberPickerDirection.Value = windDirection;

            Utilities.FixNumberPickerBug(_numberPickerRnage);
        }

        private void finishActivity()
        {
            Toast.MakeText(this, Resource.String.msg_TraceNotSelected, ToastLength.Long).Show();
            Finish();
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (_singleShotResult != null)
            {
                _singleShotResult.NumberPickerDirectionValue = _numberPickerDirection.Value;
                _singleShotResult.NumberPickerRnageValue = _numberPickerRnage.Value;
                _singleShotResult.NumberPickerWindValue = _numberPickerWind.Value;
                _singleShotResult.TextViewCalculatedAngleText = _textViewCalculatedAngle.Text;
                _singleShotResult.TextViewCalculationAngleClicksText =_textViewCalculationAngleClicks.Text;
                _singleShotResult.TextViewCalculationWindAngleText =_textViewCalculationWindAngle.Text;
                _singleShotResult.TextViewCalculatedWindClicksText = _textViewCalculatedWindClicks.Text;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (_singleShotResult != null)
            {
                _numberPickerDirection.Value = _singleShotResult.NumberPickerDirectionValue;
                _numberPickerRnage.Value = _singleShotResult.NumberPickerRnageValue;
                _numberPickerWind.Value = _singleShotResult.NumberPickerWindValue;
                _textViewCalculatedAngle.Text = _singleShotResult.TextViewCalculatedAngleText;
                _textViewCalculationAngleClicks.Text = _singleShotResult.TextViewCalculationAngleClicksText;
                _textViewCalculationWindAngle.Text = _singleShotResult.TextViewCalculationWindAngleText;
                _textViewCalculatedWindClicks.Text = _singleShotResult.TextViewCalculatedWindClicksText;
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            _singleShotResult = null;
        }

        private class NuberPickerFormatter : Java.Lang.Object, NumberPicker.IFormatter
        {
            private int _factor;

            public NuberPickerFormatter(int factor)
            {
                _factor = factor;
            }

            public string Format(int value)
            {
                var formatedValue = value * _factor;
                return formatedValue.ToString();
            }
        }

        class SingleShotResultHolder
        {
            public int NumberPickerRnageValue { get; set; }
            public int NumberPickerWindValue { get; set; }
            public int NumberPickerDirectionValue { get; set; }
            public string TextViewCalculatedAngleText { get; set; }
            public string TextViewCalculationAngleClicksText { get; set; }
            public string TextViewCalculationWindAngleText { get; set; }
            public string TextViewCalculatedWindClicksText { get; set; }
        }
    }

    
}