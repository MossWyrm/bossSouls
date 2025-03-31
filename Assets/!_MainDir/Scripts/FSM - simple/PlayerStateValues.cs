using UnityEngine;

namespace fsm
{
    [System.Serializable]
    public class PlayerStateValues
    {
        private Player _player;
        [HideInInspector] public float gracePeriod = 0.2f;
        [HideInInspector] public float Time;
        [HideInInspector] public float fixedTime;
        public float adaptSpeed;
        public float rotationSpeed;
        public bool canMove = true;
        public bool canJump = true;
        public bool canDodge = true;
        public bool blockDodge = false;
        public bool canAttack = true;
        public bool canTransform = true;
        
        [Header("Handled Internally")]
        [ReadOnly] public Vector2 moveDirection;
        [ReadOnly] public float moveAmount => Mathf.Clamp01(Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y));
        [ReadOnly] public Vector2 lookDirection;
        [ReadOnly] public bool isSprinting;
        [ReadOnly] public bool isJumping;
        [ReadOnly] public bool isDodging;
        [ReadOnly] public bool isAbilityOne;
        [ReadOnly] public bool isAbilityTwo;
        [ReadOnly] public bool isAbilityThree;
        [ReadOnly] public bool isTransformNext;
        [ReadOnly] public bool isTransformPrev;
        [ReadOnly] public bool isGrounded = true;
        
        [Header("For Debug Purposes")]
        [ReadOnly] public float dCurrentMoveSpeed;
        [ReadOnly] public float dMoveAmount;
        
        [ReadOnly]public float dodgeTimer = 0, jumpTimer = 0;
        
        public PlayerStateValues(Player player)
        {
            _player = player;
            Enter();
        }
        
        public void Enter()
        {
            CustomPlayerInputManager.MovePerformed += OnMove;
            CustomPlayerInputManager.MoveCanceled += OnMoveEnd;
            CustomPlayerInputManager.LookPerformed += OnLook;
            CustomPlayerInputManager.LookCanceled += OnLookEnd;
            CustomPlayerInputManager.AbilityOnePerformed += () => isAbilityOne = true;
            CustomPlayerInputManager.AbilityOneCanceled += () => isAbilityOne = false;
            CustomPlayerInputManager.AbilityTwoPerformed += () => isAbilityTwo = true;
            CustomPlayerInputManager.AbilityTwoCanceled += () => isAbilityTwo = false;
            CustomPlayerInputManager.AbilityThreePerformed += () => isAbilityThree = true;
            CustomPlayerInputManager.AbilityThreeCanceled += () =>  isAbilityThree = false;
            CustomPlayerInputManager.SprintPerformed += () => isSprinting = true;
            CustomPlayerInputManager.SprintCanceled += () => isSprinting = false;
            CustomPlayerInputManager.DodgePerformed += () => isDodging = true;
            CustomPlayerInputManager.DodgeCanceled += () => dodgeTimer = 0;
            CustomPlayerInputManager.JumpPerformed += () => isJumping = true;
            CustomPlayerInputManager.JumpCanceled += () => jumpTimer = 0;
            CustomPlayerInputManager.TransformNextPerformed += () => isTransformNext = true;
            CustomPlayerInputManager.TransformNextCanceled += () => isTransformNext = false;
            CustomPlayerInputManager.TransformPreviousPerformed += () => isTransformPrev = true;
            CustomPlayerInputManager.TransformPreviousCanceled += () => isTransformPrev = false;
        }

        public void Exit()
        {            
            CustomPlayerInputManager.MovePerformed -= OnMove;
            CustomPlayerInputManager.MoveCanceled -= OnMoveEnd;
            CustomPlayerInputManager.LookPerformed -= OnLook;
            CustomPlayerInputManager.LookCanceled -= OnLookEnd;
            CustomPlayerInputManager.AbilityOnePerformed -= () => isAbilityOne = true;
            CustomPlayerInputManager.AbilityOneCanceled -= () => isAbilityOne = false;
            CustomPlayerInputManager.AbilityTwoPerformed -= () => isAbilityTwo = true;
            CustomPlayerInputManager.AbilityTwoCanceled -= () => isAbilityTwo = false;
            CustomPlayerInputManager.AbilityThreePerformed -= () => isAbilityThree = true;
            CustomPlayerInputManager.AbilityThreeCanceled -= () =>  isAbilityThree = false;
            CustomPlayerInputManager.SprintPerformed -= () => isSprinting = true;
            CustomPlayerInputManager.SprintCanceled -= () => isSprinting = false;
            CustomPlayerInputManager.DodgePerformed -= () => isDodging = true;
            CustomPlayerInputManager.DodgeCanceled -= () => dodgeTimer = 0;
            CustomPlayerInputManager.JumpPerformed -= () => isJumping = true;
            CustomPlayerInputManager.JumpCanceled -= () => jumpTimer = 0;
            CustomPlayerInputManager.TransformNextPerformed -= () => isTransformNext = true;
            CustomPlayerInputManager.TransformNextCanceled -= () => isTransformNext = false;
            CustomPlayerInputManager.TransformPreviousPerformed -= () => isTransformPrev = true;
            CustomPlayerInputManager.TransformPreviousCanceled -= () => isTransformPrev = false;
        }
        
        private void OnMove()
        {
            moveDirection = CustomPlayerInputManager.Instance.MoveVector;
            dMoveAmount = moveAmount;
        }

        private void OnMoveEnd()
        {
            moveDirection = Vector2.zero;
            dMoveAmount = moveAmount;
        }

        private void OnLook()
        {
            lookDirection = CustomPlayerInputManager.Instance.LookVector;
        }

        private void OnLookEnd()
        {
            lookDirection = Vector2.zero;
        }
        public void UpdateTimers()
        {
            UpdateTimer(ref isDodging, ref dodgeTimer);
            UpdateTimer(ref isJumping, ref jumpTimer);
        }

        private void UpdateTimer(ref bool b, ref float t)
        {
            if (b == false) return;
            t += Time;
            if (t > gracePeriod)
            {
                b = false;
                t = 0;
            }
        }
    }
    
}
