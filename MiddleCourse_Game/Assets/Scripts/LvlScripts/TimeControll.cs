using UnityEngine;

public class TimeControll : MonoBehaviour
{
    [SerializeField] GameObject ItemPanel;

    private void Start()
    {
      ItemPanel.SetActive(false);
      Time.timeScale = 1;
    }

    public void CurrentTime()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }

        else Time.timeScale = 1;
    }
}
