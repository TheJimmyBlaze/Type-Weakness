using PokeTypeWeakness.Models;
using PokeTypeWeakness.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeTypeWeakness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttributionPage : ContentPage
    {
        readonly AttributionViewModel viewModel;

        public AttributionPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AttributionViewModel();
        }

        async void OnItemSelected(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;
            Attribution attribution = (Attribution)layout.BindingContext;

            await Browser.OpenAsync(attribution.Address, BrowserLaunchMode.SystemPreferred);
        }
    }
}