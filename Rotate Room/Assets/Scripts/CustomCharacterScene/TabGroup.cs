using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> list;
    public Color tabIdle;
    public Color tabHover;
    public Color tabSelected;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;
    public void Start()
    {
        OnTabSelected(list[0]);
    }

    public void Subscribe(TabButton button)
    {
        if(list == null)
        {
            list = new List<TabButton>();
        }

        list.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(button != selectedTab) button.image.color = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
        if(button != selectedTab) button.image.color = tabIdle;
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.image.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectsToSwap.Count; ++i)
        {
            if(i == index) objectsToSwap[i].SetActive(true);
            else objectsToSwap[i].SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in list)
        {
            if(button == selectedTab) continue;
            button.image.color = tabIdle;
        }
    }
}
