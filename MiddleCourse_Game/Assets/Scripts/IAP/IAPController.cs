using UnityEngine;

public class IAPController : MonoBehaviour
{
    public void OnPurchaseCompleted(string productID)
    {
        Debug.Log("Purchased");
    }
}
