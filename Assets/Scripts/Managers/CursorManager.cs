using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    
    [SerializeField] private GameObject cursorGO;
    [SerializeField] private Vector3 cursorOffset;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!cursorGO.activeInHierarchy)
        {
            return;
        }
        cursorGO.transform.position = Input.mousePosition + cursorOffset;
    }

    public void ToggleCursor(bool toggle)
    {
        cursorGO.SetActive(toggle);
    }
}
