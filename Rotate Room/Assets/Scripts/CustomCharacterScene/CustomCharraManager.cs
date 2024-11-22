using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class CustomCharaManager : MonoBehaviour
{
    public static CustomCharaManager instance;
    [SerializeField] CustomPlayer player;
    void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        player = GameObject.FindGameObjectWithTag("UIPlayer").GetComponent<CustomPlayer>();
    }
    public void ChangeMask(Sprite mask)
    {
        player.mask.sprite = mask;
    }
    public void ChangeColor(Colors color)
    {
        player.MainSprite.color = color.mainColor;
        foreach(Image image in player.Components)
        {
            image.color = color.subColor;
        }
    }
}
