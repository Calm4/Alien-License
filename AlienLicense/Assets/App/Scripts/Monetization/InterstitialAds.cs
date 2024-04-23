using UnityEngine;
using UnityEngine.Advertisements;

namespace App.Scripts.Monetization
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string androidAdUnitId;
        [SerializeField] private string iosAdUnitId;

        private string adUnitId;

        private void Awake()
        {
#if UNITY_IOS
        adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
            adUnitId = androidAdUnitId;
#endif
        }

        public void LoadInterstitialAd()
        {
            Advertisement.Load(adUnitId, this);
        }

        public void ShowInterstitialAd()
        {
            Advertisement.Show(adUnitId, this);
            LoadInterstitialAd();
        }

        #region LoadCallBacks
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Interstitial Ad Loaded!");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError("Interstitial Ad NOT Loaded!");
        }
        #endregion

        #region ShowCallBacks
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("Interstitial Ad Completed");
        }

        #endregion
    }
}