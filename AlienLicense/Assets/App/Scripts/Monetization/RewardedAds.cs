using UnityEngine;
using UnityEngine.Advertisements;

namespace App.Scripts.Monetization
{
    public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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
    
        public void LoadRewardedAd()
        {
            Advertisement.Load(adUnitId, this);
        }

        public void ShowRewardedAd()
        {
            Advertisement.Show(adUnitId, this);
            LoadRewardedAd();
        }

        #region LoadCallBacks
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("RewardedAd Loaded!");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError("RewardedAd NOT Loaded!");
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
            if (placementId == adUnitId && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
            {
                Debug.Log("Ads Fully Watched");
            }
        }

        #endregion
    }
}
