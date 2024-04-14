using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private int scoreToNextLevel = 20;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int score = 0;

    private List<IItem> iItem;

    public GameObject InventoryUIRoot;
    public List<MonoBehaviour> levelUpActions;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        InventoryUIRoot = GameObject.FindGameObjectWithTag("ItemPanel");
    }

    public void Score (int scoreAmount)
    {
        score += scoreAmount;
        if (score >= scoreToNextLevel)
        {
            LevelUp();
            score = 0;
        }
    }

    private void LevelUp()
    {
      currentLevel++;
      scoreToNextLevel *= 2;
      foreach (var action in levelUpActions)
        {
            if (!(action is ILevelUp levelUp)) return;
            levelUp.levelUp(this, currentLevel);
        }
    }
}
