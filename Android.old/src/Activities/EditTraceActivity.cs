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
using MathEx.ExternalBallistic.Units;
using BallisticCalculator.Utils;
using System.Globalization;
using BallisticCalculator.Views;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Edit Trace")]
    public class EditTraceActivity : EditActivityBase
    {
        private TraceInfo _traceInfo;

        EditText editTraceName;

        EditTextEx editBulletWeight;
        Button buttonBulletWeightUnits;

        EditTextEx editMuzzleVelocity;
        Button buttonMuzzleVelocityUnits;

        EditText editBallisticCoefficient;
        Button buttonDragTable;

        EditTextEx editZeroDistance;
        Button buttonZeroDistanceUnits;

        EditTextEx editSightHeight;
        Button buttonSightHeightUnits;

        CheckBox checkBoxCalculateSpinDrift;

        EditTextEx editBulletDiameter;
        Button buttonBulletDiameterUnits;

        EditTextEx editBulletLength;
        Button buttonBulletLengthUnits;

        EditTextEx editRifling;
        Button buttonRiflingUnits;

        RadioGroup radioButtonGroupRiflingHand;

        RadioButton radioButtonRiflingLeftHand;
        RadioButton radioButtonRiflingRightHand;

        EditTextEx editVerticalClick;
        Button buttonVerticalClickUnits;

        EditTextEx editHorizontalClick;
        Button buttonHorizontalClickUnits;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EditTrace);

            InitControls();
            InitUnits();

            int selectedTraceID = Intent.GetIntExtra("SelectedTraceID", -1);
            if (selectedTraceID >= 0)
            {
                _traceInfo = ApplicationData.Instance.TraceInfoCollection[selectedTraceID];
                Title = String.Format("{0} ({1})", Resources.GetString(Resource.String.lbl_EditTraceActivity), _traceInfo.TraceName);
            }
            else
            {
                _traceInfo = new TraceInfo();
                _traceInfo.Metric = ApplicationData.Instance.PrefferedUnits == ApplicationData.EPrefferedUnits.Metric;

                _traceInfo.BallisticCoefficient = 0.5;
                _traceInfo.BulletWeight = new Weight(0, Weight.DefaultUnit);
                _traceInfo.MuzzleVelocity = new Velocity(0, Velocity.DefaultUnit);

                _traceInfo.SightHeight = _traceInfo.Metric ? new Distance(1.5, Distance.Unit.Inch) : new Distance(5, Distance.Unit.Centimeter);
                _traceInfo.ZeroDistance = new Distance(100, DefaultUnits.Zero.Distance);
                _traceInfo.ZeroElevationAngle = new Angle(0, Angle.DefaultUnit);

                _traceInfo.DriftInfo = false;
                _traceInfo.BulletLength = new Distance(0, Distance.DefaultUnit);
                _traceInfo.BulletDiameter = new Distance(0, Distance.DefaultUnit);
                _traceInfo.RiflingTwist = new Distance(0, Distance.DefaultUnit);
                _traceInfo.RiflingRightHandTwist = true;

                _traceInfo.VerticalClick = new Angle(0.25, Angle.Unit.Moa);
                _traceInfo.HorizonalClick = new Angle(0.25, Angle.Unit.Moa);
            }

            fillDataFromTraceInfo(_traceInfo);

            initControlsVisibility();

            Init();
            buttonDragTable.Click += buttonDragTable_Click;

            checkBoxCalculateSpinDrift.Click += checkBoxCalculateSpinDrift_Click;
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
            string[] dragTable = null;
            dragTable = Resources.GetStringArray(Resource.Array.dd_lbls_Drag_Table);
            buttonDragTable.Text = dragTable[e.Which];
        }

        private void checkBoxCalculateSpinDrift_Click(object sender, EventArgs e)
        {
            initControlsVisibility();
        }

        private void InitControls()
        {
            editTraceName = FindViewById<EditText>(Resource.Id.editTextTraceName);

            editBulletWeight = FindViewById<EditTextEx>(Resource.Id.editTextBulletWeight);
            buttonBulletWeightUnits = FindViewById<Button>(Resource.Id.buttonBulletWeightUnits);
            BindButton(buttonBulletWeightUnits, editBulletWeight);

            editMuzzleVelocity = FindViewById<EditTextEx>(Resource.Id.editTextMuzzleVelocity);
            buttonMuzzleVelocityUnits = FindViewById<Button>(Resource.Id.buttonMuzzleVelocityUnits);
            BindButton(buttonMuzzleVelocityUnits, editMuzzleVelocity);

            editBallisticCoefficient = FindViewById<EditText>(Resource.Id.editTextBallisticCoefficient);
            buttonDragTable = FindViewById<Button>(Resource.Id.buttonDragTable);

            editZeroDistance = FindViewById<EditTextEx>(Resource.Id.editTextZeroDistance);
            buttonZeroDistanceUnits = FindViewById<Button>(Resource.Id.buttonZeroDistanceUnits);
            BindButton(buttonZeroDistanceUnits, editZeroDistance);

            editSightHeight = FindViewById<EditTextEx>(Resource.Id.editTextZeroHeight);
            buttonSightHeightUnits = FindViewById<Button>(Resource.Id.buttonZeroHeightUnits);
            BindButton(buttonSightHeightUnits, editSightHeight);

            checkBoxCalculateSpinDrift = FindViewById<CheckBox>(Resource.Id.checkBoxCalculateSpinDrift);

            editBulletDiameter = FindViewById<EditTextEx>(Resource.Id.editTextBulletDiameter);
            buttonBulletDiameterUnits = FindViewById<Button>(Resource.Id.buttonBulletDiameterUnits);
            BindButton(buttonBulletDiameterUnits, editBulletDiameter);

            editBulletLength = FindViewById<EditTextEx>(Resource.Id.editTextBulletLength);
            buttonBulletLengthUnits = FindViewById<Button>(Resource.Id.buttonBulletLengthUnits);
            BindButton(buttonBulletLengthUnits, editBulletLength);

            editRifling = FindViewById<EditTextEx>(Resource.Id.editTextRifling);
            buttonRiflingUnits = FindViewById<Button>(Resource.Id.buttonRiflingUnits);
            BindButton(buttonRiflingUnits, editRifling);

            radioButtonGroupRiflingHand = FindViewById<RadioGroup>(Resource.Id.radioGroupRiflingHand);

            radioButtonRiflingLeftHand = FindViewById<RadioButton>(Resource.Id.radioButtonRiflingHandLeft);
            radioButtonRiflingRightHand = FindViewById<RadioButton>(Resource.Id.radioButtonRiflingHandRight);

            editVerticalClick = FindViewById<EditTextEx>(Resource.Id.editTextVerticalClick);
            buttonVerticalClickUnits = FindViewById<Button>(Resource.Id.buttonVerticalClickUnits);
            BindButton(buttonVerticalClickUnits, editVerticalClick);

            editHorizontalClick = FindViewById<EditTextEx>(Resource.Id.editTextHorizontalClick);
            buttonHorizontalClickUnits = FindViewById<Button>(Resource.Id.buttonHorizontalClickUnits);
            BindButton(buttonHorizontalClickUnits, editHorizontalClick);
        }

        private void InitUnits()
        {
            buttonBulletWeightUnits.Text = Weight.UnitToName(DefaultUnits.Bullet.Weight);
            buttonMuzzleVelocityUnits.Text = Velocity.UnitToName(DefaultUnits.Bullet.Velocity);
            buttonZeroDistanceUnits.Text = Distance.UnitToName(DefaultUnits.Zero.Distance);
            buttonSightHeightUnits.Text = Distance.UnitToName(DefaultUnits.Zero.Sight);
            buttonBulletDiameterUnits.Text = Distance.UnitToName(DefaultUnits.Bullet.Size);
            buttonBulletLengthUnits.Text = Distance.UnitToName(DefaultUnits.Bullet.Size);
            buttonRiflingUnits.Text = Distance.UnitToName(DefaultUnits.Bullet.Size);
            buttonVerticalClickUnits.Text = Angle.UnitToName(DefaultUnits.Reticle.Adjustment);
            buttonHorizontalClickUnits.Text = Angle.UnitToName(DefaultUnits.Reticle.Adjustment);
        }

        protected override void OnCancelButtonClick(object sender, EventArgs e)
        {
            SetResult(Result.Canceled);
            Finish();
        }

        protected override void OnSaveButtonClick(object sender, EventArgs e)
        {
            int index = ApplicationData.Instance.TraceInfoCollection.Find(_traceInfo.TraceName);

            fillTraceInfoFromData(ref _traceInfo);

            ApplicationData.Instance.SelectedTraceInfo = _traceInfo;
            if (index < 0)
            {
                ApplicationData.Instance.TraceInfoCollection.Add(_traceInfo);
            }
            else
            {
                /* TraceInfoCollection cannot be assigned directly */
                ApplicationData.Instance.TraceInfoCollection.RemoveAt(index);
                ApplicationData.Instance.TraceInfoCollection.Add(_traceInfo);
            }

            SetResult(Result.Ok);
            Finish();
        }

        private void initControlsVisibility()
        {
            ViewStates viewState = checkBoxCalculateSpinDrift.Checked ? ViewStates.Visible : ViewStates.Gone;

            editBulletDiameter.Visibility = viewState;
            buttonBulletDiameterUnits.Visibility = viewState;
            editBulletLength.Visibility = viewState;
            buttonBulletLengthUnits.Visibility = viewState;
            editRifling.Visibility = viewState;
            buttonRiflingUnits.Visibility = viewState;
            radioButtonGroupRiflingHand.Visibility = viewState;

            FindViewById<TextView>(Resource.Id.textViewBulletDiameter).Visibility = viewState;
            FindViewById<TextView>(Resource.Id.textViewBulletLength).Visibility = viewState;
            FindViewById<TextView>(Resource.Id.textViewRifling).Visibility = viewState;
        }

        private void fillDataFromTraceInfo(TraceInfo traceInfo)
        {
            if (traceInfo != null)
            {
                if (traceInfo.TraceName != null)
                {
                    editTraceName.Text = traceInfo.TraceName;
                }

                if (traceInfo.BulletWeight != null)
                {
                    buttonBulletWeightUnits.Text = Weight.UnitToName(traceInfo.BulletWeight.SetUnit);
                    IUnitsAdapter adapter = new WeightAdapter(traceInfo.BulletWeight);
                    adapter.ChangeUnit(buttonBulletWeightUnits.Text);
                    editBulletWeight.UnitsAdapter = adapter;
                }

                if (traceInfo.MuzzleVelocity != null)
                {
                    buttonMuzzleVelocityUnits.Text = Velocity.UnitToName(traceInfo.MuzzleVelocity.SetUnit);
                    IUnitsAdapter adapter = new VelocityAdapter(traceInfo.MuzzleVelocity);
                    adapter.ChangeUnit(buttonMuzzleVelocityUnits.Text);
                    editMuzzleVelocity.UnitsAdapter = adapter;
                }

                editBallisticCoefficient.Text = traceInfo.BallisticCoefficient.ToString();

                buttonDragTable.Text = Utilities.DragTableToString(traceInfo.DrageTable);

                if (traceInfo.ZeroDistance != null)
                {
                    buttonZeroDistanceUnits.Text = Distance.UnitToName(traceInfo.ZeroDistance.SetUnit);
                    IUnitsAdapter adapter = new DistanceAdapter(traceInfo.ZeroDistance);
                    adapter.ChangeUnit(buttonZeroDistanceUnits.Text);
                    editZeroDistance.UnitsAdapter = adapter;
                }

                if (traceInfo.SightHeight != null)
                {
                    buttonSightHeightUnits.Text = Distance.UnitToName(traceInfo.SightHeight.SetUnit);
                    IUnitsAdapter adapter = new DistanceAdapter(traceInfo.SightHeight);
                    adapter.ChangeUnit(buttonSightHeightUnits.Text);
                    editSightHeight.UnitsAdapter = adapter;
                }

                checkBoxCalculateSpinDrift.Checked = traceInfo.DriftInfo;


                if (traceInfo.DriftInfo)
                {
                    if (traceInfo.BulletDiameter != null)
                    {
                        buttonBulletDiameterUnits.Text = Distance.UnitToName(traceInfo.BulletDiameter.SetUnit);
                        IUnitsAdapter adapter = new DistanceAdapter(traceInfo.BulletDiameter);
                        adapter.ChangeUnit(buttonBulletDiameterUnits.Text);
                        editBulletDiameter.UnitsAdapter = adapter;
                    }

                    if (traceInfo.BulletLength != null)
                    {
                        buttonBulletLengthUnits.Text = Distance.UnitToName(traceInfo.BulletLength.SetUnit);
                        IUnitsAdapter adapter = new DistanceAdapter(traceInfo.BulletLength);
                        adapter.ChangeUnit(buttonBulletLengthUnits.Text);
                        editBulletLength.UnitsAdapter = adapter;
                    }

                    if (traceInfo.RiflingTwist != null)
                    {
                        buttonRiflingUnits.Text = Distance.UnitToName(traceInfo.RiflingTwist.SetUnit);
                        IUnitsAdapter adapter = new DistanceAdapter(traceInfo.RiflingTwist);
                        adapter.ChangeUnit(buttonRiflingUnits.Text);
                        editRifling.UnitsAdapter = adapter;
                    }

                    if (traceInfo.RiflingRightHandTwist)
                        radioButtonRiflingRightHand.Checked = true;
                    else
                        radioButtonRiflingLeftHand.Checked = true;
                }

                if (traceInfo.VerticalClick != null)
                {
                    buttonVerticalClickUnits.Text = Angle.UnitToName(traceInfo.VerticalClick.SetUnit);
                    IUnitsAdapter adapter = new AngleAdapter(traceInfo.VerticalClick);
                    adapter.ChangeUnit(buttonVerticalClickUnits.Text);
                    editVerticalClick.UnitsAdapter = adapter;
                }

                if (traceInfo.HorizonalClick != null)
                {
                    buttonHorizontalClickUnits.Text = Angle.UnitToName(traceInfo.HorizonalClick.SetUnit);
                    IUnitsAdapter adapter = new AngleAdapter(traceInfo.HorizonalClick);
                    adapter.ChangeUnit(buttonHorizontalClickUnits.Text);
                    editHorizontalClick.UnitsAdapter = adapter;
                }
            }
        }

        void fillTraceInfoFromData(ref TraceInfo traceInfo)
        {
            traceInfo.TraceName = editTraceName.Text;
            traceInfo.BulletWeight = new Weight(editBulletWeight.UnitsAdapter.CurrentValue(), Weight.NameToUnit(editBulletWeight.UnitsAdapter.CurrentUnit()));
            traceInfo.MuzzleVelocity = new Velocity(editMuzzleVelocity.UnitsAdapter.CurrentValue(), Velocity.NameToUnit(editMuzzleVelocity.UnitsAdapter.CurrentUnit()));

            double ballisticCoefficient;
            if (Utilities.TryParseDouble(editBallisticCoefficient.Text, out ballisticCoefficient))
                traceInfo.BallisticCoefficient = ballisticCoefficient;

            traceInfo.DrageTable = Utilities.DragTableFromString(buttonDragTable.Text);

            traceInfo.ZeroDistance = new Distance(editZeroDistance.UnitsAdapter.CurrentValue(), Distance.NameToUnit(editZeroDistance.UnitsAdapter.CurrentUnit()));
            traceInfo.SightHeight = new Distance(editSightHeight.UnitsAdapter.CurrentValue(), Distance.NameToUnit(editSightHeight.UnitsAdapter.CurrentUnit()));

            traceInfo.DriftInfo = checkBoxCalculateSpinDrift.Checked;

            if (traceInfo.DriftInfo)
            {
                traceInfo.BulletDiameter = new Distance(editBulletDiameter.UnitsAdapter.CurrentValue(), Distance.NameToUnit(editBulletDiameter.UnitsAdapter.CurrentUnit()));
                traceInfo.BulletLength = new Distance(editBulletLength.UnitsAdapter.CurrentValue(), Distance.NameToUnit(editBulletLength.UnitsAdapter.CurrentUnit()));
                traceInfo.RiflingTwist = new Distance(editRifling.UnitsAdapter.CurrentValue(), Distance.NameToUnit(editRifling.UnitsAdapter.CurrentUnit()));

                if (radioButtonRiflingRightHand.Checked)
                    traceInfo.RiflingRightHandTwist = true;
                else
                    traceInfo.RiflingRightHandTwist = false;
            }

            traceInfo.VerticalClick = new Angle(editVerticalClick.UnitsAdapter.CurrentValue(), Angle.NameToUnit(editVerticalClick.UnitsAdapter.CurrentUnit()));
            traceInfo.HorizonalClick = new Angle(editHorizontalClick.UnitsAdapter.CurrentValue(), Angle.NameToUnit(editHorizontalClick.UnitsAdapter.CurrentUnit()));
        }

        protected override IEnumerable<Button> GetAngleButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(buttonVerticalClickUnits);
            buttons.Add(buttonHorizontalClickUnits);

            return buttons;
        }

        protected override IEnumerable<Button> GetDistanceButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(buttonZeroDistanceUnits);
            buttons.Add(buttonSightHeightUnits);
            buttons.Add(buttonBulletDiameterUnits);
            buttons.Add(buttonBulletLengthUnits);
            buttons.Add(buttonRiflingUnits);

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

            buttons.Add(buttonMuzzleVelocityUnits);

            return buttons;
        }

        protected override IEnumerable<Button> GetWeightButtons()
        {
            List<Button> buttons = new List<Button>();

            buttons.Add(buttonBulletWeightUnits);

            return buttons;
        }
    }
}