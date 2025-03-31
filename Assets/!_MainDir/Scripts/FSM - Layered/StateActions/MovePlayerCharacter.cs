using UnityEngine;

namespace FSM2
{
    public class MovePlayerCharacter : StateAction
    {
        private NewPlayerStateMachine sm;
        
        
        public MovePlayerCharacter(NewPlayerStateMachine stateMachine)
        {
            sm = stateMachine;
        }
        public override bool Execute()
        {
            HandleMove();
            return false;
        }

        private void HandleMove()
        {
            float frontY = 0;
            Vector3 origin = sm.mTransform.position + (sm.mTransform.forward * sm.frontRayOffset);
            origin.y += 0.5f;
            Debug.DrawRay(origin, -Vector3.up, Color.red, .01f, false);
            if (Physics.Raycast(origin, -Vector3.up, out var hit, 1, sm.groundLayerMask))
            {
                float y = hit.point.y;
                frontY = y - sm.mTransform.position.y;
            }
            Vector3 currentVelocity = sm.rb.linearVelocity;
            Vector3 targetVelocity = sm.mTransform.forward * (sm.moveAmount * sm.movementSpeed);

            if (sm.isGrounded)
            {
                float moveAmount = sm.moveAmount;
                if (moveAmount > 0.1f)
                {
                    if (Mathf.Abs(frontY) > 0.02f)
                    {
                        var speed = sm.isSprinting ? sm.movementSpeed * sm.sprintSpeed : sm.movementSpeed;
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

            Debug.DrawRay((sm.mTransform.position + Vector3.up * .2f), targetVelocity, Color.green, 0.01f, false);
            sm.rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time * sm.adaptSpeed);
        }
        private void HandleRotation()
        {
            float h = sm.horizontal;
            float v = sm.vertical;

            Vector3 targetDirection = sm.followCamera.transform.forward * v;
            targetDirection += sm.followCamera.transform.right * h;
            targetDirection.Normalize();

            targetDirection.y = 0;
            
            if(targetDirection == Vector3.zero)
                targetDirection = sm.mTransform.forward;
            
            Quaternion tr = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation =
                Quaternion.Slerp(sm.mTransform.rotation, tr, Time * sm.moveAmount * sm.rotationSpeed);
            sm.mTransform.rotation = targetRotation;

        }
        public override void Enter()
        {
            sm.movementSpeed = sm.player.Stats.moveSpeed;
            sm.sprintSpeed = sm.player.Stats.sprintMultiplier;
        }

        public override void Exit()
        {
            
        }
        
        
    }
}
