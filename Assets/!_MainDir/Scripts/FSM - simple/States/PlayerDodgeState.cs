using UnityEngine;

namespace fsm
{
    public class PlayerDodgeState : PlayerState
    {
        public PlayerDodgeState(Player player, PlayerStateMachine psm, string animName) : base(player, psm, animName)
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
            //TODO Implement dodge logic - dodge is unbreakable once started
            base.FixedUpdate();
        }
    }
}