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

    private string _adUnitId = null; // Это значение останется нулевым для неподдерживаемых платформ
    private CharacterData _player;

    #endregion

    #region private method

    private void Awake()
    {
        // Получить идентификатор рекламного блока для текущей платформы:
        _adUnitId = iOSAsUnitId;
        _adUnitId = androidAdUnitId;

        // Отключите кнопку до тех пор, пока реклама не будет готова к показу:
        showAdButton.interactable = false;
    }

    private void Start()
    {
        _player = FindObjectOfType<CharacterData>();
        Invoke("LoadAd", 1f);
    }

    private void OnDestroy()
    {
        // Очистите прослушиватели кнопок:
        showAdButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region public method

    // Вызывайте этот общедоступный метод, когда хотите подготовить рекламу к показу.
    public void LoadAd()
    {
        Debug.Log("Загружена реклама: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Реализовать метод, который будет выполняться, когда пользователь нажимает на кнопку:
    public void ShowAd()
    {
        // Отключить кнопку:
        showAdButton.interactable = false;
        // Затем покажите объявление:
        Advertisement.Show(_adUnitId, this);
    }

    // Если реклама успешно загрузилась, добавьте прослушиватель к кнопке и включите его:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Загруженна реклама: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Настройте кнопку так, чтобы при нажатии на нее вызывался метод ShowAd():
            showAdButton.onClick.AddListener(ShowAd);
            // Включить кнопку, по которой пользователи могут нажимать:
            showAdButton.interactable = true;
        }
    }

    // Реализовать загрузку и отображение обратных вызовов с ошибками прослушивателя:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Ошибка при загрузке рекламного блока {adUnitId}: {error.ToString()} - {message}");
        // Используйте сведения об ошибке, чтобы определить, следует ли пытаться загрузить другое объявление.
    }

    // Реализуйте метод обратного вызова OnUnityAdsShowComplete для прослушивателя Show, чтобы определить, получит ли пользователь вознаграждение:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Завершено рекламное объявление с вознаграждением Unity Ads");
            var item = Object.Instantiate(healthPack, _player.InventoryUIRoot.transform, false);
            LoadAd();
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Ошибка при показе рекламного блока {adUnitId}: {error.ToString()} - {message}");
        // Используйте сведения об ошибке, чтобы определить, следует ли пытаться загрузить другое объявление.
    }


    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }

    #endregion
}
