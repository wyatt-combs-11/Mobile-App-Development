using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Xamarin.FormsBook.Toolkit
{
    public class DateTimeViewModel : INotifyPropertyChanged
    {
        DateTime dateTime = DateTime.Now;
        DateTime startdt = DateTime.Now;
        TimeSpan elapsed = TimeSpan.Zero;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTimeViewModel()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(15), OnTimerTick);
        }

        bool OnTimerTick()
        {
            DateTime = DateTime.Now;
			return true;
        }

        public DateTime DateTime
        {
            private set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    elapsed = dateTime - startdt;

                    // Fire the event.
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elapsed"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EvenOdd"));
                }
            }

            get
            {
                return dateTime;
            }
		}

        public TimeSpan Elapsed => elapsed;
        public string EvenOdd => (elapsed.Seconds % 2 == 0 ? "Even" : "Odd");
    }
}
