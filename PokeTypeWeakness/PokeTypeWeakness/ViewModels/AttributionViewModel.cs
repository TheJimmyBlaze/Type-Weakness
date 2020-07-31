using PokeTypeWeakness.Models;
using PokeTypeWeakness.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PokeTypeWeakness.ViewModels
{
    public class AttributionViewModel: BaseViewModel
    {
        public ObservableCollection<Attribution> Attributions { get; set; }

        public AttributionViewModel()
        {
            Title = "Attributions";
            Attributions = new ObservableCollection<Attribution>();

            ExecuteLoadTypesCommand();
        }

        private async void ExecuteLoadTypesCommand()
        {
            IDataStore<Attribution> attributionStore = DependencyService.Get<IDataStore<Attribution>>();
            IEnumerable<Attribution> attributions = await attributionStore.GetItemsAsync();

            foreach(Attribution attribution in attributions)
            {
                Attributions.Add(attribution);
            }
        }
    }
}
