using UnityEngine;

namespace fsm
{
    public class PlayerAbilityState : PlayerState
    {
        private int abilityNum;
        private AbilityManager _abilityManager;
        private PlayerStateValues _values;
        private Ability _ability;
        
        public PlayerAbilityState(Player player, PlayerStateMachine psm, string animName, int abilityNum) : base(player, psm, animName)
        {
            this.abilityNum = abilityNum;
            _abilityManager = player.abilityManager;
        }

        public override void Enter()
        {
            base.Enter();
            _values = player.inputStates;
            _ability = _abilityManager.GetAbility(abilityNum);
            
            if (_ability == null || !_ability.canUse)
            {
                psm.ChangeState(psm.movingStateID);
            }
            

            _values.canMove = !_ability.blocksMovement;
            _values.canJump = !_ability.blocksMovement;
            _values.canAttack = false;
            _values.blockDodge = !_ability.canBreak;
            
            _abilityManager.UseAbility(abilityNum);
            PlayAnimation();
        }

        public override void Exit()
        {
            CancelAbility();
            _values.canMove = true;
            _values.canJump = true;
            _values.canAttack = true;
            _values.blockDodge = false;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!_ability.blocksMovement)
            {
                HandleMove();
            }
        }

        public override void Update()
        {
            base.Update();
            if (animationTriggerCalled)
            {
                animationTriggerCalled = false;
                _ability.AnimationTrigger();
            }
            
            if (animationFinishCalled)
            {
                psm.ChangeState(psm.movingStateID);
            }
            
            //DEBUG
            Debug.Log("DEBUG: Ability animations not implemented, returning to moving state");
            psm.ChangeState(psm.movingStateID);
        }

        private void CancelAbility()
        {
            _ability.CancelUse();
        }
        
    }
}
