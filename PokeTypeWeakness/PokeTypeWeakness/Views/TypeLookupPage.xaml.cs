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

            TypeDetailViewModel typeDetailViewModel = new TypeDetailViewModel(pokeType);
            await Navigation.PushAsync(new TypeDetailPage(typeDetailViewModel));
        }

        async void StartQuiz(object sender, EventArgs e)
        {
            TypeQuizViewModel typeQuizViewModel = null;
            await Task.Run(() => { typeQuizViewModel = new TypeQuizViewModel(viewModel.PokeTypes); });
            await Navigation.PushAsync(new TypeQuizPage(typeQuizViewModel));
        }
    }
}