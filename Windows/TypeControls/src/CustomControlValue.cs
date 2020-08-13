using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gehtsoft.BallisticCalculator.UI
{
    public partial class CustomControlValue : UserControl
    {
        private double mValue;

        public double Value
        {
            get
            {
                if (!mValueChanged)
                    return mValue;
                double v = 0;
                if (Double.TryParse(textBoxValue.Text, NumberStyles.Float | NumberStyles.AllowThousands, mNumberFormat, out v))
                    return v;
                else
                    return 0;
            }
            set
            {
                mValue = value;
                textBoxValue.Text = value.ToString("G", mNumberFormat);
                mValueChanged = false;
            }
        }

        public string[] Units
        {
            set
            {
                mDistableUnitHandling = true;
                comboBoxUnits.Items.Clear();
                foreach (string s in value)
                    comboBoxUnits.Items.Add(s);
                mDistableUnitHandling = false;
            }
        }

        public string UnitName
        {
            get
            {
                return comboBoxUnits.SelectedItem as string;
            }
            set
            {
                mDistableUnitHandling = true;
                comboBoxUnits.SelectedItem = value;
                mDistableUnitHandling = false;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return textBoxValue.ReadOnly;
            }
            set
            {
                textBoxValue.ReadOnly = value;
            }
        }


        NumberFormatInfo mNumberFormat = null;

        public CustomControlValue()
        {
            mNumberFormat = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxValue.ReadOnly)
            {
                e.Handled = true;
                return;
            }

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
                e.Handled = false;
            else if (e.KeyChar == '.')
            {
                e.KeyChar = mNumberFormat.CurrencyDecimalSeparator[0];
                e.Handled = textBoxValue.Text.Contains(mNumberFormat.CurrencyDecimalSeparator);
            }
            else if (e.KeyChar == mNumberFormat.CurrencyDecimalSeparator[0])
                e.Handled = textBoxValue.Text.Contains(mNumberFormat.CurrencyDecimalSeparator);
            else if (e.KeyChar == '-')
                e.Handled = textBoxValue.Text.Contains("-") || textBoxValue.SelectionStart != 0;
            else
                e.Handled = true;
        }

        public delegate void ValueChangedDelegate(object sender, EventArgs e);
        public event ValueChangedDelegate ValueChanged;
        public delegate void UnitChangedDeleate(object sender, EventArgs e);
        public event UnitChangedDeleate UnitChanged;
        private bool mValueChanged = false;

        public bool IsValueChanged
        {
            get
            {
                return mValueChanged;
            }
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            mValueChanged = true;
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        protected virtual void OnUnitChanged()
        {
            ;
        }

        private bool mDistableUnitHandling = false;

        private void comboBoxUnits_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!mDistableUnitHandling)
                OnUnitChanged();
            if (UnitChanged != null)
                UnitChanged(this, e);
        }

        private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                double v = 0;
                if (Double.TryParse(textBoxValue.Text, out v))
                {
                    v = v + 1;
                    textBoxValue.Text = v.ToString("G", mNumberFormat);
                }
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                double v = 0;
                if (Double.TryParse(textBoxValue.Text, out v))
                {
                    if (v > 1)
                    {
                        v = v - 1;
                        textBoxValue.Text = v.ToString("G", mNumberFormat);
                    }
                }
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        Size mSize;
        bool mLockSize;

        public void LockSize(bool lockSize)
        {
            mLockSize = lockSize;
            if (lockSize)
                mSize = new Size(this.Width, this.Height);
        }

        private void comboBoxUnits_SizeChanged(object sender, EventArgs e)
        {
            if (mLockSize &&
                this.Width != mSize.Width &&
                this.Height != mSize.Height)
            {
                this.Width = mSize.Width;
                this.Height = mSize.Height;
            }
        }

        public void Clear()
        {
            textBoxValue.Text = "";
        }
    }
}
