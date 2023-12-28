using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuffWindow : Window
{
    public List<GameObject> playerBuffInfoOBject=new List<GameObject>();
    public Image foodImage;
    public override void Open()
    {
        foreach(var obj in playerBuffInfoOBject)
        {
           obj.SetActive(true);
        }
        Sprite newFoodImage = null;
        //foreach(var sprite in UIManager.Instance.roomUIManager.foodSpriteList)
        //{
        //    if(sprite.name ==PlayerBuff.Instance.buffData.food)
        //}
        //var newFoodImage 
        base.Open();
    }

    public override void Close()
    {
        foreach (var obj in playerBuffInfoOBject)
        {
            obj.SetActive(false);
        }
        base.Close();
    }
}
