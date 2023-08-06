using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemCostText;
    [SerializeField] private Button itemButton;
    public void InitShopButton(Sprite itemSprite, int itemCost)
    {
        itemImage.sprite = itemSprite;
        itemImage.SetNativeSize();
        itemCostText.SetText(itemCost.ToString());
    }

    public int ItemCost()
    {
        return int.Parse(itemCostText.text);
    }

    public Button GetBtn()
    {
        return itemButton;
    }
}
