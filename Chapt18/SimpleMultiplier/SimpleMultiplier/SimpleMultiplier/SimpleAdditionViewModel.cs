using System;
using System.ComponentModel;

namespace SimpleMultiplier
{
    public class SimpleAdditionViewModel : INotifyPropertyChanged
    {
        public double leftNum, rightNum, sum;

        public event PropertyChangedEventHandler PropertyChanged;

        public double LeftNum
        {
            set
            {
                if(leftNum != value)
                {
                    leftNum = value;
                    OnPropertyChanged("LeftNum");
                    UpdateSum();
                }
            }
            get
            {
                return leftNum;
            }
        }

        public double RightNum
        {
            set
            {
                if (rightNum != value)
                {
                    rightNum = value;
                    OnPropertyChanged("RightNum");
                    UpdateSum();
                }
            }
            get
            {
                return rightNum;
            }
        }

        public double Sum
        {
            set
            {
                if (sum != value)
                {
                    sum = value;
                    OnPropertyChanged("Sum");
                    UpdateSum();
                }
            }
            get
            {
                return sum;
            }
        }

        private void UpdateSum()
        {
            Sum = LeftNum + RightNum;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
