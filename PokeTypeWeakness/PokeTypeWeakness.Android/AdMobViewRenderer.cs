using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Ads.Mediation.Admob;
using PokeTypeWeakness.Droid;
using PokeTypeWeakness.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace PokeTypeWeakness.Droid
{
	public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
	{
		private const string MAX_AD_CONTENT_RATING = "max_ad_content_rating";
		private const string AD_CONTENT_RATING_G = "G";

		public AdMobViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
			base.OnElementChanged(e);

			if (e.NewElement != null && Control == null)
				SetNativeControl(CreateAdView());
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(AdView.AdUnitId) && 
				string.IsNullOrEmpty(Control.AdUnitId))
				Control.AdUnitId = Element.AdUnitId;
		}

		private AdView CreateAdView()
		{
			var adView = new AdView(Context)
			{
				AdSize = AdSize.SmartBanner,
				AdUnitId = Element.AdUnitId
			};

			adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

			Bundle extras = new Bundle();
			extras.PutString(MAX_AD_CONTENT_RATING, AD_CONTENT_RATING_G);

			AdRequest.Builder builder = new AdRequest.Builder();
			builder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), extras);
			builder.TagForChildDirectedTreatment(true);

			AdRequest request = builder.Build();
			adView.LoadAd(request);

			return adView;
		}
	}
}