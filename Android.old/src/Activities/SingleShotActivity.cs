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
using BallisticCalculator.Utils;
using MathEx.ExternalBallistic.Units;
using MathEx.ExternalBallistic;

namespace BallisticCalculator.Activities
{
    [Activity(Label = "Single Shot")]
    public class SingleShotActivity : Activity
    {
        private NumberPicker numberPickerRange;
        private NumberPicker numberPickerWind;
        private NumberPicker numberPickerWindDirectionClock;

        private int rangeMultiplyer = 10;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SingleShot);

            if (ApplicationData.Instance.SelectedTraceInfo == null)
            {
                Toast.MakeText(this, Resource.String.msg_TraceNotSelected, ToastLength.Long).Show();
                Finish();
                return;
            }

            // Just divide Range step by 5 for single shot.
            rangeMultiplyer = (int)((ApplicationData.Instance.RangeStep.Get(DefaultUnits.Range)) / 5 + 0.5);

            FindViewById<TextView>(Resource.Id.textViewRange).Text += "\n(" + Distance.UnitToName(DefaultUnits.Range) + ")";
            FindViewById<TextView>(Resource.Id.textViewWind).Text += "\n(" + Velocity.UnitToName(DefaultUnits.Wind.Velocity) + ")";

            numberPickerRange = FindViewById<NumberPicker>(Resource.Id.numberPickerRange);
            numberPickerRange.MinValue = 0;
            numberPickerRange.MaxValue = (int)(ApplicationData.Instance.RangeMax.Get(DefaultUnits.Range) / rangeMultiplyer);
            numberPickerRange.SetFormatter(new NumberPickerFormatter(rangeMultiplyer));
            numberPickerRange.Value = (int)(ApplicationData.Instance.SingleShotDistance.Get(DefaultUnits.Range) / rangeMultiplyer + 0.5);
            Utilities.FixNumberPickerBug(numberPickerRange);

            numberPickerWind = FindViewById<NumberPicker>(Resource.Id.numberPickerWind);
            numberPickerWind.MinValue = 0;
            numberPickerWind.MaxValue = (int)(new BeaufortWindScale(this, DefaultUnits.Wind.Velocity).GetMinWindSpeed(12).Get(DefaultUnits.Wind.Velocity) + 0.5);
            numberPickerWind.Value = Math.Min((int)(ApplicationData.Instance.WindInfo.Speed.Get(DefaultUnits.Wind.Velocity) + 0.5), numberPickerWind.MaxValue);

            numberPickerWindDirectionClock = FindViewById<NumberPicker>(Resource.Id.numberPickerWindDirection);
            numberPickerWindDirectionClock.MinValue = 1;
            numberPickerWindDirectionClock.MaxValue = 12;
            numberPickerWindDirectionClock.Value = Utilities.HourFromAngle(ApplicationData.Instance.WindInfo.Direction);

            FindViewById<Button>(Resource.Id.buttonCalculateShot).Click += SingleShotActivity_Click;

            FindViewById<TextView>(Resource.Id.textViewCorrectionAngleUnits).Text = Angle.UnitToName(DefaultUnits.Reticle.Adjustment);

            ApplicationData.Instance.calculateSingleShot(
                new Distance(numberPickerRange.Value * rangeMultiplyer, DefaultUnits.Range),
                new Distance(rangeMultiplyer, DefaultUnits.Range),
                new Velocity(numberPickerWind.Value, DefaultUnits.Wind.Velocity),
                Utilities.AngleFromHour(numberPickerWindDirectionClock.Value),
                showCorrections);
        }

        private void SingleShotActivity_Click(object sender, EventArgs e)
        {
            ApplicationData.Instance.calculateSingleShot(
                new Distance(numberPickerRange.Value * rangeMultiplyer, DefaultUnits.Range),
                new Distance(rangeMultiplyer, DefaultUnits.Range),
                new Velocity(numberPickerWind.Value, DefaultUnits.Wind.Velocity),
                Utilities.AngleFromHour(numberPickerWindDirectionClock.Value),
                showCorrections);
        }

        private void showCorrections(BallisticInfo info)
        {
            RunOnUiThread(() =>
            {
                FindViewById<TextView>(Resource.Id.textViewElevationCorrectionAngle).Text = info.Hold.Get(DefaultUnits.Reticle.Adjustment).ToString("f2");
                FindViewById<TextView>(Resource.Id.textViewElevationCorrectionClicks).Text = info.HoldClicks.ToString();

                FindViewById<TextView>(Resource.Id.textViewWindCorrectionAngle).Text = info.WindageCorrection.Get(DefaultUnits.Reticle.Adjustment).ToString("f2");
                FindViewById<TextView>(Resource.Id.textViewWindCorrectionClicks).Text = info.WindageClicks.ToString();
            });
        }

        private class NumberPickerFormatter : Java.Lang.Object, NumberPicker.IFormatter
        {
            private int _multiplier;
            public NumberPickerFormatter(int multiplier)
            {
                _multiplier = multiplier;
            }

            public string Format(int value)
            {
                return (value * _multiplier).ToString();
            }
        }
    }
}