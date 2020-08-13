using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MathEx.ExternalBallistic;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic.JBM;

namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class BulletInfoInput : UserControl, IMeasurementSystemListener
    {
        public delegate void BulletNameChangedDelegate(object sender, EventArgs e);

        public event BulletNameChangedDelegate OnBulletNameChanged;

        private bool mAdvancedMode;

        public bool AdvancedMode
        {
            get
            {
                return mAdvancedMode;
            }
            set
            {
                mAdvancedMode = value;
                OnUpdateAdvancedMode();
            }
        }

        private static string mAmmoDir = null;

        public static string AmmoDir
        {
            get
            {
                return mAmmoDir;
            }
            set
            {
                mAmmoDir = value;
            }
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
                    weightControlBullet.Unit = Weight.Unit.Grain;
                    distanceBarrelLength.Unit = Distance.Unit.Inch;
                    velocityAtMuzzle.Unit = Velocity.Unit.FeetPerSecond;
                    distanceBulletInfoDiameter.Unit = Distance.Unit.Inch;
                    distanceBulletInfoLength.Unit = Distance.Unit.Inch;
                }
                else
                {
                    weightControlBullet.Unit = Weight.Unit.Gram;
                    distanceBarrelLength.Unit = Distance.Unit.Millimeter;
                    velocityAtMuzzle.Unit = Velocity.Unit.MeterPerSecond;
                    distanceBulletInfoDiameter.Unit = Distance.Unit.Millimeter;
                    distanceBulletInfoLength.Unit = Distance.Unit.Millimeter;
                }
            }
        }

        public BulletInfoInput()
        {
            InitializeComponent();
            OnUpdateAdvancedMode();
            comboBoxBCType.SelectedIndex = 0;
            numericBC.Value = 0.500M;
            CaliberList.FillComboBox(comboBoxCaliber);
            BulletList.FillComboBox(comboBoxBullet);
            //prevent resize under wine
            weightControlBullet.Width = textBoxName.Width;
            distanceBarrelLength.Width = textBoxName.Width;
            velocityAtMuzzle.Width = textBoxName.Width;
            distanceBulletInfoDiameter.Width = textBoxName.Width;
            distanceBulletInfoLength.Width = textBoxName.Width;
            weightControlBullet.LockSize(true);
            distanceBarrelLength.LockSize(true);
            velocityAtMuzzle.LockSize(true);
            distanceBulletInfoDiameter.LockSize(true);
            distanceBulletInfoLength.LockSize(true);
        }

        private void BulletInfoInput_EnabledChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in Controls)
            {
                ctrl.Enabled = Enabled;
                ctrl.Invalidate();
            }
            Invalidate();
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            mAdvancedMode = !mAdvancedMode;
            OnUpdateAdvancedMode();
        }

        private void OnUpdateAdvancedMode()
        {
            buttonSave.Visible = mAdvancedMode;
            labelSource.Visible = mAdvancedMode;
            textBoxSource.Visible = mAdvancedMode;
            labelCaliber.Visible = mAdvancedMode;
            comboBoxCaliber.Visible = mAdvancedMode;
            comboBoxBullet.Visible = mAdvancedMode;
            labelBarrelLength.Visible = mAdvancedMode;
            distanceBarrelLength.Visible = mAdvancedMode;
            distanceBulletInfoDiameter.Visible = mAdvancedMode;
            distanceBulletInfoLength.Visible = mAdvancedMode;
            labelDiameter.Visible = mAdvancedMode;
            labelLength.Visible = mAdvancedMode;

            if (!mAdvancedMode)
            {
                this.Width = labelCompactShape.Left + labelCompactShape.Width;
                this.Height = labelCompactShape.Top + labelCompactShape.Height;
            }
            else
            {
                this.Width = labelFullShape.Left + labelFullShape.Width;
                this.Height = labelFullShape.Top + labelFullShape.Height;
            }
            buttonAdvanced.Text = mAdvancedMode ? "Show Less" : "Show More";
        }



        private void SetBC(DragTable table)
        {
            switch (table)
            {
                case DragTable.G1:
                    comboBoxBCType.SelectedIndex = 0;
                    break;
                case DragTable.G2:
                    comboBoxBCType.SelectedIndex = 1;
                    break;
                case DragTable.G5:
                    comboBoxBCType.SelectedIndex = 2;
                    break;
                case DragTable.G6:
                    comboBoxBCType.SelectedIndex = 3;
                    break;
                case DragTable.G7:
                    comboBoxBCType.SelectedIndex = 4;
                    break;
                case DragTable.G8:
                    comboBoxBCType.SelectedIndex = 5;
                    break;
                case DragTable.GI:
                    comboBoxBCType.SelectedIndex = 6;
                    break;
                case DragTable.GL:
                    comboBoxBCType.SelectedIndex = 7;
                    break;
            }
        }

        private DragTable GetBC()
        {
            switch (comboBoxBCType.SelectedIndex)
            {
                case    0:
                    return DragTable.G1;
                case 1:
                    return DragTable.G2;
                case 2:
                    return DragTable.G5;
                case 3:
                    return DragTable.G6;
                case 4:
                    return DragTable.G7;
                case 5:
                    return DragTable.G8;
                case 6:
                    return DragTable.GI;
                case 7:
                    return DragTable.GL;
            }
            return DragTable.G1;
        }



        private void buttonLoad_Click(object sender, EventArgs e)
        {
            AmmoInfoEx ao = LoadAmmoFile();
            if (ao != null)
            {
                if (ao.Name != null)
                {
                    textBoxName.Text = ao.Name;
                    if (OnBulletNameChanged != null)
                        OnBulletNameChanged(this, new EventArgs());
                }
                if (ao.Source != null)
                    textBoxSource.Text = ao.Source;
                else
                    textBoxSource.Text = "";
                if (ao.Caliber != null)
                    comboBoxCaliber.Text = ao.Caliber;
                else
                    comboBoxCaliber.Text = "";
                if (ao.BulletType != null)
                    comboBoxBullet.Text = ao.BulletType;
                else
                    comboBoxBullet.Text = "";

                numericBC.Value = (decimal)ao.BallisticCoefficient;

                SetBC(ao.Table);

                if (ao.BulletWeight != null)
                    weightControlBullet.Value = ao.BulletWeight;
                else
                    weightControlBullet.Value = new Weight(0, weightControlBullet.Unit);
                if (ao.MuzzleVelocity != null)
                    velocityAtMuzzle.Value = ao.MuzzleVelocity;
                else
                    velocityAtMuzzle.Value = new Velocity(0, velocityAtMuzzle.Unit);
                if (ao.BarrelLength != null)
                    distanceBarrelLength.Value = ao.BarrelLength;
                else
                    distanceBarrelLength.Value = new Distance(0, distanceBarrelLength.Unit);
                if (ao.BulletLength != null)
                    distanceBulletInfoLength.Value = ao.BulletLength;
                else
                    distanceBulletInfoLength.Value = new Distance(0, distanceBulletInfoLength.Unit);
                if (ao.BulletDiameter != null)
                    distanceBulletInfoDiameter.Value = ao.BulletDiameter;
                else
                    distanceBulletInfoDiameter.Value = new Distance(0, distanceBulletInfoDiameter.Unit);
            }
        }

        private string mAmmoFile = null;

        private AmmoInfoEx LoadAmmoFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Ammo Files (*.ammo)|*.ammo|All Files|*.*";
            dlg.RestoreDirectory = true;
            if (mAmmoDir != null)
                dlg.InitialDirectory = mAmmoDir;
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    AmmoInfoEx ao = AmmoInfoController.Serialization.ReadFileEx(dlg.FileName);
                    mAmmoFile = dlg.FileName;
                    return ao;
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't load the ammo file");
                }
            }
            return null;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            AmmoInfoEx ao = new AmmoInfoEx(textBoxSource.Text, textBoxName.Text, comboBoxCaliber.Text,
                               comboBoxBullet.Text, weightControlBullet.Value, GetBC(),
                               (double)numericBC.Value,
                               velocityAtMuzzle.Value, distanceBarrelLength.Value, distanceBulletInfoDiameter.Value, distanceBulletInfoLength.Value);

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Ammo Files (*.ammo)|*.ammo|All Files|*.*";
            if (mAmmoDir != null)
                dlg.InitialDirectory = mAmmoDir;

            if (mAmmoFile == null)
                dlg.FileName = textBoxName.Text + ".ammo";
            else
            {
                FileInfo fi = new FileInfo(mAmmoFile);
                if (fi.Name != textBoxName.Text + ".ammo")
                    dlg.FileName = textBoxName.Text + ".ammo";
                else
                    dlg.FileName = fi.Name;
                dlg.InitialDirectory = fi.DirectoryName;
            }
            dlg.RestoreDirectory = true;
            dlg.CheckPathExists = true;
            dlg.OverwritePrompt = true;
            dlg.AddExtension = true;
            dlg.DefaultExt = "ammo";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                AmmoInfoController.Serialization.WriteFileEx(dlg.FileName, ao);
                if (OnBulletNameChanged != null)
                    OnBulletNameChanged(this, new EventArgs());
                mAmmoFile = dlg.FileName;
            }
        }

        public AmmoInfo GetAmmoInfo()
        {
            return new AmmoInfo(GetBC(), (double)numericBC.Value, velocityAtMuzzle.Value, weightControlBullet.Value);
        }

        public AmmoInfoEx GetAmmoInfoEx()
        {
            return new AmmoInfoEx(textBoxSource.Text, textBoxName.Text, comboBoxCaliber.Text,
                               comboBoxBullet.Text, weightControlBullet.Value, GetBC(),
                               (double)numericBC.Value,
                               velocityAtMuzzle.Value, distanceBarrelLength.Value, distanceBulletInfoDiameter.Value, distanceBulletInfoLength.Value);
        }

        public void Initialize(MeasurementSystem msi)
        {
            MeasurementSystem = msi;
        }

        private void rectangleShapeFull_EnabledChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in Controls)
                ctrl.Enabled = Enabled;
            Invalidate();
        }

        private void rectangleShapeCompact_EnabledChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in Controls)
                ctrl.Enabled = Enabled;
            Invalidate();
        }

        public string BulletName
        {
            get
            {
                return textBoxName.Text;
            }
            set
            {
                textBoxName.Text = value;
            }
        }

        public double BallisticCoefficient
        {
            get
            {
                return (double)numericBC.Value;
            }
            set
            {
                numericBC.Value = (decimal)value;
            }
        }

        public DragTable DragTable
        {
            get
            {
                return GetBC();
            }
            set
            {
                SetBC(value);
            }
        }

        public Weight BulletWeight
        {
            get
            {
                return weightControlBullet.Value;
            }
            set
            {
                weightControlBullet.Value = value;
            }
        }

        public Velocity MuzzleVelocity
        {
            get
            {
                return velocityAtMuzzle.Value;
            }
            set
            {
                velocityAtMuzzle.Value = value;
            }
        }

        public Distance BulletDiameter
        {
            get
            {
                return distanceBulletInfoDiameter.Value;
            }
        }

        public Distance BulletLength
        {
            get
            {
                return distanceBulletInfoLength.Value;
            }
        }

        public void AddControlsToFormState(FormState state)
        {
            AddControlsToFormState(state, "");
        }

        public void AddControlsToFormState(FormState state, string prefix)
        {
            state.AddControl(prefix + "bullet-name", textBoxName);
            state.AddControl(prefix + "bullet-source", textBoxSource);
            state.AddControl(prefix + "bullet-type", comboBoxBullet);
            state.AddControl(prefix + "bullet-caliber", comboBoxCaliber);
            state.AddControl(prefix + "bullet-bc", numericBC);
            state.AddControl(prefix + "bullet-drag", comboBoxBCType);
            state.AddControl(prefix + "bullet-weight", weightControlBullet);
            state.AddControl(prefix + "bullet-velocity", velocityAtMuzzle);
            state.AddControl(prefix + "bullet-barrel-lenght", distanceBarrelLength);
            state.AddControl(prefix + "bullet-bullet-diameter", distanceBulletInfoDiameter);
            state.AddControl(prefix + "bullet-bullet-length", distanceBulletInfoLength);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
    }
}
