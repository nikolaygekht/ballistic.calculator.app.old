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
    public partial class ShotParametersInput : UserControl, IMeasurementSystemListener
    {
        public ShotParametersInput()
        {
            InitializeComponent();

            angleVClicks.Value = new Angle(0.25, Angle.Unit.Moa);
            angleHClicks.Value = new Angle(0.25, Angle.Unit.Moa);

            //prevent resizing under wine
            distanceMax.Width = labelSize.Width;
            distanceStep.Width = labelSize.Width;
            angleCant.Width = labelSize.Width;
            angleShot.Width = labelSize.Width;
            angleVClicks.Width = labelSize.Width;
            angleHClicks.Width = labelSize.Width;
            distanceMax.LockSize(true);
            distanceStep.LockSize(true);
            angleCant.LockSize(true);
            angleShot.LockSize(true);
            angleVClicks.LockSize(true);
            angleHClicks.LockSize(true);

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
                if (mMeasurementSystem == UI.MeasurementSystem.Imperial)
                {
                    distanceMax.Unit = Distance.Unit.Yard;
                    distanceStep.Unit = Distance.Unit.Yard;
                }
                else
                {
                    distanceMax.Unit = Distance.Unit.Meter;
                    distanceStep.Unit = Distance.Unit.Meter;
                }
            }
        }

        public void FillShotInfo(ShotInfo si)
        {
            si.MaxDistance = distanceMax.Value;
            si.Step = distanceStep.Value;
            si.ShotAngle = angleShot.Value;
            si.CantAngle = angleCant.Value;
            si.VerticalClick = angleVClicks.Value;
            si.HorizonalClick = angleHClicks.Value;
        }

        public void Initialize(MeasurementSystem msi)
        {
            MeasurementSystem = msi;
            distanceMax.Value = msi == MeasurementSystem.Imperial ? new Distance(1000, Distance.Unit.Yard) : new Distance(1000, Distance.Unit.Meter);
            distanceStep.Value = msi == MeasurementSystem.Imperial ? new Distance(25, Distance.Unit.Yard) : new Distance(25, Distance.Unit.Meter);
        }

        public void AddControlsToFormState(FormState state)
        {
            AddControlsToFormState(state, "");
        }

        public void AddControlsToFormState(FormState state, string prefix)
        {
            state.AddControl(prefix + "parameters-max", distanceMax);
            state.AddControl(prefix + "parameters-step", distanceStep);
            state.AddControl(prefix + "parameters-angle-shot", angleShot);
            state.AddControl(prefix + "parameters-angle-cant", angleCant);
            state.AddControl(prefix + "parameters-hclick", angleHClicks);
            state.AddControl(prefix + "parameters-vclick", angleVClicks);
        }

        public Angle VerticalClick
        {
            get
            {
                return angleVClicks.Value;
            }
            set
            {
                angleVClicks.Value = value;
            }
        }

        public Angle HorizontalClick
        {
            get
            {
                return angleHClicks.Value;
            }
            set
            {
                angleHClicks.Value = value;
            }

        }


    }
}
