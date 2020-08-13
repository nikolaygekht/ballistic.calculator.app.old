using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;

namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class ZeroInfoInput : UserControl, IMeasurementSystemListener
    {
        public ZeroInfoInput()
        {
            InitializeComponent();
            ZeroPresetController.FillComboBox(comboBoxPreset);
            bulletInfoInput.AdvancedMode = false;

            //prevent resizing under wine
            distanceSight.Width = comboBoxPreset.Width;
            distanceSight.LockSize(true);
            distanceZero.Width = comboBoxPreset.Width;
            distanceZero.LockSize(true);
            angleSightElevationAngle.Width = comboBoxPreset.Width;
            distanceZero.LockSize(true);
        }

        private void comboBoxPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZeroPreset preset = comboBoxPreset.SelectedItem as ZeroPreset;
            if (preset != null)
            {
                distanceSight.Value = preset.ZeroHeight;
                distanceZero.Value = preset.ZeroDistance;
                checkBoxNearZero.Checked = preset.NearZero;
            }
        }

        private void checkBoxOtherAmmo_CheckedChanged(object sender, EventArgs e)
        {
            bulletInfoInput.Enabled = checkBoxOtherAmmo.Checked;
        }

        private void checkBoxOtherAtmo_CheckedChanged(object sender, EventArgs e)
        {
            atmoInfoInput.Enabled = checkBoxOtherAtmo.Checked;
        }

        private MeasurementSystem mMeasurementSystem;

        public MeasurementSystem MeasurementSystem
        {
            get
            {
                return mMeasurementSystem;
            }
            set
            {
                mMeasurementSystem = value;
                bulletInfoInput.MeasurementSystem = value;
                atmoInfoInput.MeasurementSystem = value;
                if (mMeasurementSystem == UI.MeasurementSystem.Imperial)
                {
                    distanceSight.Unit = Distance.Unit.Inch;
                    distanceZero.Unit = Distance.Unit.Yard;
                }
                else
                {
                    distanceSight.Unit = Distance.Unit.Centimeter;
                    distanceZero.Unit = Distance.Unit.Meter;
                }
            }
        }

        public void FillShotInfo(ShotInfo shotInfo)
        {
            shotInfo.ZeroDistance = distanceZero.Value;
            shotInfo.NearZero = checkBoxNearZero.Checked;
            shotInfo.SightHeight = distanceSight.Value;
            shotInfo.Clicks = (int)numericClicks.Value;

            if (checkBoxUseElevationAngle.Checked)
            {
                shotInfo.ElevationAngle = angleSightElevationAngle.Value;
            }
            else
            {
                shotInfo.ElevationAngle = null;
                if (checkBoxOtherAmmo.Checked)
                    shotInfo.Ammo = bulletInfoInput.GetAmmoInfo();
                if (checkBoxOtherAtmo.Checked)
                    shotInfo.Atmosphere = atmoInfoInput.GetAtmosphere();
            }
        }

        public void Initialize(MeasurementSystem msi)
        {
            MeasurementSystem = msi;
            distanceSight.Value = msi == UI.MeasurementSystem.Imperial ? new Distance(1.5, Distance.Unit.Inch) : new Distance(5, Distance.Unit.Centimeter);
            distanceZero.Value = msi == UI.MeasurementSystem.Imperial ? new Distance(100, Distance.Unit.Yard) : new Distance(100, Distance.Unit.Meter);
        }

        public void AddControlsToFormState(FormState state)
        {
            AddControlsToFormState(state, "");
        }

        public void AddControlsToFormState(FormState state, string prefix)
        {
            state.AddControl("" + "zero-sight-height", distanceSight);
            state.AddControl("" + "zero-distance", distanceZero);
            state.AddControl("" + "zero-clicks", numericClicks);
            state.AddControl("" + "zero-other-bullet", checkBoxOtherAmmo);
            state.AddControl("" + "zero-other-atmo", checkBoxOtherAtmo);
            state.AddControl("" + "zero-use-elevation-angle", checkBoxUseElevationAngle);
            state.AddControl("" + "zero-elevation-angle", angleSightElevationAngle);
            bulletInfoInput.AddControlsToFormState(state, "zero-");
            atmoInfoInput.AddControlsToFormState(state, "zero-");
        }

        public Distance ZeroDistance
        {
            get
            {
                return distanceZero.Value;
            }
            set
            {
                distanceZero.Value = value;
            }
        }

        public Distance SightHeight
        {
            get
            {
                return distanceSight.Value;
            }
            set
            {
                distanceSight.Value = value;
            }
        }

        private void checkBoxUseElevationAngle_CheckedChanged(object sender, EventArgs e)
        {
            handleElevationAngle();
        }

        private void handleElevationAngle()
        {
            if (checkBoxUseElevationAngle.Checked)
            {
                angleSightElevationAngle.ReadOnly = false;
                checkBoxOtherAmmo.Enabled = false;
                bulletInfoInput.Enabled = false;
                checkBoxOtherAtmo.Enabled = false;
                atmoInfoInput.Enabled = false;
            }
            else
            {
                angleSightElevationAngle.ReadOnly = true;
                checkBoxOtherAmmo.Enabled = true;
                bulletInfoInput.Enabled = checkBoxOtherAmmo.Checked;
                checkBoxOtherAtmo.Enabled = true;
                atmoInfoInput.Enabled = checkBoxOtherAtmo.Checked;

            }
        }

        public Angle ZeroElevationAngle
        {
            get
            {
                return angleSightElevationAngle.Value;
            }
            set
            {
                angleSightElevationAngle.Value = value;
            }
        }

        public bool UseZeroElevationAngle
        {
            get
            {
                return checkBoxUseElevationAngle.Checked;
            }
            set
            {
                checkBoxUseElevationAngle.Checked = value;
                handleElevationAngle();
            }
        }
    }
}
