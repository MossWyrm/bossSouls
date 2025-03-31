using System;
using fsm;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private CustomPlayerInputManager _input;
    public static Player instance;
    public Animator animator { get; private set; }
    public Rigidbody rb { get; private set; }
    [FormerlySerializedAs("playerStateValues")] public PlayerStateValues inputStates = new PlayerStateValues(instance);
    public CinemachineCamera followCam;
    public Character CurrentCharacter => _shapeShiftMgr.CurrentCharacter;
    private PlayerStateMachine _stateMachine;
    public Stats Stats => _shapeShiftMgr.CurrentCharacter.stats;
    public AbilityManager abilityManager;
    
    private PlayerShapeshiftManager _shapeShiftMgr;

    public bool EnableMovement() => inputStates.canMove = true;
    public bool DisableMovement() => inputStates.canMove = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _input = CustomPlayerInputManager.Instance;
        _shapeShiftMgr = GetComponent<PlayerShapeshiftManager>();
        rb = GetComponent<Rigidbody>();

        _stateMachine = new PlayerStateMachine();
        _stateMachine.Initialize();
    }

    public void NewPlayerModel(GameObject obj)
    {
        animator = obj.GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        inputStates.fixedTime = Time.fixedDeltaTime;
        _stateMachine.FixedUpdate();
    }

    private void Update()
    {
        inputStates.Time = Time.deltaTime;
        _stateMachine.Update();
        inputStates.UpdateTimers();
    }
}
