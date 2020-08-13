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
using Android.Util;

using BallisticCalculator.Utils;
using System.Globalization;

namespace BallisticCalculator.Views
{
    [Register("ballisticcalculator.views.EditTextEx")]
    public class EditTextEx : EditText
    {
        private void Init()
        {
            base.TextChanged += OnTextChanged;
        }

        #region Constructors

        public EditTextEx(Context context)
            : base(context)
        {
            Init();
        }
        public EditTextEx(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init();
        }

        protected EditTextEx(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Init();
        }

        public EditTextEx(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Init();
        } 

        #endregion

        private IUnitsAdapter _linkedUnitsAdapter;
        public IUnitsAdapter UnitsAdapter
        {
            get
            {
                if (_textChanged)
                {
                    double value;
                    if (Utilities.TryParseDouble(Text, out value))
                        _linkedUnitsAdapter.Set(value, _linkedUnitsAdapter.CurrentUnit());
                }
                return _linkedUnitsAdapter;
            }
            set
            {
                _linkedUnitsAdapter = value;
                SetText(Utilities.RoundDouble(_linkedUnitsAdapter.CurrentValue(), _linkedUnitsAdapter.DefaultDisplayPrecision()).ToString("G", CultureInfo.InvariantCulture));
                _linkedUnitsAdapter.ValueChanged += _linkedUnitsAdapter_ValueChanged;
            }
        }

        private void _linkedUnitsAdapter_ValueChanged(object sender, EventArgs e)
        {
            SetText(Utilities.RoundDouble(_linkedUnitsAdapter.CurrentValue(), _linkedUnitsAdapter.DefaultDisplayPrecision()).ToString("G", CultureInfo.InvariantCulture));
        }

        public void UpdateUnits(string units)
        {
            if (_textChanged)
            {
                double value;
                if (Utilities.TryParseDouble(Text, out value))
                    _linkedUnitsAdapter.Set(value, units);
            }
            else
            {
                _linkedUnitsAdapter.ChangeUnit(units);
            }
            SetSelection(Text.Length);
        }

        private bool _textChanged = false;
        private void SetText(string text)
        {
            base.Text = text;
            _textChanged = false;
        }

        private void OnTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _textChanged = true;
        }
    }
}