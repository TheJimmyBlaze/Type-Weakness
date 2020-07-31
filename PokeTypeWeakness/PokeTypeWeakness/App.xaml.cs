using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokeTypeWeakness.Services;
using PokeTypeWeakness.Views;

namespace PokeTypeWeakness
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<PokeTypeStore>();
            DependencyService.Register<AttributionStore>();
            DependencyService.Register<Random>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
