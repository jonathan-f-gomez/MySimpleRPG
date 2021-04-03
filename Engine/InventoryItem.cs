using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

//TODO: Lesson 20.3 Step 3

namespace Engine
{
    public class InventoryItem : INotifyPropertyChanged
    {
        private Item details;
        private int quantity;

        public Item Details 
        {
            get { return details; }
            set
            {
                details = value;
                OnPropertyChanged("Details");
            }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
                OnPropertyChanged("Description");
            }
        }

        public string Description
        {
            get { return Quantity > 1 ? Details.NamePlural : Details.Name; }
        }

        public InventoryItem(Item details,int quantity)
        {
            this.Details = details;
            this.Quantity = quantity;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
