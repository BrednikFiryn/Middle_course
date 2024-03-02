
using UnityEngine;

public class TimeControll : MonoBehaviour
{
    private void Start()
    {
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
