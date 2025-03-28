using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerControls : MonoBehaviour
{
    [FormerlySerializedAs("_player")] [SerializeField] private PlayerStats player;
    private Rigidbody _rb;
    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 _lookDirection;
    private GameOptions _gameOptions;
    private AbilityManager _abilityManager;
    private PlayerShapeshiftManager _shapeshiftManager;
    private bool _sprinting = false;
    private InputAction _sprintAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _abilityManager = GetComponent<AbilityManager>();
        _shapeshiftManager = GetComponent<PlayerShapeshiftManager>();
        
    }

    private void Start()
    {
        _sprintAction = InputSystem.actions.FindAction("Sprint");
    }

    public void OnMove(InputValue context)
    {
        _moveDirection = context.Get<Vector2>(); 
    }

    public void OnLook(InputValue context)
    {
        _lookDirection = context.Get<Vector2>();
    }

    public void OnAbilityOne(InputValue context)
    {
        _abilityManager.UseAbility(0);
    }    
    
    public void OnAbilityTwo(InputValue context)
    {
        _abilityManager.UseAbility(1);
    }    
    
    public void OnAbilityThree(InputValue context)
    {
        _abilityManager.UseAbility(2);
    }

    public void OnTransformNext()
    {
        var characterTransformRequest = (_shapeshiftManager.CurrentCharacterIndex + 1) % 3;
        _shapeshiftManager.RequestTransform(characterTransformRequest);
    }
    
    public void OnTransformPrevious()
    {
        var characterTransformRequest = (_shapeshiftManager.CurrentCharacterIndex + 2) % 3;
        _shapeshiftManager.RequestTransform(characterTransformRequest);
    }

    private void Update()
    {
        _sprinting = _sprintAction.IsPressed();
    }

    private void FixedUpdate()
    {
        MovePlayer(_moveDirection);
        RotatePlayer(_lookDirection);
    }

    private void RotatePlayer(Vector2 lookDirection)
    {
        _gameOptions ??= GameManager.Instance.gameOptions;
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0, lookDirection.x * _gameOptions.xSensitivity, 0));
    }

    private void MovePlayer(Vector2 directions)
    {
        if(player.Stats == null) return;
        var speed = player.Stats.speed;
        speed *= _sprinting ? player.Stats.sprintMultiplier : 1;
        var movement = new Vector3(0.0f, 0.0f, 0.0f) + (directions.y * transform.forward) + (directions.x * transform.right);
        // Vector3 offset = new Vector3(movement.x * transform.position.x)
        _rb.MovePosition(_rb.position +  movement * (Time.fixedDeltaTime * speed));
    }
}
