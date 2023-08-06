using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopkeeperController : MonoBehaviour
{
    [SerializeField] private string shopName;
    [SerializeField] private List<ItemSO> items;
    
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        
        for (int i = items.Count - 1; i >= 0; i--)
        {
            var item = items[i];
            if (item.IsBought())
            {
                GameManager.Instance.inventory.AddItemToInventory(item);
                RemoveItemFromShop(item);
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueTrigger.TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShopManager.Instance.CloseShopWindow();
        }
        
    }

    public void ToggleShop()
    {
        ShopManager.Instance.SetUpShop(shopName, items, this);
    }
    
    [Button]
    private void RandomizeItemList()
    {
        Shuffle(items);
    }
    
    private void Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }
    }

    public void AddItemsToShop(ItemSO itemToAdd)
    {
        if (items.Contains(itemToAdd))
        {
            return;
        }
        items.Add(itemToAdd);
        ShopManager.Instance.SpawnButton(itemToAdd);
    }

    public void RemoveItemFromShop(ItemSO itemToRemove)
    {
        if (!items.Contains(itemToRemove))
        {
            return;
        }

        items.Remove(itemToRemove);
    }
}
