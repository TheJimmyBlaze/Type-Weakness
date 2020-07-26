using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PokeTypeWeakness.Droid
{
    [Activity(Label = "Pokemon Type Weaknesses", Icon = "@drawable/pokemon_icon", Theme = "@style/MainTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            OverrideUI();

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            OverrideUI();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void OverrideUI()
        {
            View decorView = Window.DecorView;
            int uiOptions = (int)decorView.SystemUiVisibility;
            int newUiOptions = (int)uiOptions;

            newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;
            newUiOptions |= (int)SystemUiFlags.LowProfile;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;

            decorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }
    }
}