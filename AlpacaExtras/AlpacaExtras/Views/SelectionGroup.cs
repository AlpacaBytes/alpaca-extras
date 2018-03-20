using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace AlpacaExtras.Views
{
    public class SelectionGroup : StackLayout
    {
        public static readonly BindableProperty SelectedValuesProperty =
            BindableProperty.Create(nameof(SelectedValues), typeof(IList), typeof(SelectionGroup), null, BindingMode.TwoWay, propertyChanged: ApplySelectedValues);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(SelectionGroup), null, BindingMode.TwoWay, propertyChanged: ApplyItemsSource);

        public static readonly BindableProperty SelectionTemplateProperty =
            BindableProperty.Create(nameof(SelectionTemplate), typeof(DataTemplate), typeof(SelectionGroup), new DataTemplate(typeof(SelectButton)));

        public IList SelectedValues { get => (IList)GetValue(SelectedValuesProperty); set => SetValue(SelectedValuesProperty, value); }
        public IList ItemsSource { get => (IList)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
        public DataTemplate SelectionTemplate { get => (DataTemplate)GetValue(SelectionTemplateProperty); set => SetValue(SelectionTemplateProperty, value); }

        private static void ApplySelectedValues(BindableObject bindable, object oldValue, object newValue)
        {
            var group = (SelectionGroup)bindable;
            group.UpdateSelectedButtons(group);
        }

        public void SelectValue(object value)
        {
            if (SelectedValues != null)
            {
                if (SelectedValues.Contains(value))
                {
                    SelectedValues.Remove(value);
                }
                else
                {
                    SelectedValues.Add(value);
                }
                UpdateSelectedButtons(this);
            }
        }

        private void UpdateSelectedButtons(SelectionGroup group)
        {
            foreach (var view in group.Children)
            {
                if (view is SelectButton btn)
                {
                    btn.IsToggled = SelectedValues?.Contains(btn.Value) ?? false;
                }
            }
        }

        private static void ApplyItemsSource(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= ((SelectionGroup)bindable).Oc_CollectionChanged;
            }
            if (newValue != null && newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += ((SelectionGroup)bindable).Oc_CollectionChanged;
            }

            UpdateRadioButtons((SelectionGroup)bindable);
        }

        private void Oc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateRadioButtons(this);
        }

        static void UpdateRadioButtons(SelectionGroup group)
        {
            group.Children.Clear();

            if (group.ItemsSource != null)
            {
                foreach (var item in group.ItemsSource)
                {
                    if (group.SelectionTemplate != null)
                    {
                        var select = group.SelectionTemplate.CreateContent() as View;
                        select.BindingContext = item;
                        group.Children.Add(select);
                    }
                }
            }

            group.InvalidateLayout();
        }
    }
}
