using System;
using System.Collections.Generic;
using UnityEngine;

namespace fsm
{
    public class PlayerStateMachine
    {
        public PlayerState currentState { get; private set; }
        public Animation currentStateAnimation { get; private set; }
        public Player player;
        public LayerMask groundLayerMask;

        public float dodgeCooldown;
        
        #region [--- States ---]
        [HideInInspector] public string idleStateID = "idle"; 
        [HideInInspector] public string dodgeStateID = "dodge"; 
        [HideInInspector] public string movingStateID = "moving"; 
        [HideInInspector] public string abilityOneID = "abilityOne";
        [HideInInspector] public string abilityTwoID = "abilityTwo";
        [HideInInspector] public string abilityThreeID = "abilityThree";
        [HideInInspector] public string jumpStateID = "jump";
        [HideInInspector] public string transformStateID = "transform";
        
        #endregion
        
        private Dictionary<string, PlayerState> _allStates = new Dictionary<string, PlayerState>();

        public void Initialize()
        {
            player = Player.instance;
            CreateStateMachine();
            currentState = GetState(idleStateID);
            currentState.Enter();
        }

        private void CreateStateMachine()
        {
            RegisterState(movingStateID, new PlayerMoveState(player, this, ""));
            RegisterState(idleStateID, new PlayerIdleState(player, this, ""));
            RegisterState(dodgeStateID, new PlayerDodgeState(player, this, ""));
            RegisterState(transformStateID, new PlayerTransformState(player, this, ""));
            RegisterState(jumpStateID, new PlayerJumpState(player, this, ""));
            RegisterState(abilityOneID, new PlayerAbilityState(player, this, "", 0));
            RegisterState(abilityTwoID, new PlayerAbilityState(player, this, "", 1));
            RegisterState(abilityThreeID, new PlayerAbilityState(player, this, "", 2));
        }
        public void ChangeState(string targetID)
        {
            currentState.Exit();
            var targetState = GetState(targetID);
            targetState.Enter();
            currentState = targetState;
        }

        public void Update()
        {
            if (dodgeCooldown > 0) dodgeCooldown -= Time.deltaTime;
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        private PlayerState GetState(string targetID)
        {
            _allStates.TryGetValue(targetID, out PlayerState retVal);
            return retVal;
        }
        
        protected void RegisterState(String stateID, PlayerState playerState)
        {
            _allStates.Add(stateID, playerState);
        }
    }
}