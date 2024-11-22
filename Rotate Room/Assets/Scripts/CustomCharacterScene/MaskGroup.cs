using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class MaskGroup: ItemGroup
{
	[SerializeField] GameObject item;
	public override void Action(IvenItem item)
	{
		MaskItem m_Item = (MaskItem)item;
		CustomCharaManager.instance.ChangeMask(m_Item.mask);
	}
	void Start()
	{
		List<Sprite> list = Resources.LoadAll<Sprite>("CustomCharacter/Masks").ToList();
		foreach(Sprite mask in list)
		{
			GameObject maskItem = Instantiate(item, gameObject.transform);
			maskItem.GetComponent<IvenItem>().group = this;
			maskItem.GetComponent<MaskItem>().mask = mask;
		}
		UpdateList();
	}
}