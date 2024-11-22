using System.Collections.Generic;
using System.Linq;
using UnityEngine;

enum ItemGroupType
{
    Mask,
    Color
}

public abstract class ItemGroup : MonoBehaviour
{
    [SerializeField] ItemGroupType type;
    [SerializeField] Color imageIdle, imageHover, imageSelected;
    IvenItem selectedItem; 
    List<IvenItem> list;
    void Start()
    {
        UpdateList();
    }
    public void OnItemEnter(IvenItem item)
    {
        ResetItems();
        if(item != selectedItem) item.Background.color = imageHover;
    }

    public void OnItemExit(IvenItem item)
    {
        ResetItems();
        if(item != selectedItem) item.Background.color = imageIdle;
    }

    public void OnItemSelected(IvenItem item)
    {
        selectedItem = item;
        ResetItems();
        item.Background.color = imageSelected;
        Action(item);
    }
    public void ResetItems()
    {
        foreach(IvenItem item in list)
        {
            if(item == selectedItem) continue;
            item.Background.color = imageIdle;
        }
    }
    public void UpdateList()
    {
        list = GetComponentsInChildren<IvenItem>().ToList();
    }
    public abstract void Action(IvenItem item);
}
