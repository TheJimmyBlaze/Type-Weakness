using PokeTypeWeakness.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
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
        public string DisplayName { get { return string.Format("{0}{1}", NaturalID.Substring(0, 1).ToUpper(), NaturalID.Substring(1)); } }

        public ObservableCollection<PokeType> Strengths { get; set; }
        public ObservableCollection<PokeType> Weaknesses { get; set; }

        public PokeType()
        {
            Strengths = new ObservableCollection<PokeType>();
            Weaknesses = new ObservableCollection<PokeType>();
        }

        public async Task LoadStrengths()
        {
            if (Weaknesses.Count == 0)
                await LoadWeaknesses();

            IEnumerable<PokeType> pokeTypes = await DataStore.GetItemsAsync();
            IEnumerable<PokeType> strengths = pokeTypes.Where(x => x.WeaknessNaturalIDs.Contains(NaturalID));

            Strengths.Clear();
            foreach (PokeType pokeType in strengths)
            {
                Strengths.Add(pokeType);
            }
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
