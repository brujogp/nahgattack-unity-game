using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

using SaveLoad;

using GoogleMobileAds.Api;

//using admob;

public class Banner : MonoBehaviour {

	private static Banner ads;
	private BannerView banner;

	private string AdBannerID;

	AdRequest request;

	public void Awake()
	{
		SaveLoadManager sLManager = GameObject.Find ("SaveLoadManager").GetComponent<SaveLoadManager>();

		if (sLManager.pData.ads) {

			if (Banner.ads == null) {
				Banner.ads = this;
				DontDestroyOnLoad (gameObject);
			} else if (Banner.ads != null) {
				Destroy (gameObject);
			}

			#if UNITY_ANDROID
			string appId = "ca-app-pub-6352970493438685~4278697884";
			#elif UNITY_IPHONE
			string appId = "ca-app-pub-6352970493438685~1682558394";
			#else
		string appId = "unexpected_platform";
			#endif

			// Initialize the Google Mobile Ads SDK.
			MobileAds.Initialize (appId);

			this.RequestBanner ();
		} else {
			Debug.Log ("No ads");
		}
	}


	private void RequestBanner()
	{
		#if UNITY_ANDROID
		this.AdBannerID = "ca-app-pub-6352970493438685/8980005009";
		#elif UNITY_IPHONE
		this.AdBannerID = "ca-app-pub-6352970493438685/9467772975";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		this.request = new AdRequest.Builder().Build();
	}
	


	public void showBanner()
	{
		// Create a 320x50 banner at the top of the screen.
		this.banner = new BannerView(AdBannerID, AdSize.Banner, AdPosition.Top);
		this.banner.LoadAd(this.request);
	}

	public void destroyBanner()
	{
		this.banner.Destroy ();
	}
}
