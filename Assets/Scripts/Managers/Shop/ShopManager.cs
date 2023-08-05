using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    
    [SerializeField] private TMP_Text shopNameText;
    [SerializeField] private ShopButton shopButtonPrefab;
    [SerializeField] private Transform buttonHolder;

    private List<ShopButton> currentShopItems = new List<ShopButton>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetUpShop(string shopName, List<ItemSO> items)
    {
        if (currentShopItems.Count > 0)
        {
            foreach (ShopButton currentShopItem in currentShopItems)
            {
                LeanPool.Despawn(currentShopItem);
            }
            
            currentShopItems.Clear();
        }
        
        shopNameText.SetText(shopName);

        for (int i = 0; i < items.Count; i++)
        {
            var button = LeanPool.Spawn(shopButtonPrefab, buttonHolder);
            button.InitShopButton(items[i].itemSprite, items[i].itemCost);
        }
    }
}
