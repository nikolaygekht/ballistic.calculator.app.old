using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Gehtsoft.BallisticCalculator.Connectivity;
using Gehtsoft.BallisticCalculator.DataProviders;
using Gehtsoft.BallisticCalculator.Model;
using Gehtsoft.BallisticCalculator.Utils;
using Gehtsoft.BallisticCalculator.Views;
using MathEx.ExternalBallistic.Units;
using System;
using System.Collections.Generic;

namespace Gehtsoft.BallisticCalculator.Activities
{
    [Activity(Label = "Edit Trace")]
    public class EditTraceActivity : EditActivityBase
    {
        protected override bool IsImperial
        {
            get
            {
                return _dataProvider.MeasurementSystem
                    == MeasurementSystem.Imperial;
            }
        }

        private string _applicationName;
        private BallisticDataProvider _dataProvider;
        private static TraceInfo _traceInfo;

        private EditText _editTraceName;
        private EditTextEx _editMuzzleVelocity;
        private EditTextEx _editBulletWeight;
        private EditText _editBallisticCoefficient;
        private EditTextEx _editZeroDistance;
        private EditTextEx _editSightHeight;
        private EditTextEx _editBulletDiameter;
        private EditTextEx _editBulletLength;
        private EditTextEx _editRifling;
        private EditTextEx _editHorizontalClick;
        private Button _buttonBulletWeightUnits;
        private Button _buttonMuzzleVelocityUnits;
        private Button _buttonDragTable; 
        private Button _buttonZeroDistanceUnits;     
        private Button _buttonSightHeightUnits;
        private Button _buttonBulletDiameterUnits;
        private Button _buttonBulletLengthUnits;
        private Button _buttonRiflingUnits;
        private RadioGroup _radioButtonGroupRiflingHand;
        private RadioButton _radioButtonRiflingLeftHand;
        private RadioButton _radioButtonRiflingRightHand;
        private EditTextEx _editVerticalClick;
        private Button _buttonVerticalClickUnits;
        private Button _buttonHorizontalClickUnits;
        private CheckBox _checkBoxCalculateSpinDrift;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EditTrace);

            _applicationName = Resources.GetString(Resource.String.lbl_EditTraceActivity);
            _dataProvider = BallisticDataProvider.Instance;
            int selectedTracePosition = Intent.GetIntExtra("SelectedTracePosition", -1);

            if (_traceInfo == null)
            {
                if (selectedTracePosition >= 0)
                {
                    _traceInfo = _dataProvider.GetTraceByIndex(selectedTracePosition);
                    var selectedTraceName = _traceInfo.TraceName;
                    Title = string.Format("{0} ({1})", _applicationName, selectedTraceName);
                }
                else
                {
                    Title = string.Format("{0}", _applicationName);
                    _traceInfo = DefaultValues.CreateTraceInfo();
                }
            }

