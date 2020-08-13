using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Gehtsoft.BallisticCalculator.Model;
using Gehtsoft.BallisticCalculator.Views;
using MathEx.ExternalBallistic.Units;
using System;
using System.Collections.Generic;

namespace Gehtsoft.BallisticCalculator.Activities
{
    abstract public class EditActivityBase : Activity
    {
        Button cancelButton;
        Button saveButton;
        Dictionary<Button, EditTextEx> _buttonBindings = new Dictionary<Button, EditTextEx>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        abstract protected bool IsImperial{ get;  }
            
        public void Init()
        {
            foreach (Button button in GetAngleButtons())
                button.Click += buttonAngleUnits_Click;
            foreach (Button button in GetDistanceButtons())
                button.Click += buttonDistanceUnits_Click;
            foreach (Button button in GetEnergyButtons())
                button.Click += buttonEnergyUnits_Click;
            foreach (Button button in GetPressureButtons())
                button.Click += buttonPressureUnits_Click;
            foreach (Button button in GetTemperatureButtons())
                button.Click += buttonTemperatureUnits_Click;
            foreach (Button button in GetVelocityButtons())
                button.Click += buttonVelocityUnits_Click;
            foreach (Button button in GetWeightButtons())
                button.Click += buttonWeightUnits_Click;
            foreach (Button button in GetDistanceAllUnitButtons())
                button.Click += buttonDistanceCustomSystem_Click;

            cancelButton = FindViewById<Button>(Resource.Id.buttonCancel);
            saveButton = FindViewById<Button>(Resource.Id.buttonSave);

            cancelButton.Click += OnCancelButtonClick;
            saveButton.Click += OnSaveButtonClick;
        }

        protected void BindButton(Button button, EditTextEx editText)
        {
            _buttonBindings.Add(button, editText);
        }

        private void OnButtonUnitsUpdate(Button button)
        {
            EditTextEx editText = null;
            if (_buttonBindings.TryGetValue(button, out editText))
            {
                editText.UpdateUnits(button.Text);
            }
        }

        abstract protected void OnSaveButtonClick(object sender, EventArgs e);
        abstract protected void OnCancelButtonClick(object sender, EventArgs e);

        abstract protected IEnumerable<Button> GetAngleButtons();
        abstract protected IEnumerable<Button> GetDistanceButtons();
        abstract protected IEnumerable<Button> GetEnergyButtons();
        abstract protected IEnumerable<Button> GetPressureButtons();
        abstract protected IEnumerable<Button> GetTemperatureButtons();
        abstract protected IEnumerable<Button> GetVelocityButtons();
        abstract protected IEnumerable<Button> GetWeightButtons();

        virtual protected IEnumerable<Button> GetDistanceAllUnitButtons()
        {
            return new List<Button>();
        }

        private EventHandler<DialogClickEventArgs> UnitsButtonHandlerBinder(Action<object, DialogClickEventArgs, Button> handler, Button button)
        {
            return (object sender, DialogClickEventArgs e) => handler(sender, e, button);
        }

        #region AngleUnits
        void buttonAngleUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Angle_imperial, UnitsButtonHandlerBinder(AngleUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Angle_metric, UnitsButtonHandlerBinder(AngleUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void AngleUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] angleUnits = null;
            if (IsImperial)
                angleUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Angle_imperial);
            else
                angleUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Angle_metric);

            string buttonText = angleUnits[e.Which].Split('(', ')')[1];
            try
            {
                Angle.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion

        #region DistanceUnits
        void buttonDistanceUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Distance_imperial, UnitsButtonHandlerBinder(DistanceUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Distance_metric, UnitsButtonHandlerBinder(DistanceUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void DistanceUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] distanceUnits = null;
            if (IsImperial)
                distanceUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Distance_imperial);
            else
                distanceUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Distance_metric);

