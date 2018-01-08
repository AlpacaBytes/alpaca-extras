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
            BindableProperty.Create(nameof(SelectedValue), typeof(object), typeof(ToggleButton), false, BindingMode.TwoWay);

        public object SelectedValue { get => GetValue(SelectedValueProperty); set => SetValue(SelectedValueProperty, value); }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            if (child is RadioButton radio)
            {
                radio.IsToggled = radio.Value == SelectedValue;
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
                        radio.IsToggled = radio.Value == SelectedValue;
                    }
                }
            }
        }
    }
}
