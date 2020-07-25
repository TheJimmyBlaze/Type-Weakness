using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokeTypeWeakness.Models
{
    public class ElectablePokeType: PokeType, INotifyPropertyChanged
    {
        private bool elected;
        public bool Elected
        {
            get { return elected; }
            set
            {
                elected = value;
                OnPropertyChanged(nameof(Elected));
            }
        }

        public ElectablePokeType(PokeType pokeType)
        {
            NaturalID = pokeType.NaturalID;
            PrimaryColourHex = pokeType.PrimaryColourHex;
            SecondaryColourHex = pokeType.SecondaryColourHex;
            WeaknessNaturalIDs = pokeType.WeaknessNaturalIDs;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
