using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IvenItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] protected Image Foreground;
    public Image Background;
    public ItemGroup group;
    public void OnPointerClick(PointerEventData eventData)
    {
        group.OnItemSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        group.OnItemEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        group.OnItemExit(this);
    }
    //TODO: ScriptableObjects lel
}
