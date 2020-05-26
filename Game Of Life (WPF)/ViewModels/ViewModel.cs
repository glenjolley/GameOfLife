using Game_Of_Life__WPF_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Game_Of_Life__WPF_.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Universe uni;
        public Cell[] Life
        {
            get 
                { 
                    if (uni != null)
                        return uni.Life;
                    else
                        return null;
                }
        }

        public int Length
        {
            get { return this.Length; }
            set
            {
                this.Length = value;
                NotifyPropertyChanged("Length");
            }
        }

        public int Width
        {
            get { return this.Width; }
            set
            {
                this.Width = value;
                NotifyPropertyChanged("Width");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
    }
}
