using UnityEngine;

namespace fsm
{
    public class PlayerTransformState : PlayerState
    {
        public PlayerTransformState(Player player, PlayerStateMachine psm, string animName) : base(player, psm, animName)
        {
        }

        public override void Enter()
        {
            psm.ChangeState(psm.movingStateID);
            //TODO Disable necessary things
            base.Enter();
        }

        public override void Exit()
        {
            //TODO Re-enable other aspects
            base.Exit();
        }

        public override void FixedUpdate()
        {
            //TODO Implement transform logic - likely animation and full stop to all actions until animation is complete
            base.FixedUpdate();
        }
    }
    
}
