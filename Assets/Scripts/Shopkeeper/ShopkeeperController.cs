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
                items.Remove(item);
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

    public void ToggleShop()
    {
        ShopManager.Instance.SetUpShop(shopName, items);
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
}
