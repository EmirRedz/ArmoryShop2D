using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemSO> myItems;
    [SerializeField] private ItemButton itemButtonPrefab;
    [SerializeField] private Transform inventory;
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
