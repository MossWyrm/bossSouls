using System;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CustomPlayerInputManager : MonoBehaviour
{
    public static CustomPlayerInputManager Instance;

    public Vector2 MoveVector { get; private set; }
    public Vector2 LookVector { get; private set; }

    public PlayerInputActions Actions;

    [Header("Actions")] 
    public static Action MovePerformed;
    public static Action MoveCanceled;
    public static Action LookPerformed;
    public static Action LookCanceled;
    public static Action AbilityOnePerformed;
    public static Action AbilityOneCanceled;
    public static Action AbilityTwoPerformed;
    public static Action AbilityTwoCanceled;
    public static Action AbilityThreePerformed;
    public static Action AbilityThreeCanceled;
    public static Action SprintPerformed;
    public static Action SprintCanceled;
    public static Action DodgePerformed;
    public static Action DodgeCanceled;
    public static Action JumpPerformed;
    public static Action JumpCanceled;
    public static Action TransformNextPerformed;
    public static Action TransformNextCanceled;
    public static Action TransformPreviousPerformed;
    public static Action TransformPreviousCanceled;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        SetupInputsOnEnable();
    }

    private void OnDisable()
    {
        SetupInputsOnDisable();
    }

    private void SetupInputsOnEnable()
    {
        Actions = new PlayerInputActions();
        Actions.Enable();

        Actions.Player.Move.performed += OnMovePerformed;
        Actions.Player.Move.canceled += OnMoveCanceled;
        Actions.Player.Look.performed += OnLookPerformed;
        Actions.Player.Look.canceled += OnLookCanceled;
        Actions.Player.AbilityOne.performed += OnAbilityOnePerformed;
        Actions.Player.AbilityOne.canceled += OnAbilityOneCanceled;
        Actions.Player.AbilityTwo.performed += OnAbilityTwoPerformed;
        Actions.Player.AbilityTwo.canceled += OnAbilityTwoCanceled;
        Actions.Player.AbilityThree.performed += OnAbilityThreePerformed;
        Actions.Player.AbilityThree.canceled += OnAbilityThreeCanceled;
        Actions.Player.Jump.performed += OnJumpPerformed;
        Actions.Player.Jump.canceled += OnJumpCanceled;
        Actions.Player.Sprint.performed += OnSprintPerformed;
        Actions.Player.Sprint.canceled += OnSprintCanceled;
        Actions.Player.Dodge.performed += OnDodgePerformed;
        Actions.Player.Dodge.canceled += OnDodgeCanceled;
        Actions.Player.TransformNext.performed += OnTransformNextPerformed;
        Actions.Player.TransformNext.canceled += OnTransformNextCanceled;
        Actions.Player.TransformPrevious.performed += OnTransformPreviousPerformed;
        Actions.Player.TransformPrevious.canceled += OnTransformPreviousCanceled;
        
    }

    private void SetupInputsOnDisable()
    {
        Actions.Player.Move.performed -= OnMovePerformed;
        Actions.Player.Move.canceled -= OnMoveCanceled;
        Actions.Player.Look.performed -= OnLookPerformed;
        Actions.Player.Look.canceled -= OnLookCanceled;
        Actions.Player.AbilityOne.performed -= OnAbilityOnePerformed;
        Actions.Player.AbilityOne.canceled -= OnAbilityOneCanceled;
        Actions.Player.AbilityTwo.performed -= OnAbilityTwoPerformed;
        Actions.Player.AbilityTwo.canceled -= OnAbilityTwoCanceled;
        Actions.Player.AbilityThree.performed -= OnAbilityThreePerformed;
        Actions.Player.AbilityThree.canceled -= OnAbilityThreeCanceled;
        Actions.Player.Jump.performed -= OnJumpPerformed;
        Actions.Player.Jump.canceled -= OnJumpCanceled;
        Actions.Player.Sprint.performed -= OnSprintPerformed;
        Actions.Player.Sprint.canceled -= OnSprintCanceled;
        Actions.Player.Dodge.performed -= OnDodgePerformed;
        Actions.Player.Dodge.canceled -= OnDodgeCanceled;
        Actions.Player.TransformNext.performed -= OnTransformNextPerformed;
        Actions.Player.TransformNext.canceled -= OnTransformNextCanceled;
        Actions.Player.TransformPrevious.performed -= OnTransformPreviousPerformed;
        Actions.Player.TransformPrevious.canceled -= OnTransformPreviousCanceled;
        Actions.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveVector = context.ReadValue<Vector2>();
        MovePerformed?.Invoke();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveVector = Vector2.zero;
        MoveCanceled?.Invoke();
    }
    
    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        LookVector = context.ReadValue<Vector2>();
        LookPerformed?.Invoke();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        LookVector = Vector2.zero;
        LookCanceled?.Invoke();
    }

    private void OnAbilityOnePerformed(InputAction.CallbackContext context)
    {
        AbilityOnePerformed?.Invoke();
    }
    private void OnAbilityOneCanceled(InputAction.CallbackContext context)
    {
        AbilityOneCanceled?.Invoke();
    }
    private void OnAbilityTwoPerformed(InputAction.CallbackContext context)
    {
        AbilityTwoPerformed?.Invoke();
    }   
    private void OnAbilityTwoCanceled(InputAction.CallbackContext context)
    {
        AbilityTwoCanceled?.Invoke();
    }
    private void OnAbilityThreePerformed(InputAction.CallbackContext context)
    {
        AbilityThreePerformed?.Invoke();
    }
    private void OnAbilityThreeCanceled(InputAction.CallbackContext context)
    {
        AbilityThreeCanceled?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpPerformed?.Invoke();
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        JumpCanceled?.Invoke();
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        SprintPerformed?.Invoke();
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        SprintCanceled?.Invoke();
    }

    private void OnDodgePerformed(InputAction.CallbackContext context)
    {
        DodgePerformed?.Invoke();
    }
    private void OnDodgeCanceled(InputAction.CallbackContext context)
    {
        DodgeCanceled?.Invoke();
    }

    private void OnTransformNextPerformed(InputAction.CallbackContext context)
    {
        TransformNextPerformed?.Invoke();
    }
    
    private void OnTransformNextCanceled(InputAction.CallbackContext context)
    {
        TransformNextCanceled?.Invoke();
    }

    private void OnTransformPreviousPerformed(InputAction.CallbackContext context)
    {
        TransformPreviousPerformed?.Invoke();
    }
    
    private void OnTransformPreviousCanceled(InputAction.CallbackContext context)
    {
        TransformPreviousCanceled?.Invoke();
    }
    

    //
    // private void FixedUpdate()
    // {
    //     MovePlayer(_moveDirection);
    //     // RotatePlayer(_lookDirection);
    // }
    //
    // private void RotatePlayer(Vector2 targetDirection)
    // {
    //     _gameOptions ??= GameManager.Instance.gameOptions;
    //     
    //     _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0, targetDirection.x * _gameOptions.xSensitivity, 0));
    // }
    //
    // private void MovePlayer(Vector2 directions)
    // {
    //     if(player.Stats == null) return;
    //     var speed = player.Stats.speed;
    //     speed *= _sprinting ? player.Stats.sprintMultiplier : 1;
    //     
    //     var cameraForward = _camera.transform.forward;
    //     var cameraRight = _camera.transform.right;
    //     cameraForward.y = 0;
    //     cameraRight.y = 0;
    //     
    //     var movement = new Vector3(0.0f, 0.0f, 0.0f) + (directions.y * cameraForward) + (directions.x * cameraRight);
    //     
    //     print(movement);
    //     if(movement != Vector3.zero)
    //     {
    //         _rb.transform.LookAt(_rb.transform.position + movement);
    //     //     _rb.MoveRotation(_rb.rotation * Quaternion.Euler(new Vector3(0,_camera.transform.rotation.y,0)));
    //     }
    //     
    //     // Vector3 offset = new Vector3(movement.x * transform.position.x)
    //     _rb.MovePosition(_rb.position +  movement * (Time.fixedDeltaTime * speed));
    // }
}
