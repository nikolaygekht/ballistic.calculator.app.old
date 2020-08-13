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
using Gehtsoft.BallisticCalculator.Views;
using MathEx.ExternalBallistic.Units;
using Gehtsoft.BallisticCalculator.Utils;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : EditActivityBase
    {
        private static SettingsDataHolder _settingsDataHolder;

        protected override bool IsImperial
        {
            get
            {
                return _dataProvider.MeasurementSystem 
                    == MeasurementSystem.Imperial;
            }
        }

        private const string IMPERIAL_UNITS = "Imperial";
        private const string METRIC_UNITS = "Metric";

        private EditTextEx _editBallistTableStep;
        private Button _buttonUnitStep;
        private EditTextEx _editStepSingleShot;
        private Button _buttonUnitStepSingleShot;
          
        private Button _buttonUnits;
        private BallisticDataProvider _dataProvider;
        private MeasurementSystem _measurementSystem;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);

            _dataProvider = BallisticDataProvider.Instance;
            
            if (_settingsDataHolder == null)
            {
                _settingsDataHolder = new SettingsDataHolder();
                _settingsDataHolder.MeasurementSystem = _dataProvider.MeasurementSystem;
                _settingsDataHolder.StepForBallisticTable = _dataProvider.ShotData.StepForBallisticTable;
                _settingsDataHolder.StepForSingleShot = _dataProvider.ShotData.StepForSingleShot;
            }
                        
            CreateControls();
            FillControlsFromSettingsData(_settingsDataHolder);
            Init();
        }

        private void FillControlsFromSettingsData(SettingsDataHolder dataHolder)
        {
            if (dataHolder == null)
                return;

            _measurementSystem = dataHolder.MeasurementSystem;

            _buttonUnits.Text = _measurementSystem
                == MeasurementSystem.Imperial ? IMPERIAL_UNITS : METRIC_UNITS;

            Distance btStep = dataHolder.StepForBallisticTable;
            Distance ssStep = dataHolder.StepForSingleShot;

            if (!IsImperial &&
             (
                 btStep.SetUnit == Distance.Unit.Inch ||
                 btStep.SetUnit == Distance.Unit.Foot ||
                 btStep.SetUnit == Distance.Unit.Yard ||
                 btStep.SetUnit == Distance.Unit.Mile
             ) ||
             IsImperial &&
             (
                 btStep.SetUnit == Distance.Unit.Millimeter ||
                 btStep.SetUnit == Distance.Unit.Centimeter ||
                 btStep.SetUnit == Distance.Unit.Meter ||
                 btStep.SetUnit == Distance.Unit.Kilometer
             ))
            {
                _buttonUnitStep.Text = Distance.UnitToName(DefaultUnits.Range);
            }
            else
            {
                _buttonUnitStep.Text = Distance.UnitToName(btStep.SetUnit);
            }

            IUnitsAdapter btsAdapter = new DistanceAdapter(btStep);
            btsAdapter.ChangeUnit(_buttonUnitStep.Text);
            _editBallistTableStep.UnitsAdapter = btsAdapter;

            if (!IsImperial &&
             (
                 ssStep.SetUnit == Distance.Unit.Inch ||
                 ssStep.SetUnit == Distance.Unit.Foot ||
                 ssStep.SetUnit == Distance.Unit.Yard ||
                 ssStep.SetUnit == Distance.Unit.Mile
             ) ||
             IsImperial &&
             (
                 ssStep.SetUnit == Distance.Unit.Millimeter ||
                 ssStep.SetUnit == Distance.Unit.Centimeter ||
                 ssStep.SetUnit == Distance.Unit.Meter ||
                 ssStep.SetUnit == Distance.Unit.Kilometer
             ))
            {
                _buttonUnitStepSingleShot.Text = Distance.UnitToName(DefaultUnits.Range);
            }
            else
            {
                _buttonUnitStepSingleShot.Text = Distance.UnitToName(ssStep.SetUnit);
            }

            IUnitsAdapter sstSdapter = new DistanceAdapter(ssStep);
            sstSdapter.ChangeUnit(_buttonUnitStepSingleShot.Text);
            _editStepSingleShot.UnitsAdapter = sstSdapter;
        }

        private void FillSettingsDataFromControls(SettingsDataHolder dataHolder)
        {
            if (dataHolder == null)
                return;

            dataHolder.MeasurementSystem = _measurementSystem;
            dataHolder.StepForBallisticTable =
                new Distance(_editBallistTableStep.UnitsAdapter.CurrentValue(),
                Distance.NameToUnit(_editBallistTableStep.UnitsAdapter.CurrentUnit())
                );
            dataHolder.StepForSingleShot =
                new Distance(_editStepSingleShot.UnitsAdapter.CurrentValue(),
                Distance.NameToUnit(_editStepSingleShot.UnitsAdapter.CurrentUnit())
                );
        }

        private void CreateControls()
        {
            _editBallistTableStep = FindViewById<EditTextEx>(Resource.Id.editStep);
            _buttonUnitStep = FindViewById<Button>(Resource.Id.buttonStepUnits);
            BindButton(_buttonUnitStep, _editBallistTableStep);

            _editStepSingleShot = FindViewById<EditTextEx>(Resource.Id.editStepSingleShot);
            _buttonUnitStepSingleShot = FindViewById<Button>(Resource.Id.buttonStepSingleShotUnits);
            BindButton(_buttonUnitStepSingleShot, _editStepSingleShot);

            _buttonUnits = FindViewById<Button>(Resource.Id.buttonUnits);
            _buttonUnits.Click += buttonUnits_Click;
        }

        void buttonUnits_Click(object sender, EventArgs e)
        {
            if (_buttonUnits.Text == IMPERIAL_UNITS)
            {
                _measurementSystem = MeasurementSystem.Metric;
                _buttonUnits.Text = METRIC_UNITS;
            }
            else
            {
                _measurementSystem = MeasurementSystem.Imperial;
                _buttonUnits.Text = IMPERIAL_UNITS;
            }

            var unit = Distance.Unit.Meter;

            if (_measurementSystem == MeasurementSystem.Imperial)
            {
                unit = Distance.Unit.Yard;
            }

            double value = _editBallistTableStep.UnitsAdapter.Get(Distance.UnitToName(unit));

            IUnitsAdapter adapter = new DistanceAdapter(
                new Distance(value, unit));
            _buttonUnitStep.Text = Distance.UnitToName(unit);
            _editBallistTableStep.UnitsAdapter = adapter;

            value = _editStepSingleShot.UnitsAdapter.Get(Distance.UnitToName(unit));

            IUnitsAdapter adapterSingleShot = new DistanceAdapter(
                new Distance(value, unit));
            _buttonUnitStepSingleShot.Text = Distance.UnitToName(unit);
            _editStepSingleShot.UnitsAdapter = adapterSingleShot;
        }

        override protected void OnSaveButtonClick(object sender, EventArgs e)
        {
            FillSettingsDataFromControls(_settingsDataHolder);
            _dataProvider.MeasurementSystem = _settingsDataHolder.MeasurementSystem;
            _dataProvider.ShotData.StepForBallisticTable = _settingsDataHolder.StepForBallisticTable;
            _dataProvider.ShotData.StepForSingleShot = _settingsDataHolder.StepForSingleShot;
            _settingsDataHolder = null;

            SetResult(Result.Ok, new Intent());
            Finish();
        }
        override protected void OnCancelButtonClick(object sender, EventArgs e)
        {
            _settingsDataHolder = null;

            SetResult(Result.Canceled, new Intent());
            Finish();
        }

        public override void OnBackPressed()
        {
            OnSaveButtonClick(this, new EventArgs());
        }

        protected override void OnPause()
        {
            base.OnPause();
            FillSettingsDataFromControls(_settingsDataHolder);
        }

        protected override void OnResume()
        {
            base.OnResume();
            FillControlsFromSettingsData(_settingsDataHolder);
        }

        override protected IEnumerable<Button> GetAngleButtons()
        {
            return new List<Button>();
        }

        override protected IEnumerable<Button> GetDistanceAllUnitButtons()
        {
            var list = new List<Button>();
            list.Add(_buttonUnitStep);
            list.Add(_buttonUnitStepSingleShot);
            return list;
        }
        override protected IEnumerable<Button> GetDistanceButtons()
        {
            return new List<Button>();
        }
        override protected IEnumerable<Button> GetEnergyButtons()
        {
            return new List<Button>();
        }
        override protected IEnumerable<Button> GetPressureButtons()
        {
            return new List<Button>();

        }
        override protected IEnumerable<Button> GetTemperatureButtons()
        {
            return new List<Button>();
        }
        override protected IEnumerable<Button> GetVelocityButtons()
        {
            return new List<Button>();
        }
        override protected IEnumerable<Button> GetWeightButtons()
        {
            return new List<Button>();
        }

        class SettingsDataHolder
        {
            public MeasurementSystem MeasurementSystem { get; set; }
            public Distance StepForBallisticTable { get; set; }
            public Distance StepForSingleShot { get; set; }
        }
    }
}