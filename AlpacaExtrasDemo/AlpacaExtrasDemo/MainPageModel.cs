using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlpacaExtrasDemo
{
    class MainPageModel : INotifyPropertyChanged
    {
        string _tValue;
        public string TValue
        {
            get => _tValue;
            set
            {
                _tValue = value;
                RaisePropertyChanged();
            }
        }

        string _number;
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                RaisePropertyChanged();
            }
        }

        Size _size = Size.Medium;
        public Size SelectedSize
        {
            get => _size;
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        //public ObservableCollection<string> ListViewData { get; set; } = new ObservableCollection<string>
        //{
        //    "POCO",
        //    "Net Standard",
        //    "Android",
        //    "iOS"
        //};

        //public ICommand ListViewItemClickedCommand => new Command<string>((arg) => ListViewData.Remove(arg));

        public List<string> RadioItems => new List<string>(new[] { "Tom", "Ted", "Tina" });

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
