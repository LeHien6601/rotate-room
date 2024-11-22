using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class MaskItem: IvenItem
{
	public Sprite mask;
	void Start()
	{
		Foreground.sprite = mask;
	}
}