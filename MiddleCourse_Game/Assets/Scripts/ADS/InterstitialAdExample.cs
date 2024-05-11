using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #region SerializeField

    [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";

    #endregion

    #region private

    private string _adUnitId;

    #endregion

    #region private method

    private void Awake()
    {
        // Получить идентификатор рекламного блока для текущей платформы:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOsAdUnitId : _androidAdUnitId;   
    }

    #endregion

    #region public method

    // Загрузка контента в рекламный блок:
    public void LoadAd()
    {
        Debug.Log("Загрузка рекламы: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Показывать загруженный контент в рекламном блоке:
    private void ShowAd()
    {
        Debug.Log("Показ рекламы: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Реализовать методы интерфейса Load Listener и Show Listener: 
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Показ рекламы");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // При необходимости выполнить код, если рекламный блок не загружается, например, попытайтесь повторить попытку.
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // При необходимости выполнить код, если рекламный блок не отображается, например, при загрузке другого объявления.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }

    public void OnUnityAdsShowClick(string _adUnitId) { }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }

    #endregion
}
