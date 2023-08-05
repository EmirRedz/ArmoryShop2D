using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Input
    private Controls input;
    private InputAction playerMovement;
    #endregion

    [Header("Movement")] 
    [SerializeField] private float movementSpeed;
    private Vector2 inputDirection;
    private Rigidbody2D rb;

    [Header("Animations")] 
    [SerializeField] private Animator playerAnimator;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Awake()
    {
        input = new Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerMovement = input.Player.Move;
        playerMovement.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
    }

    private void Update()
    {
        SetInput();
        SetRotation();
    }

    
    private void FixedUpdate()
    {
        rb.velocity = (movementSpeed * Time.deltaTime) * inputDirection;
    }
    
    private void SetInput()
    {
        inputDirection = playerMovement.ReadValue<Vector2>();
        playerAnimator.SetBool(IsMoving, rb.velocity.magnitude > 0.1);
    }
    
    private void SetRotation()
    {
        Vector3 targetRot = Vector3.zero;
        var currentRotation = playerAnimator.transform.localEulerAngles;

        if (inputDirection.x >= 0.15f)
        {
            targetRot = Vector3.up * 180;
        }
        else if (inputDirection.x <= -0.15f)
        {
            targetRot = Vector3.zero;
        }
        else if (inputDirection.x == 0)
        {
            targetRot = currentRotation;
        }

        playerAnimator.transform.localEulerAngles = Vector3.Lerp(currentRotation, targetRot, 0.25f);
    }
}
