using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemSO> myItems;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform inventory;

    [Space(5)] 
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer helmetRenderer;

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

    public void AddItemToInventory(ItemSO itemToAdd)
    {
        if (myItems.Contains(itemToAdd))
        {
            return;
        }

        myItems.Add(itemToAdd);
        var itemButton = LeanPool.Spawn(itemButtonPrefab, inventory);
        itemButton.GetBtn().onClick.RemoveAllListeners();
        itemButton.InitShopButton(itemToAdd.itemSprite, 0);
        
        
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
    
    public void RemoveItemFromList(ItemSO itemToRemove)
    {
        if (!myItems.Contains(itemToRemove))
        {
            return;
        }

        myItems.Remove(itemToRemove);
    }
    
}
