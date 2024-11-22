using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ColorGroup: ItemGroup
{
	[SerializeField] GameObject item;
    public override void Action(IvenItem item)
    {
        ColorItem m_Item = (ColorItem)item;
		CustomCharaManager.instance.ChangeColor(m_Item.color);
    }
	void Start()
	{
		List<Colors> list = Resources.LoadAll<Colors>("CustomCharacter/Colors").ToList();
		foreach(Colors color in list)
		{
			GameObject maskItem = Instantiate(item, gameObject.transform);
			maskItem.GetComponent<IvenItem>().group = this;
			maskItem.GetComponent<ColorItem>().color = color;
		}
		UpdateList();
	}
}