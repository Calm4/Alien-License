using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace App.Scripts.Monetization
{
    public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string androidGameId;
        [SerializeField] private string iosGameId;
        [SerializeField] private bool isTesting;

        private string gameId;

        private void Awake()
        {
#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
            gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = androidGameId;
#endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(gameId, isTesting, this);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads Initialized...");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            throw new NotImplementedException();
        }
    }
}