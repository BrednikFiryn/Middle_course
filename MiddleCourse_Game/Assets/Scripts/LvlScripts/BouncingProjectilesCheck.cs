using UnityEngine;
using UnityEngine.UI;

public class BouncingProjectilesCheck : MonoBehaviour
{
    [SerializeField] private float perkDelay;
    private Image image;
    private ApplyPerk applyPerk;
    private float perkTime = float.MinValue;

    private void Start()
    {
        image = GetComponent<Image>();
        applyPerk = FindObjectOfType<ApplyPerk>();
    }

    void Update()
    {
        PerkCheck();
    }

    private void PerkCheck()
    {
        image.fillAmount = (Time.time - perkDelay) / perkDelay;
        if (Time.time < perkTime + perkDelay) return;
        applyPerk.perk = false;
        gameObject.SetActive(false);
        perkTime = perkDelay;
        image.fillAmount = 1f;
    }
}
