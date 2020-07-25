using PokeTypeWeakness.Models;
using PokeTypeWeakness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeTypeWeakness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TypeDetailPage : ContentPage
    {
        readonly TypeDetailViewModel viewModel;

        public TypeDetailPage(TypeDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnItemSelected(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;

            PokeType pokeType = (PokeType)layout.BindingContext;
            await pokeType.LoadStrengths();

            Page previousPage = Navigation.NavigationStack.Last();
            await Navigation.PushAsync(new TypeDetailPage(new TypeDetailViewModel(pokeType)));
            Navigation.RemovePage(previousPage);
        }
    }
}