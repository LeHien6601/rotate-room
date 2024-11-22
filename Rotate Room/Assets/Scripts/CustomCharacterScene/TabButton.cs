using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Image image;
    public TabGroup group;

    void Start()
    {
        image = GetComponent<Image>();
        // group.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        group.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        group.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        group.OnTabExit(this);
    }
}
