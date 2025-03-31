using UnityEngine;


namespace FSM2
{
    public abstract class StateAction
    {
        public float Time;
        public FiniteStateMachine stateMachine;
        public abstract bool Execute();
        public abstract void Enter();
        public abstract void Exit();
    }
}
