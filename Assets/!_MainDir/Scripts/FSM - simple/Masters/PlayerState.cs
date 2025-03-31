using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fsm
{
    public class PlayerState
    {
        [Header("Components")]
        protected PlayerStateMachine psm;
        protected Player player;
        protected Rigidbody rb;

        [Header("Settings")] 
        protected float stateDuration;
        public string animName;
        protected int animIndex = -1;
        protected Transform mPlayerTransform;
        protected float frontRayOffset = 0.5f;

        [Header("State / Triggers")]
        protected bool animationFinishCalled;
        protected bool animationTriggerCalled;
        protected bool isBusy;

        private bool idle;
        private float timeUntilIdle = 5;
        private float idleTimer;
        

        public PlayerState(Player player, PlayerStateMachine psm, string animName)
        {
            this.player =  player;
            this.psm = psm;
            this.animName = animName;
            mPlayerTransform = player.transform;
        }

        public virtual void Enter()
        {
            PlayAnimation();
            rb = player.GetComponent<Rigidbody>();
            animationFinishCalled = false;
            animationTriggerCalled = false;
        }

        public virtual void Update()
        {
            IdleCheck();
            stateDuration -= Time.deltaTime;
            player.inputStates.canDodge = !player.inputStates.blockDodge && psm.dodgeCooldown <= 0;
            if(player.inputStates.canDodge && player.inputStates.isDodging) psm.ChangeState(psm.dodgeStateID);
        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Exit()
        {
            
        }

        public virtual void AnimationFinishTrigger()
        {
            animationFinishCalled = true;
        }
        
        public virtual void AnimationMidTrigger()
        {
            animationTriggerCalled = true;
        }
        
        #region for all playerStates

        protected IEnumerator BusyFor(float seconds)
        {
            isBusy = true;
            player.DisableMovement();
            yield return new WaitForSeconds(seconds);
            player.EnableMovement();
            isBusy = false;
        }
        #endregion

        protected void PlayAnimation()
        {
            if (player.animator == null) return;
            player?.animator?.Play(animName);
        }

        protected void OverrideAnimation(string animationName)
        {
            this.animName = animationName;
        }
        
        protected void HandleMove()
        {
            float frontY = 0;
            Vector3 origin = mPlayerTransform.position + (mPlayerTransform.forward * frontRayOffset);
            origin.y += 0.5f;
            Debug.DrawRay(origin, -Vector3.up, Color.red, .01f, false);
            if (Physics.Raycast(origin, -Vector3.up, out var hit, 1, psm.groundLayerMask))
            {
                float y = hit.point.y;
                frontY = y - mPlayerTransform.position.y;
            }
            
            var speed = player.inputStates.isSprinting ? player.Stats.moveSpeed * player.Stats.sprintMultiplier : player.Stats.moveSpeed;
            float moveAmount = player.inputStates.moveAmount;
            
            Vector3 currentVelocity = psm.player.rb.linearVelocity;
            Vector3 targetVelocity = mPlayerTransform.forward * (moveAmount * speed);

            player.inputStates.dCurrentMoveSpeed = moveAmount * speed;
            
            if (player.inputStates.isGrounded)
            {
                if (moveAmount > 0.1f)
                {
                    Debug.Log("Moving");
                    if (Mathf.Abs(frontY) > 0.02f)
                    {
                        targetVelocity.y = ((frontY > 0) ? frontY + 0.2f : frontY - 0.2f) * speed;
                    }
                }
                else
                {
                    float abs = Mathf.Abs(frontY);
                    if (abs > 0.02f)
                    {
                        targetVelocity.y = 0;
                    }
                }            
                HandleRotation();
            }
            else
            {
                targetVelocity.y = currentVelocity.y;
            }

            Debug.DrawRay((mPlayerTransform.position + Vector3.up * .2f), targetVelocity, Color.green, 0.01f, false);
            player.rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, player.inputStates.fixedTime * player.inputStates.adaptSpeed);
        }
        private void HandleRotation()
        {
            float h = player.inputStates.moveDirection.x;
            float v = player.inputStates.moveDirection.y;

            Vector3 targetDirection = player.followCam.transform.forward * v;
            targetDirection += player.followCam.transform.right * h;
            targetDirection.Normalize();

            targetDirection.y = 0;
            
            if(targetDirection == Vector3.zero)
                targetDirection = mPlayerTransform.forward;
            
            Quaternion tr = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation =
                Quaternion.Slerp(mPlayerTransform.rotation, tr, player.inputStates.fixedTime * player.inputStates.moveAmount * player.inputStates.rotationSpeed);
            mPlayerTransform.rotation = targetRotation;
        }
        private void IdleCheck()
        {
            var _values = player.inputStates;
            if (_values.moveAmount > 0.02f || _values.isAbilityOne || _values.isAbilityTwo || _values.isAbilityThree ||
                _values.isSprinting || _values.isDodging || _values.isDodging || _values.isJumping ||
                _values.isTransformNext || _values.isTransformPrev)
            {
                idleTimer = 0;
            }
            else
            {
                idleTimer += _values.Time;
            }

            if (idleTimer > timeUntilIdle)
            {
                psm.ChangeState(psm.idleStateID);
            }
        }
    }
}
