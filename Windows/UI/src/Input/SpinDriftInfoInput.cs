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
    public partial class SpinDriftInfoInput : UserControl
    {
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
                if (mMeasurementSystem == UI.MeasurementSystem.Imperial)
                {
                    distanceDiameter.Unit = Distance.Unit.Inch;
                    distanceLength.Unit = Distance.Unit.Inch;
                    distanceRifling.Unit = Distance.Unit.Inch;
                }
                else
                {
                    distanceDiameter.Unit = Distance.Unit.Millimeter;
                    distanceLength.Unit = Distance.Unit.Millimeter;
                    distanceRifling.Unit = Distance.Unit.Millimeter;

                }
            }
        }


        public SpinDriftInfoInput()
        {
            InitializeComponent();
            DriftInfoAmmoPresetList.FillComboBox(comboBoxPreset);
            DriftInfoGunPresetList.FillComboBox(comboBoxPreset1);
            comboBoxPreset.SelectedIndex = -1;
            comboBoxPreset1.SelectedIndex = -1;
            radioButtonRH.Checked = true;
        }

        private void comboBoxPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriftInfoAmmoPreset preset = comboBoxPreset.SelectedItem as DriftInfoAmmoPreset;
            if (preset != null)
            {
                distanceDiameter.Value = preset.BulletDiameter;
                distanceLength.Value = preset.BulletLength;
            }
        }

        public DriftInfo GetDriftInfo()
        {
            if (!checkBoxCalculate.Checked)
                return null;

            return new DriftInfo(distanceLength.Value, distanceDiameter.Value, distanceRifling.Value, radioButtonRH.Checked);
        }

        public void AddControlsToFormState(FormState state)
        {
            AddControlsToFormState(state, "");
        }

        public void AddControlsToFormState(FormState state, string prefix)
        {
            state.AddControl(prefix + "drift-diameter", distanceDiameter);
            state.AddControl(prefix + "drift-length", distanceLength);
            state.AddControl(prefix + "drift-step", distanceRifling);
            state.AddControl(prefix + "drift-lh", radioButtonLH);
            state.AddControl(prefix + "drift-rh", radioButtonRH);
        }

        public bool CalculateSpinDrift
        {
            get
            {
                return checkBoxCalculate.Checked;
            }
            set
            {
                checkBoxCalculate.Checked = value;
            }
        }

        public Distance BulletDiameter
        {
            get
            {
                return distanceDiameter.Value;
            }
            set
            {
                distanceDiameter.Value = value;
            }
        }

        public Distance BulletLength
        {
            get
            {
                return distanceLength.Value;
            }
            set
            {
                distanceLength.Value = value;
            }
        }

        public Distance Rifling
        {
            get
            {
                return distanceRifling.Value;
            }
            set
            {
                distanceRifling.Value = value;
            }
        }

        public bool RightHandRifling
        {
            get
            {
                return radioButtonRH.Checked;
            }
            set
            {
                radioButtonRH.Checked = value;
                radioButtonLH.Checked = !value;
            }
        }

        private void comboBoxPreset1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriftInfoGunPreset preset = comboBoxPreset1.SelectedItem as DriftInfoGunPreset;
            if (preset != null)
            {
                distanceRifling.Value = preset.RiflingTwist;
                radioButtonLH.Checked = !preset.RightHandTwist;
                radioButtonRH.Checked = preset.RightHandTwist;
            }

        }
        
    }
}
