using UnityEngine.Advertisements;
using UnityEngine;
using System;

public class AdInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    #region SerializeField
    [SerializeField] string androidGameID = "5613722";
    [SerializeField] string iOSGameID = "5613723";
    [SerializeField] bool testMode = true;
    #endregion

    #region private

    private string _gameID;

    #endregion

    #region private method

    private void Awake()
    {
        InitializeAds();
    }

    #endregion

    #region public method

    /// <summary>
    /// Инициализация рекламы
    /// </summary>
    public void InitializeAds()
    {
        _gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSGameID : androidGameID;
        Advertisement.Initialize(_gameID, testMode, this);
    }

    /// <summary>
    /// Инициализация прошла успешно
    /// </summary>
    public void OnInitializationComplete()
    {
        Debug.Log("Инициализация Unity Ads завершена.");
    }

    /// <summary>
    /// Инициализация рекламы не удалась
    /// </summary>
    /// <param name="error"></param>
    /// <param name="message"></param>
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Не удалось выполнить инициализацию Unity Ads: {error.ToString()} - {message}");
    }

    #endregion
}
