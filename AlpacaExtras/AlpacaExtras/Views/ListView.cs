using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlpacaExtras.Views
{
    [Preserve(AllMembers = true)]
    public class ListView : Xamarin.Forms.ListView
    {
        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create("ItemClickedProperty", typeof(ICommand), typeof(ListView));

        static ListViewCachingStrategy GetStrategy()
        {
            return ListViewCachingStrategy.RecycleElement;
        }

        public ListView() : base(GetStrategy())
        {
            this.ItemTapped += this.OnItemTapped;
        }


        public ICommand ItemClickCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }


        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && this.ItemClickCommand != null && this.ItemClickCommand.CanExecute(e.Item))
            {
                this.ItemClickCommand.Execute(e.Item);
                this.SelectedItem = null;
            }
            else
                SelectedItem = null;
        }
    }
}