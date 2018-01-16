using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Timers;

namespace Matchmaking_UI
{
    public class Counter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        private int count;
        public int Count
        {
            get { return count; }
            private set
            {
                SetField(ref count, value, "Count");
            }
        }

        private int waitTime;
        private System.Timers.Timer threadCounter;
        private delegate void IncDelegate();

        private void IncCount(Object sender, System.Timers.ElapsedEventArgs e)
        {
            ++Count;
        }

        public Counter()
        {
            count = 0;
            waitTime = 1000;

            threadCounter = new System.Timers.Timer();
            threadCounter.Interval = waitTime;
            threadCounter.Elapsed += IncCount;
            threadCounter.Enabled = true;
        }
    }
}