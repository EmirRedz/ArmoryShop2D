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

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Start()
    {
        CursorManager.Instance.ToggleCursor(false);
    }

    public void ToggleInventoryWindow()
    {
        isInventoryWindowShown = !isInventoryWindowShown;
        inventoryWindow.SetActive(isInventoryWindowShown);
        CursorManager.Instance.ToggleCursor(isInventoryWindowShown);
    }
    
    
}
