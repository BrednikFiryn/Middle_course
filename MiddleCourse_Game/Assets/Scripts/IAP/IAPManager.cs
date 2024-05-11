using UnityEngine;

public class IAPManager : MonoBehaviour
{
    [SerializeField] private GameObject SeniorPack;
    private CharacterData _player;

    private void Start()
    {
        _player = FindObjectOfType<CharacterData>();
    }

    public void OnPurchaseCompleted(string productID)
    {
        var item = GameObject.Instantiate(SeniorPack, _player.InventoryUIRoot.transform, false);
        Debug.Log(productID);
        Time.timeScale = 1;
    }
}
