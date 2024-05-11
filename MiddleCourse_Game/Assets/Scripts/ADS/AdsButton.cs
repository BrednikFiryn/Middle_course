using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button buttonAds;
    [SerializeField] string androidRewardID = "Rewarded_Android";
    [SerializeField] string iOSRewardID = "Rewarded_iOS";
    [SerializeField] GameObject healthPack;

    private string rewardedVideo;
    private CharacterData _player;

    private void Awake()
    {
        rewardedVideo = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSRewardID : androidRewardID;
        buttonAds.interactable = false;
    }

    private void Start()
    {
        buttonAds.onClick.AddListener(ShowAd);
        _player = FindObjectOfType<CharacterData>();
        Invoke("LoadAd", 1f);
    }

    private void LoadAd()
    {
        Debug.Log("Загрузилась реклама: " + rewardedVideo);
        Advertisement.Load(rewardedVideo, this);
    }

    public void ShowAd()
    {
        buttonAds.interactable = false;
        Time.timeScale = 0;
        Advertisement.Show(rewardedVideo, this);
    }

    /// <summary>
    /// реклама загрузилась
    /// </summary>
    /// <param name="placementId"></param>
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Реклама загрузилась: " + placementId);

        if (placementId.Equals(rewardedVideo))
        {
            buttonAds.interactable = true;
        }
    }

    /// <summary>
    /// Реклама закончилась
    /// </summary>
    /// <param name="placementId"></param>
    /// <param name="showCompletionState"></param>
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Показ рекламы окончен");
        var item = GameObject.Instantiate(healthPack, _player.InventoryUIRoot.transform, false);
        Time.timeScale = 1;
        LoadAd();
    }

    /// <summary>
    /// Очистить все подписки кнопки
    /// </summary>
    private void OnDestroy()
    {
        buttonAds.onClick.RemoveAllListeners();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}
