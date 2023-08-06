using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Scene References")]
    public PlayerInventory inventory;

    [Header("Inventory")] 
    [SerializeField] private GameObject inventoryWindow;
    private bool isInventoryWindowShown;
    private void Awake()
    {
        Instance = this;
    }

    public void ToggleInventoryWindow()
    {
        isInventoryWindowShown = !isInventoryWindowShown;
        inventoryWindow.SetActive(isInventoryWindowShown);
    }
    
    
}
