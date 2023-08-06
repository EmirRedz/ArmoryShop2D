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
    [SerializeField] private GameObject shopHolder;
    [SerializeField] private TMP_Text shopNameText;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform buttonHolder;

    private List<ItemButton> currentShopItems = new List<ItemButton>();
    private ShopkeeperController currentShopKeeper;
    private void Awake()
    {
        Instance = this;
    }

    public void SetUpShop(string shopName, List<ItemSO> items, ShopkeeperController shopKeeper)
    {
        currentShopKeeper = shopKeeper;
        shopNameText.SetText(shopName);

        for (int i = 0; i < items.Count; i++)
        {
            var index = i;

            SpawnButton(items[index]);
        }
        
        CheckIfCanBuy();
    }

    public void CloseShopWindow()
    {
        foreach (ItemButton currentShopItem in currentShopItems)
        {
            currentShopItem.GetBtn().onClick.RemoveAllListeners();
            LeanPool.Despawn(currentShopItem);
        }
        currentShopItems.Clear();
        shopHolder.SetActive(false);
    }


    public void SpawnButton(ItemSO item)
    {
        var button = LeanPool.Spawn(itemButtonPrefab, buttonHolder);
        button.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f).SetEase(Ease.Linear);

        button.InitShopButton(item.itemSprite, item.itemCost);
        currentShopItems.Add(button);
        button.GetBtn().onClick.AddListener((() =>
        {
            SetupButtonClickEvent(button.ItemCost(), item);
            button.GetBtn().transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear)
                .OnComplete((() => LeanPool.Despawn(button)));
            currentShopItems.Remove(button);
            
            currentShopKeeper.RemoveItemFromShop(item);
        }));
    }
    
    private void SetupButtonClickEvent(int cost, ItemSO itemToAdd)
    {
        CoinManager.Instance.RemoveCoin(cost);
        GameManager.Instance.inventory.AddItemToInventory(itemToAdd);
        itemToAdd.SetItemBoughtState(1);
    }

    public void CheckIfCanBuy()
    {
        for (int i = 0; i < currentShopItems.Count; i++)
        {
            currentShopItems[i].GetBtn().interactable =
                currentShopItems[i].ItemCost() <= CoinManager.Instance.CurrentCoinAmount();
        }
    }

    public ShopkeeperController GetShopKeeper()
    {
        return currentShopKeeper;
    }
}
