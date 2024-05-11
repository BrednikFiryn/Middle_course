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
        // �������� ������������� ���������� ����� ��� ������� ���������:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOsAdUnitId : _androidAdUnitId;   
    }

    #endregion

    #region public method

    // �������� �������� � ��������� ����:
    public void LoadAd()
    {
        Debug.Log("�������� �������: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // ���������� ����������� ������� � ��������� �����:
    private void ShowAd()
    {
        Debug.Log("����� �������: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // ����������� ������ ���������� Load Listener � Show Listener: 
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("����� �������");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // ��� ������������� ��������� ���, ���� ��������� ���� �� �����������, ��������, ����������� ��������� �������.
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // ��� ������������� ��������� ���, ���� ��������� ���� �� ������������, ��������, ��� �������� ������� ����������.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }

    public void OnUnityAdsShowClick(string _adUnitId) { }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }

    #endregion
}
