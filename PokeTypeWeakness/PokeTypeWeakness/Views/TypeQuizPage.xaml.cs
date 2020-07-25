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
    public partial class TypeQuizPage : ContentPage
    {
        readonly TypeQuizViewModel viewModel;

        public TypeQuizPage(TypeQuizViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        void Shake(VisualElement element)
        {
            new Animation {
                { 0, 0.125, new Animation (v => element.TranslationX = v, 0, -13) },
                { 0.125, 0.250, new Animation (v => element.TranslationX = v, -13, 13) },
                { 0.250, 0.375, new Animation (v => element.TranslationX = v, 13, -11) },
                { 0.375, 0.5, new Animation (v => element.TranslationX = v, -11, 11) },
                { 0.5, 0.625, new Animation (v => element.TranslationX = v, 11, -7) },
                { 0.625, 0.75, new Animation (v => element.TranslationX = v, -7, 7) },
                { 0.75, 0.875, new Animation (v => element.TranslationX = v, 7, -5) },
                { 0.875, 1, new Animation (v => element.TranslationX = v, -5, 0) }
            }
            .Commit(this, "AppleShakeChildAnimations", length: 500, easing: Easing.Linear);
        }

        void Drop(VisualElement element)
        {
            new Animation {
              { 0, .5, new Animation (v => element.TranslationY = v, -512, 0, easing: Easing.BounceOut) }
            }
            .Commit(this, "AppleIconBounceChildAnimations", length: 1000);
        }

        void ElectType(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;

            ElectablePokeType pokeType = (ElectablePokeType)layout.BindingContext;
            if (viewModel.NumWeaknesses > viewModel.NumElections ||
                pokeType.Elected)
                pokeType.Elected = !pokeType.Elected;
            else
                Shake(WeaknessLabel);
        }

        void Submit(object sender, EventArgs e)
        {
            if (!viewModel.SubmitElections())
                Shake(QuizImage);
            else
                Drop(QuizImage);
        }
    }
}