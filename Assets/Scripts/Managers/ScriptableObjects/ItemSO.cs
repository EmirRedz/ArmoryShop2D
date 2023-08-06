using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Create New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private bool useSameNameAsSprite;
    [SerializeField] private bool randomizePrice;
    [SerializeField] private bool setSellingPriceByFactorOfBuyingPrice;
    
    [ShowIf(nameof(randomizePrice))]
    [SerializeField] private Vector2Int randomPriceRange;

    [ShowIf(nameof(setSellingPriceByFactorOfBuyingPrice))]
    [SerializeField] private float sellingPriceFactor;
    
    [Space(10)]
    
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public int itemCost;
    public int sellingCost;

    private void OnValidate()
    {
        if (useSameNameAsSprite)
        {
            itemName = itemSprite.name;
        }

        if (randomizePrice)
        {
            itemCost = Random.Range(randomPriceRange.x, randomPriceRange.y);
        }

        if (setSellingPriceByFactorOfBuyingPrice)
        {
            sellingCost = Mathf.RoundToInt(itemCost * sellingPriceFactor);
        }
    }
    
    public bool IsBought()
    {
        return PlayerPrefs.GetInt(itemName, 0) == 1;
    }

    public bool IsEquipped()
    {
        return PlayerPrefs.GetInt(itemName + "Equipped") == 1;
    }

    public void EquipItem()
    {
        PlayerPrefs.SetInt(itemName + "Equipped", 1);
        PlayerPrefs.Save();
    }

    public void UnequipItem()
    {
        PlayerPrefs.SetInt(itemName + "Equipped", 0);
        PlayerPrefs.Save(); 
    }

    public void BuyItem()
    {
        PlayerPrefs.SetInt(itemName, 1);
        PlayerPrefs.Save();
    }

    public enum ItemType
    {
        Body,
        Head
    }
}
