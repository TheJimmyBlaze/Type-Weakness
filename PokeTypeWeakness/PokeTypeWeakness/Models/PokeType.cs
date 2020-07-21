using PokeTypeWeakness.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace PokeTypeWeakness.Models
{
    public class PokeType
    {
        private IDataStore<PokeType> DataStore => DependencyService.Get<IDataStore<PokeType>>();

        public string NaturalID { get; set; }
        public string PrimaryColourHex { get; set; }
        public string SecondaryColourHex { get; set; }
        public string[] WeaknessNaturalIDs { get; set; }

        public Color PrimaryColour { get { return Color.FromHex(PrimaryColourHex); } }
        public Color SecondaryColour { get { return Color.FromHex(SecondaryColourHex); } }

        public string Image { get { return string.Format("{0}.png", NaturalID); } }

        public ObservableCollection<PokeType> Weaknesses { get; set; }

        public PokeType()
        {
            Weaknesses = new ObservableCollection<PokeType>();
        }

        public async Task LoadWeaknesses()
        {
            Weaknesses.Clear();
            foreach(string weaknessNaturalID in WeaknessNaturalIDs)
            {
                PokeType weakness = await DataStore.GetItemAsync(weaknessNaturalID);
                Weaknesses.Add(weakness);
            }
        }
    }
}
