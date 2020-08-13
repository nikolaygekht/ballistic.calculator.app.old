using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Keycode = Android.Views.Keycode;
using StringBuilder = System.Text.StringBuilder;

namespace Gehtsoft.BallisticCalculator.View
{
    class InetAddresEditText : EditText
    {
       public InetAddresEditText(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public InetAddresEditText(Context context) : base(context)
        {
        }

        public InetAddresEditText(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public InetAddresEditText(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        private bool processRemoveChar(Keycode keyCode, KeyEvent e)
        {
            int position = this.SelectionStart;
            position--;
            if (position == -1) return true;
            var charAtPos = Text[position];
            if (charAtPos == '.')
            {
                position--;
                if (position == -1) return true;
            }

            StringBuilder sb = new StringBuilder(Text);
            sb[position] = '0';
            Text = sb.ToString();

            if (position > 0)
                SetSelection(position);
            else
                SetSelection(0);

            return true;
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (Resources.Configuration.Keyboard == Android.Content.Res.KeyboardType.Nokeys)
                return base.OnKeyDown(keyCode, e);

            if (e.KeyCode == Keycode.Del)
                return processRemoveChar(keyCode, e);

            if (e.KeyCode == Keycode.DpadLeft || e.KeyCode == Keycode.DpadRight || e.KeyCode == Keycode.Back)
                return base.OnKeyDown(keyCode, e);

           
            if (e.DisplayLabel >= '0' && e.DisplayLabel <= '9')
            {
                int position = this.SelectionStart;
                if (position >= Text.Length) return true;
                var charAtPos = Text[position];
                if (charAtPos == '.')
                {
                    position++;
                    if (position >= Text.Length) return true;
                    charAtPos = Text[position];
                }

                StringBuilder sb = new StringBuilder(Text);
                sb[position] = e.DisplayLabel;
                Text = sb.ToString();

                position++;
                if (position < Text.Length)
                    SetSelection(position);
                else
                    SetSelection(Text.Length);
            }

            return true;
        }
    }
}