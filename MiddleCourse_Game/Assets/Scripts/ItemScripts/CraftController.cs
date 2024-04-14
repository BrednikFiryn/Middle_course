using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftController : MonoBehaviour
{
    private List<ICraftable> _items = new List<ICraftable>();
    private List<GameObject> _selected = new List<GameObject>();

    public Transform uIItemsRoot;
    public CraftSettings craftSettings;
    public bool activeCraft;

    public void EnterCraftMode()
    {
        _selected.Clear();
        _items = GetComponentsInChildren<ICraftable>().ToList();
        foreach (var  item in _items)
        {
            var button = ((MonoBehaviour)item)?.gameObject.AddComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { Select(button.gameObject); });
        }
    }

    private void Select(GameObject item)
    {
       if (_selected.Contains(item))
        {
            _selected.Remove(item);
            item.GetComponent<Image>().color = new Color(1, 1, 1);
        }

       else
        {
            _selected.Add(item);
            item.GetComponent<Image>().color = new Color(1, 0.5f, 0.7f);
        }

        CheckCombo();
    }

    private void CheckCombo()
    {
      List<string> selectedNames = new List<string>();
      foreach (var item in _selected)
        {
            var itemName = item.GetComponent<ICraftable>().Name;
            selectedNames.Add(itemName);
        }
      foreach (var combination in craftSettings.combinations)
        {
            if (combination.sources.SequenceEqual(selectedNames))
            {
                combination.sources.Sort();
                foreach (var recipe in _selected)
                {
                    Destroy(recipe);
                }

                var newItem = Instantiate(combination.result, uIItemsRoot);
            }
        }
    }
}
