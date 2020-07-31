using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokeTypeWeakness.ViewModels
{
    public class TypeLookupViewModel: BaseViewModel
    {
        public ObservableCollection<PokeType> PokeTypes { get; set; }

        public TypeLookupViewModel()
        {
            Title = "Weakness Summary";
            PokeTypes = new ObservableCollection<PokeType>();
            ExecuteLoadTypesCommand();
        }

        private async void ExecuteLoadTypesCommand()
        {
            IEnumerable<PokeType> pokeTypes = await DataStore.GetItemsAsync();
            foreach (PokeType pokeType in pokeTypes)
            {
                await pokeType.LoadWeaknesses();
                PokeTypes.Add(pokeType);
            }
        }
    }
}
