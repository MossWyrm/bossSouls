using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace FSM2
{
    public class NewPlayerStateMachine : CharacterStateMachine
    {
        [Header("References")] 
        public Player player;
        public LayerMask groundLayerMask;
        [HideInInspector] public CinemachineCamera followCamera;
        
        [Header("Inputs")]
        public float lookHorizontal;
        public float lookVertical;
        public float moveAmount;
        public Vector3 rotateDirection;

        [Header("States")] 
        public bool isGrounded;
        public bool isSprinting;
        public bool isAbilityOne;
        public bool isAbilityTwo;
        public bool isAbilityThree;
        public bool isDodging;
        
        [Header("Movement Stats")]
        public float frontRayOffset = 0.5f;
        public float adaptSpeed = 10;
        public float rotationSpeed = 10;
        [ReadOnly] public float movementSpeed = 2;
        [ReadOnly] public float sprintSpeed = 10;

        
        
        [HideInInspector] public string movingID = "moving";
        [HideInInspector] public string idleID = "idle";
        [HideInInspector] public string dodgingID = "dodging";
        [HideInInspector] public string abilityID = "ability";
        public override void Init()
        {
            base.Init();
            followCamera = Player.instance.followCam;
            player = Player.instance;
            State moving = new State(
                new List<StateAction>()
                {
                    new InputManager(this),
                    new MovePlayerCharacter(this)
                },
                new List<StateAction>()
                {
                    
                },
                new List<StateAction>()
                );
            
            State idle = new State(
                new List<StateAction>()
                {
                    new InputManager(this),
                },
                new List<StateAction>(),
                new List<StateAction>()
            );
            
            State dodging = new State(
                new List<StateAction>(),
                new List<StateAction>(),
                new List<StateAction>()
            );
            
            State ability = new State(
                new List<StateAction>(),
                new List<StateAction>(),
                new List<StateAction>()
            );
            
            RegisterState(movingID, moving);
            RegisterState(idleID, idle);
            RegisterState(dodgingID, dodging);
            RegisterState(abilityID, ability);
            
            ChangeState(movingID);
        }

        public void ChangeAnimator(Animator animator)
        {
            anim = animator;
        }

        private void FixedUpdate()
        {
            base.FixedTick();
        }

        private void Update()
        {
            base.Tick();
        }

        private void LateUpdate()
        {
            base.LateTick();
        }
    }
}
