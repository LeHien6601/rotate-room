using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private int currentUnlockedLevel;
    [SerializeField] private int maxLevel;
    [SerializeField] private GameObject levelBtnPref;
    private List<Button> levels;
    private void Awake()
    {
        levels = new List<Button>();
        maxLevel = GameManager.instance.maxLevel;
        for (int i = 0; i < maxLevel; i++)
        {
            Instantiate(levelBtnPref, transform);
            levels.Add(transform.GetChild(i).GetComponent<Button>());
            levels[i].interactable = (i < currentUnlockedLevel);
            int j = i;
            levels[i].onClick.AddListener(() => { GameManager.instance.LoadLevel(j + 1); });
            levels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }
    }
    public void UnlockLevel(int level)
    {
        if (level == 0) return;
        levels[level - 1].interactable = true;
        if (level > currentUnlockedLevel)
        {
            currentUnlockedLevel = level;
        }
    }
}
