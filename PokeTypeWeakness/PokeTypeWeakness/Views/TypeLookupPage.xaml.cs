using PokeTypeWeakness.Models;
using PokeTypeWeakness.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeTypeWeakness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class TypeLookupPage : ContentPage
    {
        readonly TypeLookupViewModel viewModel;

        public TypeLookupPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TypeLookupViewModel();
        }

        async void OnItemSelected(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;

            PokeType pokeType = (PokeType)layout.BindingContext;
            await pokeType.LoadStrengths();

            await Navigation.PushAsync(new TypeDetailPage(new TypeDetailViewModel(pokeType)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.PokeTypes.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}