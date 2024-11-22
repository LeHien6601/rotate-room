using UnityEngine;
using UnityEngine.UI;

public class ColorItem: IvenItem
{
	public Colors color;
	void Start()
	{
		Foreground.color = color.mainColor;
	}
}