            string buttonText = distanceUnits[e.Which].Split('(', ')')[1];
            try
            {
                Distance.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion



        #region DistanceUnitsCustomSystem

        void buttonDistanceCustomSystem_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MeasurementSystem measurSystem = MeasurementSystem.Metric;
            if (button.Text == "in" || button.Text == "ft" || button.Text == "yd" || button.Text == "mi")
                measurSystem = MeasurementSystem.Imperial;

            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (measurSystem == MeasurementSystem.Imperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Distance_imperial, UnitsButtonHandlerBinder(DistanceUnitsCustomSystem_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Distance_metric, UnitsButtonHandlerBinder(DistanceUnitsCustomSystem_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void DistanceUnitsCustomSystem_Click(object sender, DialogClickEventArgs e, Button button)
        {
            MeasurementSystem measurSystem = MeasurementSystem.Metric;
            if (button.Text == "in" || button.Text == "ft" || button.Text == "yd" || button.Text == "mi")
                measurSystem = MeasurementSystem.Imperial;

            string[] distanceUnits = null;
            if (measurSystem == MeasurementSystem.Imperial)
                distanceUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Distance_imperial);
            else
                distanceUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Distance_metric);

            string buttonText = distanceUnits[e.Which].Split('(', ')')[1];
            try
            {
                Distance.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion


        #region EnergyUnits
        void buttonEnergyUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Energy_imperial, UnitsButtonHandlerBinder(EnergyUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Energy_metric, UnitsButtonHandlerBinder(EnergyUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void EnergyUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] energyUnits = null;
            if (IsImperial)
                energyUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Energy_imperial);
            else
                energyUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Energy_metric);

            string buttonText = energyUnits[e.Which].Split('(', ')')[1];
            try
            {
                Energy.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion

        #region PressureUnits
        void buttonPressureUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Pressure_imperial, UnitsButtonHandlerBinder(PressureUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Pressure_metric, UnitsButtonHandlerBinder(PressureUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void PressureUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] pressureUnits = null;
            if (IsImperial)
                pressureUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Pressure_imperial);
            else
                pressureUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Pressure_metric);

            string buttonText = pressureUnits[e.Which].Split('(', ')')[1];
            try
            {
                Pressure.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion

        #region TemperatureUnits
        void buttonTemperatureUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Temperature_imperial, UnitsButtonHandlerBinder(TemperatureUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Temperature_metric, UnitsButtonHandlerBinder(TemperatureUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void TemperatureUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] temperatureUnits = null;
            if (IsImperial)
                temperatureUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Temperature_imperial);
            else
                temperatureUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Temperature_metric);

            string buttonText = temperatureUnits[e.Which].Split('(', ')')[1];
            try
            {
                Temperature.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion

        #region VelocityUnits
        void buttonVelocityUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Velocity_imperial, UnitsButtonHandlerBinder(VelocityUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Velocity_metric, UnitsButtonHandlerBinder(VelocityUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void VelocityUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] velocityUnits = null;
            if (IsImperial)
                velocityUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Velocity_imperial);
            else
                velocityUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Velocity_metric);

            string buttonText = velocityUnits[e.Which].Split('(', ')')[1];
            try
            {
                Velocity.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion

        #region WeightUnits
        void buttonWeightUnits_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(Resource.String.dd_prompt_Units);

            if (IsImperial)
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Weight_imperial, UnitsButtonHandlerBinder(WeightUnits_Click, (Button)sender));
            else
                alertDialogBuilder.SetItems(Resource.Array.dd_lbls_Weight_metric, UnitsButtonHandlerBinder(WeightUnits_Click, (Button)sender));
            alertDialogBuilder.Show();
        }

        private void WeightUnits_Click(object sender, DialogClickEventArgs e, Button button)
        {
            string[] weightUnits = null;
            if (IsImperial)
                weightUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Weight_imperial);
            else
                weightUnits = Resources.GetStringArray(Resource.Array.dd_lbls_Weight_metric);

            string buttonText = weightUnits[e.Which].Split('(', ')')[1];
            try
            {
                Weight.NameToUnit(buttonText);
            }
            catch (ArgumentException)
            {
                return;
            }

            button.Text = buttonText;
            OnButtonUnitsUpdate(button);
        }
        #endregion
    }
}