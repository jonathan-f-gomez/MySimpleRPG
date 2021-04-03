using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Engine
{ 
    public class LivingCreature : INotifyPropertyChanged
    {
        private int currentHitPoints { get; set; }
        public int MaximumHitPoints { get; set; }

        public int CurrentHitPoints
        {
            get { return currentHitPoints; }
            set
            {
                currentHitPoints = value;
                OnPropertyChanged("CurrentHitPoints")
            }

        }

        public LivingCreature(int currentHitPoints, int maximunHitPoints)
        {
            this.MaximumHitPoints = maximunHitPoints;
            this.CurrentHitPoints = currentHitPoints;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
