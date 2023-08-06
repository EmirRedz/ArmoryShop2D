using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    
    [SerializeField] private TMP_Text shopNameText;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform buttonHolder;

    private List<ItemButton> currentShopItems = new List<ItemButton>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetUpShop(string shopName, List<ItemSO> items)
    {
        if (currentShopItems.Count > 0)
        {
            foreach (ItemButton currentShopItem in currentShopItems)
            {
                currentShopItem.GetBtn().onClick.RemoveAllListeners();
                LeanPool.Despawn(currentShopItem);
            }
            
            currentShopItems.Clear();
        }
        
        shopNameText.SetText(shopName);

        for (int i = 0; i < items.Count; i++)
        {
            var index = i;

            var button = LeanPool.Spawn(itemButtonPrefab, buttonHolder);
            button.InitShopButton(items[i].itemSprite, items[i].itemCost);
            button.GetBtn().onClick.AddListener((() =>
            {
                SetupButtonClickEvent(button.ItemCost(), items[index]);
                button.GetBtn().transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear)
                    .OnComplete(() => LeanPool.Despawn(button));
            }));
        }
    }

    private void SetupButtonClickEvent(int cost, ItemSO itemToAdd)
    {
        CoinManager.Instance.RemoveCoin(cost);
        GameManager.Instance.inventory.AddItemToInventory(itemToAdd);
        itemToAdd.BuyItem();
        CheckIfCanBuy();
    }

    public void CheckIfCanBuy()
    {
        for (int i = 0; i < currentShopItems.Count; i++)
        {

            currentShopItems[i].GetBtn().interactable =
                currentShopItems[i].ItemCost() > CoinManager.Instance.CurrentCoinAmount();

        }
    }
}
