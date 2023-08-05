using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemCostText;

    public void InitShopButton(Sprite itemSprite, int itemCost)
    {
        itemImage.sprite = itemSprite;
        itemImage.SetNativeSize();
        itemCostText.SetText(itemCost.ToString());
    }
}
