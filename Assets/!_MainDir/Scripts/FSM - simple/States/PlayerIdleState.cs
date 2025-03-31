using System;
using System.Collections.Generic;
using UnityEngine;

namespace fsm
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, PlayerStateMachine psm, string animName) : base(player, psm, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (!player.inputStates.canMove) return;
            if (player.inputStates.moveAmount > 0.02f) psm.ChangeState(psm.movingStateID);
        }
    }
}
