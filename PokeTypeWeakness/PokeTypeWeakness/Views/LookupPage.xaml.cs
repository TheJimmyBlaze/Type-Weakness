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
    public partial class LookupPage : ContentPage
    {
        LookupViewModel viewModel;

        public LookupPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LookupViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.PokeTypes.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}