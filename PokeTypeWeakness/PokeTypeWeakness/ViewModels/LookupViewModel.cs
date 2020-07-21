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
    public class LookupViewModel: BaseViewModel
    {
        public ObservableCollection<PokeType> PokeTypes { get; set; }
        public Command LoadTypesCommand { get; set; }

        public LookupViewModel()
        {
            Title = "Weaknesses";
            PokeTypes = new ObservableCollection<PokeType>();
            LoadTypesCommand = new Command(async () => await ExecuteLoadTypesCommand());
        }

        private async Task ExecuteLoadTypesCommand()
        {
            IsBusy = true;

            try
            {
                PokeTypes.Clear();
                IEnumerable<PokeType> pokeTypes = await DataStore.GetItemsAsync();
                foreach (PokeType pokeType in pokeTypes)
                {
                    await pokeType.LoadWeaknesses();
                    PokeTypes.Add(pokeType);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
