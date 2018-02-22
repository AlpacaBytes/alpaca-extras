using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class RadioGroup : StackLayout
    {
        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(nameof(SelectedValue), typeof(object), typeof(RadioGroup), false, BindingMode.TwoWay);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(RadioGroup), null, BindingMode.TwoWay, propertyChanged: ApplyItemsSource);

        public static readonly BindableProperty RadioTemplateProperty =
            BindableProperty.Create(nameof(RadioTemplate), typeof(DataTemplate), typeof(RadioGroup), new DataTemplate(typeof(RadioButton)));

        public object SelectedValue { get => GetValue(SelectedValueProperty); set => SetValue(SelectedValueProperty, value); }
        public IList ItemsSource { get => (IList)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
        public DataTemplate RadioTemplate { get => (DataTemplate)GetValue(RadioTemplateProperty); set => SetValue(RadioTemplateProperty, value); }

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
                        radio.IsToggled = radio.Value.ToString() == (SelectedValue ?? string.Empty).ToString();
                    }
                }
            }
        }

        static void ApplyItemsSource(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= ((RadioGroup)bindable).Oc_CollectionChanged;
            }
            if (newValue != null && newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += ((RadioGroup)bindable).Oc_CollectionChanged;
            }

            UpdateRadioButtons((RadioGroup)bindable);
        }

        private void Oc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateRadioButtons(this);
        }

        static void UpdateRadioButtons(RadioGroup rg)
        {
            rg.Children.Clear();

            if (rg.ItemsSource != null)
            {
                foreach (var item in rg.ItemsSource)
                {
                    if (rg.RadioTemplate != null)
                    {
                        var radio = rg.RadioTemplate.CreateContent() as View;
                        radio.BindingContext = item;
                        rg.Children.Add(radio);
                    }
                }
            }

            rg.InvalidateLayout();
        }
    }
}
