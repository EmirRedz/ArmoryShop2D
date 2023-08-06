using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private List<ItemSO> myItems;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform inventory;
    private List<ItemButton> inventoryButtons = new List<ItemButton>();
    
    [Space(5)]
    
    [SerializeField] private ItemButton sellScreenButtonPrefab;
    [SerializeField] private Transform sellScreenParent;
    
    [Space(10)] 
    
    [Header("Item Update")]
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer helmetRenderer;

    [SerializeField] private Sprite defaultBody;
    [SerializeField] private Sprite defaultHelmet;

    private void Start()
    {
        for (int i = 0; i < myItems.Count; i++)
        {
            if (myItems[i].IsEquipped())
            {
                switch (myItems[i].itemType)
                {
                    case ItemSO.ItemType.Body:
                        bodyRenderer.sprite = myItems[i].itemSprite;
                        break;
                    case ItemSO.ItemType.Head:
                        helmetRenderer.sprite = myItems[i].itemSprite;
                        break;
                }
            }
        }
    }



    private void SetSellingButtons(ItemSO itemToAdd)
    {
        var sellingButton = LeanPool.Spawn(sellScreenButtonPrefab, sellScreenParent);
        sellingButton.GetBtn().interactable = true;
        sellingButton.transform.localScale = Vector3.one * 0.6f;
        sellingButton.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f).SetEase(Ease.Linear);

        sellingButton.GetBtn().onClick.RemoveAllListeners();
        sellingButton.InitShopButton(itemToAdd.itemSprite, itemToAdd.sellingCost);

        sellingButton.GetBtn().onClick.AddListener((() =>
        {
            CoinManager.Instance.AddCoin(itemToAdd.sellingCost);

            sellingButton.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                var inventoryButtonIndex = myItems.IndexOf(itemToAdd);
                itemToAdd.SetItemBoughtState(0);
                LeanPool.Despawn(inventoryButtons[inventoryButtonIndex]);
                inventoryButtons.RemoveAt(inventoryButtonIndex);

                RemoveItemFromList(itemToAdd);
                LeanPool.Despawn(sellingButton);

                ShopManager.Instance.GetShopKeeper().AddItemsToShop(itemToAdd);
            });
        }));
    }

    private void SetInventoryButton(ItemSO itemToAdd)
    {
        var itemButton = LeanPool.Spawn(itemButtonPrefab, inventory);
        itemButton.GetBtn().interactable = true;
        itemButton.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f).SetEase(Ease.Linear);
        itemButton.GetBtn().onClick.RemoveAllListeners();
        itemButton.InitShopButton(itemToAdd.itemSprite, 0);
        inventoryButtons.Add(itemButton);

        switch (itemToAdd.itemType)
        {
            case ItemSO.ItemType.Body:
                itemButton.GetBtn().onClick.AddListener((() =>
                {
                    bodyRenderer.sprite = itemToAdd.itemSprite;
                    foreach (ItemSO myItem in myItems)
                    {
                        if (myItem.itemType == ItemSO.ItemType.Body)
                        {
                            myItem.UnequipItem();
                        }
                    }

                    itemToAdd.EquipItem();
                }));
                break;
            case ItemSO.ItemType.Head:
                itemButton.GetBtn().onClick.AddListener((() =>
                {
                    helmetRenderer.sprite = itemToAdd.itemSprite;
                    foreach (ItemSO myItem in myItems)
                    {
                        if (myItem.itemType == ItemSO.ItemType.Head)
                        {
                            myItem.UnequipItem();
                        }
                    }

                    itemToAdd.EquipItem();
                }));
                break;
        }
    }

    public void AddItemToInventory(ItemSO itemToAdd)
    {
        if (myItems.Contains(itemToAdd))
        {
            return;
        }

        myItems.Add(itemToAdd);
        
        SetInventoryButton(itemToAdd);
        SetSellingButtons(itemToAdd);
    }
    
    public void RemoveItemFromList(ItemSO itemToRemove)
    {
        if (!myItems.Contains(itemToRemove))
        {
            return;
        }

        myItems.Remove(itemToRemove);
        if (!HasSpecificTypeOfArmor(ItemSO.ItemType.Head))
        {
            helmetRenderer.sprite = defaultHelmet;
        }

        if (!HasSpecificTypeOfArmor(ItemSO.ItemType.Body))
        {
            bodyRenderer.sprite = defaultBody;
        }
        
        if (myItems.Count <= 0)
        {
            bodyRenderer.sprite = defaultBody;
            helmetRenderer.sprite = defaultHelmet;
        }
    }

    private bool HasSpecificTypeOfArmor(ItemSO.ItemType itemType)
    {
        for (int i = 0; i < myItems.Count; i++)
        {
            if (myItems[i].itemType == itemType)
            {
                return true;
            }
        }

        return false;
    }
}
