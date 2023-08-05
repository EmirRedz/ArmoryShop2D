using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperController : MonoBehaviour
{
    [SerializeField] private string shopName;
    [SerializeField] private List<ItemSO> items;
    
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
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
}
