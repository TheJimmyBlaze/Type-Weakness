using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using PokeTypeWeakness.Models;
using PokeTypeWeakness.Services;

namespace PokeTypeWeakness.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<PokeType> DataStore => DependencyService.Get<IDataStore<PokeType>>();
        public Random random => DependencyService.Get<Random>();

        private const string AD_UNIT_ID = "ca-app-pub-8758740980137529/7549952927";
        public string AdUnitID { get { return AD_UNIT_ID; } }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
