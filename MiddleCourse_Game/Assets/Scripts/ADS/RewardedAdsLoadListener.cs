using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsLoadListener : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #region SerializeField

    [SerializeField] Button showAdButton;
    [SerializeField] string androidAdUnitId = "Rewarder_Android";
    [SerializeField] string iOSAsUnitId = "Rewarder_iOS";
    [SerializeField] GameObject healthPack;

    #endregion

    #region private

    private string _adUnitId = null; // ��� �������� ��������� ������� ��� ���������������� ��������
    private CharacterData _player;

    #endregion

    #region private method

    private void Awake()
    {
        // �������� ������������� ���������� ����� ��� ������� ���������:
        _adUnitId = iOSAsUnitId;
        _adUnitId = androidAdUnitId;

        // ��������� ������ �� ��� ���, ���� ������� �� ����� ������ � ������:
        showAdButton.interactable = false;
    }

    private void Start()
    {
        _player = FindObjectOfType<CharacterData>();
        Invoke("LoadAd", 1f);
    }

    private void OnDestroy()
    {
        // �������� �������������� ������:
        showAdButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region public method

    // ��������� ���� ������������� �����, ����� ������ ����������� ������� � ������.
    public void LoadAd()
    {
        Debug.Log("��������� �������: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // ����������� �����, ������� ����� �����������, ����� ������������ �������� �� ������:
    public void ShowAd()
    {
        // ��������� ������:
        showAdButton.interactable = false;
        // ����� �������� ����������:
        Advertisement.Show(_adUnitId, this);
    }

    // ���� ������� ������� �����������, �������� �������������� � ������ � �������� ���:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("���������� �������: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // ��������� ������ ���, ����� ��� ������� �� ��� ��������� ����� ShowAd():
            showAdButton.onClick.AddListener(ShowAd);
            // �������� ������, �� ������� ������������ ����� ��������:
            showAdButton.interactable = true;
        }
    }

    // ����������� �������� � ����������� �������� ������� � �������� ��������������:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"������ ��� �������� ���������� ����� {adUnitId}: {error.ToString()} - {message}");
        // ����������� �������� �� ������, ����� ����������, ������� �� �������� ��������� ������ ����������.
    }

    // ���������� ����� ��������� ������ OnUnityAdsShowComplete ��� �������������� Show, ����� ����������, ������� �� ������������ ��������������:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("��������� ��������� ���������� � ��������������� Unity Ads");
            var item = Object.Instantiate(healthPack, _player.InventoryUIRoot.transform, false);
            LoadAd();
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"������ ��� ������ ���������� ����� {adUnitId}: {error.ToString()} - {message}");
        // ����������� �������� �� ������, ����� ����������, ������� �� �������� ��������� ������ ����������.
    }


    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }

    #endregion
}
