using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM2
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        public State currentState;
        Dictionary<string, State> allStates = new Dictionary<string, State>();
        [HideInInspector] public Transform mTransform;

        private void Start()
        {
            mTransform = transform;
            Init();
        }

        public abstract void Init();

        public void FixedTick()
        {
            currentState?.FixedTick();
        }

        public void Tick()
        {
            currentState?.Tick();
        }

        public void LateTick()
        {
            currentState?.LateTick();
        }

        public void ChangeState(string targetId)
        {
            State targetState = GetState(targetId);
            if (currentState == targetState) return;
            
            Debug.Log("Changing state to "+targetId);
            
            currentState?.Exit();
            targetState.Enter();
            currentState = targetState;
        }

        private State GetState(string targetId)
        {
            allStates.TryGetValue(targetId, out State retVal);
            return retVal;
        }
        
        protected void RegisterState(string stateID, State state)
        {
            allStates.Add(stateID, state);
        }
    }
}
