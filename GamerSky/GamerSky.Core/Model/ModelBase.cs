﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Set<T>(ref T storage, T newValue, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, newValue))
            {
                return;
            }
            storage = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}
