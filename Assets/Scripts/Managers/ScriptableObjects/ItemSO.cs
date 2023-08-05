using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Create New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private bool useSameNameAsSprite;
    
    public Sprite itemSprite;
    public ItemType itemType;
    public int itemCost;

    private void OnValidate()
    {
        if (useSameNameAsSprite)
        {
            name = itemSprite.name;
        }
    }

    public enum ItemType
    {
        Body,
        Head,
        RightArm,
        LeftArm,
        RightLeg,
        LeftLeg
    }
}
