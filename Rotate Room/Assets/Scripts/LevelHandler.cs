using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private int currentUnlockedLevel;
    private List<Button> levels;
    private void Awake()
    {
        levels = new List<Button>();
        for (int i = 0; i < transform.childCount; i++)
        {
            levels.Add(transform.GetChild(i).GetComponent<Button>());
            levels[i].interactable = false;
        }
        for (int i = 0; i < currentUnlockedLevel; i++)
        {
            levels[i].interactable = true;
        }
    }
    public void UnlockLevel(int level)
    {
        levels[level - 1].interactable = true;
        if (level > currentUnlockedLevel)
        {
            currentUnlockedLevel = level;
        }
    }
}
