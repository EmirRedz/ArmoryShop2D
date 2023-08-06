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
                }));
                break;
            case ItemSO.ItemType.Head:
                itemButton.GetBtn().onClick.AddListener((() =>
                {
                    helmetRenderer.sprite = itemToAdd.itemSprite;
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