            CreateControls();
            InitControls(null);
            InitControls(_traceInfo);
            SetSpinDriftControlVisibility();
            Init();
        }

        private void buttonDragTable_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);
            alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Drag_Table, DragTable_Click);
            alertDialogBuilder.Show();
        }

        private void DragTable_Click(object sender, DialogClickEventArgs e)
        {
            string[] dragTable = Resources.GetStringArray(Resource.Array.dd_lbls_Drag_Table);
            _buttonDragTable.Text = dragTable[e.Which];
        }

        private void checkBoxCalculateSpinDrift_Click(object sender, EventArgs e)
        {
            SetSpinDriftControlVisibility();
        }

        private void CreateControls()
        {
            _editTraceName = FindViewById<EditText>(Resource.Id.editTextTraceName);

            _editBulletWeight = FindViewById<EditTextEx>(Resource.Id.editTextBulletWeight);
            _buttonBulletWeightUnits = FindViewById<Button>(Resource.Id.buttonBulletWeightUnits);
            BindButton(_buttonBulletWeightUnits, _editBulletWeight);

            _editMuzzleVelocity = FindViewById<EditTextEx>(Resource.Id.editTextMuzzleVelocity);
            _buttonMuzzleVelocityUnits = FindViewById<Button>(Resource.Id.buttonMuzzleVelocityUnits);
            BindButton(_buttonMuzzleVelocityUnits, _editMuzzleVelocity);

            _editBallisticCoefficient = FindViewById<EditText>(Resource.Id.editTextBallisticCoefficient);
            _buttonDragTable = FindViewById<Button>(Resource.Id.buttonDragTable);
            _buttonDragTable.Click += buttonDragTable_Click;

            _editZeroDistance = FindViewById<EditTextEx>(Resource.Id.editTextZeroDistance);
            _buttonZeroDistanceUnits = FindViewById<Button>(Resource.Id.buttonZeroDistanceUnits);
            BindButton(_buttonZeroDistanceUnits, _editZeroDistance);

            _editSightHeight = FindViewById<EditTextEx>(Resource.Id.editTextZeroHeight);
            _buttonSightHeightUnits = FindViewById<Button>(Resource.Id.buttonZeroHeightUnits);
            BindButton(_buttonSightHeightUnits, _editSightHeight);

            _checkBoxCalculateSpinDrift = FindViewById<CheckBox>(Resource.Id.checkBoxCalculateSpinDrift);
            _checkBoxCalculateSpinDrift.Click += checkBoxCalculateSpinDrift_Click;

            _editBulletDiameter = FindViewById<EditTextEx>(Resource.Id.editTextBulletDiameter);
            _buttonBulletDiameterUnits = FindViewById<Button>(Resource.Id.buttonBulletDiameterUnits);
            BindButton(_buttonBulletDiameterUnits, _editBulletDiameter);

            _editBulletLength = FindViewById<EditTextEx>(Resource.Id.editTextBulletLength);
            _buttonBulletLengthUnits = FindViewById<Button>(Resource.Id.buttonBulletLengthUnits);
            BindButton(_buttonBulletLengthUnits, _editBulletLength);

            _editRifling = FindViewById<EditTextEx>(Resource.Id.editTextRifling);
            _buttonRiflingUnits = FindViewById<Button>(Resource.Id.buttonRiflingUnits);
            BindButton(_buttonRiflingUnits, _editRifling);

            _radioButtonGroupRiflingHand = FindViewById<RadioGroup>(Resource.Id.radioGroupRiflingHand);

            _radioButtonRiflingLeftHand = FindViewById<RadioButton>(Resource.Id.radioButtonRiflingHandLeft);
            _radioButtonRiflingRightHand = FindViewById<RadioButton>(Resource.Id.radioButtonRiflingHandRight);

            _editVerticalClick = FindViewById<EditTextEx>(Resource.Id.editTextVerticalClick);
            _buttonVerticalClickUnits = FindViewById<Button>(Resource.Id.buttonVerticalClickUnits);
            BindButton(_buttonVerticalClickUnits, _editVerticalClick);

            _editHorizontalClick = FindViewById<EditTextEx>(Resource.Id.editTextHorizontalClick);
            _buttonHorizontalClickUnits = FindViewById<Button>(Resource.Id.buttonHorizontalClickUnits);
            BindButton(_buttonHorizontalClickUnits, _editHorizontalClick);
        }

        protected override void OnCancelButtonClick(object sender, EventArgs e)
        {
            _traceInfo = null;
            SetResult(Result.Canceled);
            Finish();
        }

        protected override void OnSaveButtonClick(object sender, EventArgs e)
        {
            fillTraceInfoFromControls(ref _traceInfo);
            _dataProvider.AddNewTraceInfoToCollection(_traceInfo);
            _dataProvider.SetSelectedTraceByName(_traceInfo.TraceName);
            _traceInfo = null;
            SetResult(Result.Ok);
            Finish();
        }

        private void SetSpinDriftControlVisibility()
        {
            ViewStates viewState = _checkBoxCalculateSpinDrift.Checked ? ViewStates.Visible : ViewStates.Gone;

            _editBulletDiameter.Visibility = viewState;
            _buttonBulletDiameterUnits.Visibility = viewState;
            _editBulletLength.Visibility = viewState;
            _buttonBulletLengthUnits.Visibility = viewState;
            _editRifling.Visibility = viewState;
            _buttonRiflingUnits.Visibility = viewState;
            _radioButtonGroupRiflingHand.Visibility = viewState;

            FindViewById<TextView>(Resource.Id.textViewBulletDiameter).Visibility = viewState;
            FindViewById<TextView>(Resource.Id.textViewBulletLength).Visibility = viewState;
            FindViewById<TextView>(Resource.Id.textViewRifling).Visibility = viewState;
        }

        private void InitControls(TraceInfo traceInfo)
        {
            if (traceInfo == null)
                traceInfo = DefaultValues.CreateTraceInfo();

            if (traceInfo.TraceName != null)
            {
                _editTraceName.Text = traceInfo.TraceName;
            }

            if (traceInfo.BulletWeight != null)
            {
                _buttonBulletWeightUnits.Text = Weight.UnitToName(traceInfo.BulletWeight.SetUnit);
                IUnitsAdapter adapterWeight = new WeightAdapter(traceInfo.BulletWeight);
                adapterWeight.ChangeUnit(_buttonBulletWeightUnits.Text);
                _editBulletWeight.UnitsAdapter = adapterWeight;
            }

            if (traceInfo.MuzzleVelocity != null)
            {
                _buttonMuzzleVelocityUnits.Text = Velocity.UnitToName(traceInfo.MuzzleVelocity.SetUnit);
                IUnitsAdapter adapterMuzzVel = new VelocityAdapter(traceInfo.MuzzleVelocity);
                adapterMuzzVel.ChangeUnit(_buttonMuzzleVelocityUnits.Text);
                _editMuzzleVelocity.UnitsAdapter = adapterMuzzVel;
            }

            _editBallisticCoefficient.Text = traceInfo.BallisticCoefficient.ToString();

            _buttonDragTable.Text = Utilities.DragTableToString(traceInfo.DrageTable);

            if (traceInfo.ZeroDistance != null)
            {
                _buttonZeroDistanceUnits.Text = Distance.UnitToName(traceInfo.ZeroDistance.SetUnit);
                IUnitsAdapter adapterZeroDist = new DistanceAdapter(traceInfo.ZeroDistance);
                adapterZeroDist.ChangeUnit(_buttonZeroDistanceUnits.Text);
                _editZeroDistance.UnitsAdapter = adapterZeroDist;
            }

            if (traceInfo.SightHeight != null)
            {
                _buttonSightHeightUnits.Text = Distance.UnitToName(traceInfo.SightHeight.SetUnit);
                IUnitsAdapter adapterSH = new DistanceAdapter(traceInfo.SightHeight);
                adapterSH.ChangeUnit(_buttonSightHeightUnits.Text);
                _editSightHeight.UnitsAdapter = adapterSH;
            }

            _checkBoxCalculateSpinDrift.Checked = traceInfo.DriftInfo;

            if (traceInfo.BulletDiameter != null)
            {
                _buttonBulletDiameterUnits.Text = Distance.UnitToName(traceInfo.BulletDiameter.SetUnit);
                IUnitsAdapter adapterBulletDiam = new DistanceAdapter(traceInfo.BulletDiameter);
                adapterBulletDiam.ChangeUnit(_buttonBulletDiameterUnits.Text);
                _editBulletDiameter.UnitsAdapter = adapterBulletDiam;
            }

            if (traceInfo.BulletLength != null)
            {
                _buttonBulletLengthUnits.Text = Distance.UnitToName(traceInfo.BulletLength.SetUnit);
                IUnitsAdapter adapterBulletLen = new DistanceAdapter(traceInfo.BulletLength);
                adapterBulletLen.ChangeUnit(_buttonBulletLengthUnits.Text);
                _editBulletLength.UnitsAdapter = adapterBulletLen;
            }

            if (traceInfo.RiflingTwist != null)
            {
                _buttonRiflingUnits.Text = Distance.UnitToName(traceInfo.RiflingTwist.SetUnit);
                IUnitsAdapter adapterRiflTwist = new DistanceAdapter(traceInfo.RiflingTwist);
                adapterRiflTwist.ChangeUnit(_buttonRiflingUnits.Text);
                _editRifling.UnitsAdapter = adapterRiflTwist;
            }

            if (traceInfo.RiflingRightHandTwist)
                _radioButtonRiflingRightHand.Checked = true;
            else
                _radioButtonRiflingLeftHand.Checked = true;

            if (traceInfo.VerticalClick != null)
            {
                _buttonVerticalClickUnits.Text = Angle.UnitToName(traceInfo.VerticalClick.SetUnit);
                IUnitsAdapter adapterVClick = new AngleAdapter(traceInfo.VerticalClick);
                adapterVClick.ChangeUnit(_buttonVerticalClickUnits.Text);
                _editVerticalClick.UnitsAdapter = adapterVClick;
            }

            if (traceInfo.HorizonalClick != null)
            {
                _buttonHorizontalClickUnits.Text = Angle.UnitToName(traceInfo.HorizonalClick.SetUnit);
                IUnitsAdapter adapterHClick = new AngleAdapter(traceInfo.HorizonalClick);
                adapterHClick.ChangeUnit(_buttonHorizontalClickUnits.Text);
                _editHorizontalClick.UnitsAdapter = adapterHClick;
            }
        }

        void fillTraceInfoFromControls(ref TraceInfo traceInfo)
        {
            if (traceInfo == null)
                return;

            traceInfo.TraceName = _editTraceName.Text;
            traceInfo.BulletWeight = new Weight(_editBulletWeight.UnitsAdapter.CurrentValue(), Weight.NameToUnit(_editBulletWeight.UnitsAdapter.CurrentUnit()));
            traceInfo.MuzzleVelocity = new Velocity(_editMuzzleVelocity.UnitsAdapter.CurrentValue(), Velocity.NameToUnit(_editMuzzleVelocity.UnitsAdapter.CurrentUnit()));

            double ballisticCoefficient;
            if (Utilities.TryParseDouble(_editBallisticCoefficient.Text, out ballisticCoefficient))
                traceInfo.BallisticCoefficient = ballisticCoefficient;

            traceInfo.DrageTable = Utilities.DragTableFromString(_buttonDragTable.Text);

            traceInfo.ZeroDistance = new Distance(_editZeroDistance.UnitsAdapter.CurrentValue(), Distance.NameToUnit(_editZeroDistance.UnitsAdapter.CurrentUnit()));
            traceInfo.SightHeight = new Distance(_editSightHeight.UnitsAdapter.CurrentValue(), Distance.NameToUnit(_editSightHeight.UnitsAdapter.CurrentUnit()));

            traceInfo.DriftInfo = _checkBoxCalculateSpinDrift.Checked;

            if (traceInfo.DriftInfo)
            {
                traceInfo.BulletDiameter = new Distance(_editBulletDiameter.UnitsAdapter.CurrentValue(), Distance.NameToUnit(_editBulletDiameter.UnitsAdapter.CurrentUnit()));
                traceInfo.BulletLength = new Distance(_editBulletLength.UnitsAdapter.CurrentValue(), Distance.NameToUnit(_editBulletLength.UnitsAdapter.CurrentUnit()));
                traceInfo.RiflingTwist = new Distance(_editRifling.UnitsAdapter.CurrentValue(), Distance.NameToUnit(_editRifling.UnitsAdapter.CurrentUnit()));

                if (_radioButtonRiflingRightHand.Checked)
                    traceInfo.RiflingRightHandTwist = true;
                else
                    traceInfo.RiflingRightHandTwist = false;
            }

            traceInfo.VerticalClick = new Angle(_editVerticalClick.UnitsAdapter.CurrentValue(), Angle.NameToUnit(_editVerticalClick.UnitsAdapter.CurrentUnit()));
            traceInfo.HorizonalClick = new Angle(_editHorizontalClick.UnitsAdapter.CurrentValue(), Angle.NameToUnit(_editHorizontalClick.UnitsAdapter.CurrentUnit()));
        }

        protected override IEnumerable<Button> GetAngleButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(_buttonVerticalClickUnits);
            buttons.Add(_buttonHorizontalClickUnits);

            return buttons;
        }

        protected override IEnumerable<Button> GetDistanceButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(_buttonZeroDistanceUnits);
            buttons.Add(_buttonSightHeightUnits);
            buttons.Add(_buttonBulletDiameterUnits);
            buttons.Add(_buttonBulletLengthUnits);
            buttons.Add(_buttonRiflingUnits);

            return buttons;
        }

        protected override IEnumerable<Button> GetEnergyButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetPressureButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetTemperatureButtons()
        {
            return new List<Button>();
        }

        protected override IEnumerable<Button> GetVelocityButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(_buttonMuzzleVelocityUnits);

            return buttons;
        }

        protected override IEnumerable<Button> GetWeightButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(_buttonBulletWeightUnits);

            return buttons;
        }

        public override void OnBackPressed()
        {
            OnSaveButtonClick(this, new EventArgs());
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (_traceInfo != null)
                InitControls(_traceInfo);
        }

        protected override void OnPause()
        {
            base.OnPause();
            fillTraceInfoFromControls(ref _traceInfo);
        }
    }
}