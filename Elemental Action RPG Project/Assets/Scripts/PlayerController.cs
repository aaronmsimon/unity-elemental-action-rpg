using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Reference Variables
    private PlayerInput playerInput;
    private CharacterController characterController;

    // Player Input Values
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 currentRunMovement;
    private bool isMovementPressed;
    private bool isRunPressed;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;

    private void Awake()
    {
        // Set Reference Variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRun;
        playerInput.CharacterControls.Run.canceled += OnRun;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * walkSpeed;
        currentMovement.y = 0;
        currentMovement.z = 0;
        currentRunMovement.x = currentMovementInput.x * runSpeed;
        currentRunMovement.y = 0;
        currentRunMovement.z = 0;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        } else
        {
            float gravity = -9.81f;
            currentMovement.y = gravity;
            currentRunMovement.y = gravity;
        }
    }

    private void Update()
    {
        HandleGravity();

        characterController.Move((isRunPressed ? currentRunMovement : currentMovement) * Time.deltaTime);
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
