using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class RadioGroup : StackLayout
    {
        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(nameof(SelectedValue), typeof(object), typeof(ToggleButton), false);

        public static readonly BindableProperty DefaultValueProperty =
            BindableProperty.Create(nameof(DefaultValue), typeof(object), typeof(ToggleButton), false);

        public object SelectedValue { get => GetValue(SelectedValueProperty); set => SetValue(SelectedValueProperty, value); }
        public object DefaultValue { get => GetValue(DefaultValueProperty); set => SetValue(DefaultValueProperty, value); }

        protected override void OnAdded(View view)
        {
            base.OnAdded(view);

            if (view is RadioButton radio)
            {
                if (radio.Value == DefaultValue)
                    radio.IsToggled = true;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == SelectedValueProperty.PropertyName)
            {
                // Uncheck all the RadioButtons that weren't just checked
                foreach (var view in Children)
                {
                    if (view is RadioButton radio)
                    {
                        if (radio.Value != SelectedValue)
                            radio.IsToggled = false;
                    }
                }
            }
        }
    }
}
