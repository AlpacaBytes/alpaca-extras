using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class RadioButton : ToggleButton
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(object), typeof(ToggleButton));

        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        public RadioButton()
        {
            OnTapped = () =>
            {
                if (Parent is RadioGroup rg)
                {
                    rg.SelectedValue = Value;
                }
            };


        }
    }
}
