using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using SaveLoad;

using GoogleMobileAds.Api;

public class Interstitiales : MonoBehaviour {

	private static Interstitiales instance;
	private InterstitialAd inter;

	private string AdInterstitialID;

	AdRequest request;

	public void Start()
	{
		SaveLoadManager sLManager = GameObject.Find ("SaveLoadManager").GetComponent<SaveLoadManager>();

		if (sLManager.pData.ads) {
   			if (Interstitiales.instance == null) {
				Interstitiales.instance = this;
				DontDestroyOnLoad (gameObject);
			} else if (Interstitiales.instance != null) {
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

			this.RequestIntestitial ();

		} else {
			Debug.Log ("No ads");
		}
	}
    

	private void RequestIntestitial()
	{
		#if UNITY_ANDROID
		this.AdInterstitialID = "ca-app-pub-6352970493438685/1470617434";
		#elif UNITY_IPHONE
		this.AdInterstitialID = "ca-app-pub-6352970493438685/9448425823";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		this.request = new AdRequest.Builder().Build();
	}
	
	public void loadInter()
	{
		this.inter = new InterstitialAd (this.AdInterstitialID);

		// Called when an ad request has successfully loaded.
		this.inter.OnAdLoaded += HandleOnAdLoaded;

		// Called when an ad request failed to load.
		this.inter.OnAdFailedToLoad += HandleOnAdFailedToLoad;

		// Called when an ad is shown.
		this.inter.OnAdOpening += HandleOnAdOpened;

		// Called when the ad is closed.
		this.inter.OnAdClosed += HandleOnAdClosed;

		this.inter.LoadAd (this.request);
	}

	public void showIntestitial()
    {
		if(this.inter.IsLoaded()){
			this.inter.Show ();
		}
	}


	/*void OnDisable(){
		/*try{
			this.inter.Destroy ();
		}
		catch(NullReferenceException e) {
		}
	}*/
		public void HandleOnAdLoaded(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleAdLoaded event received");
		}

		public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
		+ args.Message);
		}

		public void HandleOnAdOpened(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleAdOpened event received");
		}

		public void HandleOnAdClosed(object sender, EventArgs args)
		{
		    this.inter.Destroy ();
		}

		public void HandleOnAdLeftApplication(object sender, EventArgs args)
		{
		MonoBehaviour.print("HandleAdLeftApplication event received");
		}

}
