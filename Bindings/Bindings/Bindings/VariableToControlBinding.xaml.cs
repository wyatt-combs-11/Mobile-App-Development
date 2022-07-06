using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bindings {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MyData : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private int data;
        public int Value {
            get {
                return data;
            }
            set {
                if (data != value) {
                    data = value;
                    RaisePropertyChanged();
                }
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MyPoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double x,y;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                value = new Random().NextDouble();
                if (x != value)
                {
                    x = value;
                    RaisePropertyChanged();
                }
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                value = new Random().NextDouble();
                if (y != value)
                {
                    y = value;
                    RaisePropertyChanged();
                }
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class VariableToControlBinding : ContentPage {
        MyData myData = new MyData { Value = 0 };
        MyPoint myPoint = new MyPoint { X = 0, Y = 0 };
        public VariableToControlBinding() {
            InitializeComponent();
            varValue.SetBinding(Label.TextProperty, new Binding { Source = myData, Path = "Value" });
            xValue.SetBinding(Label.TextProperty, new Binding { Source = myPoint, Path = "X" });
            yValue.SetBinding(Label.TextProperty, new Binding { Source = myPoint, Path = "Y" });
            BindingContext = this;
            Device.StartTimer(new TimeSpan(0, 0, 1), () => { myData.Value += 1; myPoint.X = 0; myPoint.Y = 0; return true; });
        }
    }
}
