using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] GameObject deathMenu;

    public void GameOver() { deathMenu.SetActive(true); }
}
