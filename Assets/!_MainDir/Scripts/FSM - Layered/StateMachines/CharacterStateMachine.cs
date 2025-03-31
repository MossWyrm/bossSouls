using UnityEngine;

namespace FSM2
{
    public abstract class CharacterStateMachine : FiniteStateMachine
    {
        [Header("References")]
        public Animator anim;
        public Rigidbody rb;

        [Header("Controller Values")] 
        public float vertical;
        public float horizontal;

        [Header("Bool Values")] 
        public bool animationTriggerCalled;

        public override void Init()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }
        
        public virtual void AnimationTrigger()
        {
            animationTriggerCalled = true;
        }
        
        public void PlayAnimation(string targetAnim)
        {
            anim?.CrossFade(targetAnim, 0.2f);
        }
    }
}
