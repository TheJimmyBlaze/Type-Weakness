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
    }
}