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

    [ShowIf(nameof(randomizePrice))]
    [SerializeField] private Vector2Int randomPriceRange;
    
    [Space(10)]
    
    public string itemName;
    public Sprite itemSprite;
    public ItemType itemType;
    public int itemCost;

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
    }

    [SerializeField] private bool isBought;
    private void Awake()
    {
        isBought = IsBought();
    }

    public bool IsBought()
    {
        return PlayerPrefs.GetInt(itemName, 0) == 1;
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
