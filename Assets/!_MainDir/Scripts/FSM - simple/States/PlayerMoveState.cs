using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace fsm
{
    public class PlayerMoveState : PlayerState
    {

        public PlayerMoveState(Player player, PlayerStateMachine psm, string animName) : base(player, psm, animName)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            HandleMove();
        }

        public override void Update()
        {
            AttackCheck();
        }

        private void AttackCheck()
        {
            if (!player.inputStates.canAttack) return;
            if(player.inputStates.isAbilityOne) psm.ChangeState(psm.abilityOneID);
            else if(player.inputStates.isAbilityTwo) psm.ChangeState(psm.abilityTwoID);
            else if(player.inputStates.isAbilityThree) psm.ChangeState(psm.abilityThreeID);
        }
    }
}

