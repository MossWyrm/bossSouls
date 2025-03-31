using UnityEngine;
using UnityEngine.InputSystem;

namespace FSM2
{
    public class InputManager : StateAction
    {
        private NewPlayerStateMachine sm;
        private float gracePeriod = 0.2f;
        private bool abilityOne, abilityTwo, abilityThree, dodge, jump, transformNext, transformPrev;
        private float abilityOneTimer = 0, abilityTwoTimer = 0, abilityThreeTimer = 0, dodgeTimer = 0, jumpTimer = 0, transformNextTimer = 0, transformPrevTimer = 0;
        
        public InputManager(NewPlayerStateMachine sm)
        {
            this.sm = sm;
        }
        public override bool Execute()
        {
            sm.moveAmount = Mathf.Clamp01(Mathf.Abs(sm.horizontal) + Mathf.Abs(sm.vertical));

            var retVal = AttackCheck();
            var transforming = false;
            UpdateTimers();
            
            if (transformNext || transformPrev)
            {
                transforming = true;
            }

            return false;
        }

        private bool AttackCheck()
        {
            var isAttacking = abilityOne || abilityTwo || abilityThree;
            
            if (dodge)
            {
                isAttacking = false;
                return false;
            }
            
            if (isAttacking)
            {
                Debug.Log("Attempted change to ability use state, not implemented so returning");
                isAttacking = false;
                // sm.ChangeState(sm.abilityID);
            }
            
            return isAttacking;
            
        }

        public override void Enter()
        {
            CustomPlayerInputManager.MovePerformed += OnMove;
            CustomPlayerInputManager.MoveCanceled += OnMoveEnd;
            CustomPlayerInputManager.LookPerformed += OnLook;
            CustomPlayerInputManager.LookCanceled += OnLookEnd;
            CustomPlayerInputManager.AbilityOnePerformed += () => abilityOne = true;
            CustomPlayerInputManager.AbilityOneCanceled += () => abilityOneTimer = 0;
            CustomPlayerInputManager.AbilityTwoPerformed += () => abilityTwo = true;
            CustomPlayerInputManager.AbilityTwoCanceled += () => abilityTwoTimer = 0;
            CustomPlayerInputManager.AbilityThreePerformed += () => abilityThree = true;
            CustomPlayerInputManager.AbilityThreeCanceled += () =>  abilityThreeTimer = 0;
            CustomPlayerInputManager.SprintPerformed += () => sm.isSprinting = true;
            CustomPlayerInputManager.SprintCanceled += () => sm.isSprinting = false;
            CustomPlayerInputManager.DodgePerformed += () => dodge = true;
            CustomPlayerInputManager.DodgeCanceled += () => dodgeTimer = 0;
            CustomPlayerInputManager.JumpPerformed += () => jump = true;
            CustomPlayerInputManager.JumpCanceled += () => jumpTimer = 0;
            CustomPlayerInputManager.TransformNextPerformed += () => transformNext = true;
            CustomPlayerInputManager.TransformNextCanceled += () => transformPrevTimer = 0;
            CustomPlayerInputManager.TransformPreviousPerformed += () => transformPrev = true;
            CustomPlayerInputManager.TransformPreviousCanceled += () => transformPrevTimer = 0;
        }

        public override void Exit()
        {            
            CustomPlayerInputManager.MovePerformed -= OnMove;
            CustomPlayerInputManager.MoveCanceled -= OnMoveEnd;
            CustomPlayerInputManager.LookPerformed -= OnLook;
            CustomPlayerInputManager.LookCanceled -= OnLookEnd;
            CustomPlayerInputManager.AbilityOnePerformed -= () => abilityOne = true;
            CustomPlayerInputManager.AbilityOneCanceled -= () => abilityOneTimer = 0;
            CustomPlayerInputManager.AbilityTwoPerformed -= () => abilityTwo = true;
            CustomPlayerInputManager.AbilityTwoCanceled -= () => abilityTwoTimer = 0;
            CustomPlayerInputManager.AbilityThreePerformed -= () => abilityThree = true;
            CustomPlayerInputManager.AbilityThreeCanceled -= () =>  abilityThreeTimer = 0;
            CustomPlayerInputManager.SprintPerformed -= () => sm.isSprinting = true;
            CustomPlayerInputManager.SprintCanceled -= () => sm.isSprinting = false;
            CustomPlayerInputManager.DodgePerformed -= () => dodge = true;
            CustomPlayerInputManager.DodgeCanceled -= () => dodgeTimer = 0;
            CustomPlayerInputManager.JumpPerformed -= () => jump = true;
            CustomPlayerInputManager.JumpCanceled -= () => jumpTimer = 0;
            CustomPlayerInputManager.TransformNextPerformed -= () => transformNext = true;
            CustomPlayerInputManager.TransformNextCanceled -= () => transformPrevTimer = 0;
            CustomPlayerInputManager.TransformPreviousPerformed -= () => transformPrev = true;
            CustomPlayerInputManager.TransformPreviousCanceled -= () => transformPrevTimer = 0;
        }

        private void OnMove()
        {
            sm.ChangeState(sm.movingID);
            var val = CustomPlayerInputManager.Instance.MoveVector;
            sm.horizontal = val.x;
            sm.vertical = val.y;
        }

        private void OnMoveEnd()
        {
            sm.horizontal = 0;
            sm.vertical = 0;
            sm.ChangeState(sm.idleID);
        }

        private void OnLook()
        {
            var val = CustomPlayerInputManager.Instance.LookVector;
            sm.lookHorizontal = val.x;
            sm.lookVertical = val.y;
        }

        private void OnLookEnd()
        {
            sm.lookHorizontal = 0;
            sm.lookVertical = 0;
        }

        private void UpdateTimers()
        {
            UpdateTimer(abilityOne, abilityOneTimer);
            UpdateTimer(abilityTwo, abilityTwoTimer);
            UpdateTimer(abilityThree, abilityThreeTimer);
            UpdateTimer(dodge, dodgeTimer);
            UpdateTimer(jump, jumpTimer);
            UpdateTimer(transformNext, transformNextTimer);
            UpdateTimer(transformPrev, transformPrevTimer);
        }

        private void UpdateTimer(bool b, float t)
        {
            if (!b) return;
            t += Time;
            if (t > gracePeriod)
            {
                b = false;
            }
        }

    }
}
