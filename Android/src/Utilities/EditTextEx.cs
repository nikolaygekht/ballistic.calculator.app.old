using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Android.Util;
using System.Globalization;
using Gehtsoft.BallisticCalculator.Utils;
using Android.Text;
using Java.Lang;

namespace Gehtsoft.BallisticCalculator.Views
{
    [Register("ballisticcalculator.views.EditTextEx")]
    public class EditTextEx : EditText
    {
        private void Init()
        {
            TextChanged += OnTextChanged;
            IInputFilter[] filters = { new InputFilter() };
            SetFilters(filters);
        }

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
                if (Text.Length == 0)
                {
                    _linkedUnitsAdapter.Set(0, units);
                }
                else if (Utilities.TryParseDouble(Text, out value))
                {
                    _linkedUnitsAdapter.Set(value, units);
                }
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
            Text = text;
            _textChanged = false;
        }

        private void OnTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _textChanged = true;
        }

        class InputFilter : Java.Lang.Object, IInputFilter
        {
            public ICharSequence FilterFormatted(ICharSequence src, int start, int end, ISpanned dest, int dstart, int dend)
            {
                string source = src.ToString();
                if (src.ToString() == ",")
                {
                    source = ".";
                }
                int len = source.Length + dest.Length();
                if (len > 13)
                {
                    return new Java.Lang.String("");
                }

                return new Java.Lang.String(source);
            }
        }
    }
}