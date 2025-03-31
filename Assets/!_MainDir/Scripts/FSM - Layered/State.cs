using System.Collections.Generic;
using UnityEngine;

namespace FSM2
{
    public class State
    {
        public bool forceExit;
        private List<StateAction> fixedActions;
        private List<StateAction> updateActions;
        private List<StateAction> lateActions;

        public State(List<StateAction> fixedActionsList, List<StateAction> updateActionsList, List<StateAction> lateActionsList)
        {
            fixedActions = fixedActionsList;
            updateActions = updateActionsList;
            lateActions = lateActionsList;
        }

        public void Enter()
        {
            EnterActions(fixedActions);
            EnterActions(updateActions);
            EnterActions(lateActions);
        }

        public void Exit()
        {
            ExitActions(fixedActions);
            ExitActions(updateActions);
            ExitActions(lateActions);
        }
        
        public void FixedTick()
        {
            ExecuteListOfActions(fixedActions, Time.fixedDeltaTime);
        }

        public void Tick()
        {
            ExecuteListOfActions(updateActions, Time.deltaTime);
        }

        public void LateTick()
        {
            ExecuteListOfActions(lateActions, Time.deltaTime);
            forceExit = false;
        }

        void ExecuteListOfActions(List<StateAction> actions, float time)
        {
            foreach (StateAction action in actions)
            {
                if (forceExit) return;
                action.Time = time;
                forceExit = action.Execute();
            }
        }

        void EnterActions(List<StateAction> actions)
        {
            foreach (StateAction action in actions)
            {
                action?.Enter();
            }
        }

        void ExitActions(List<StateAction> actions)
        {
            foreach (var action in actions)
            {
                action?.Exit();
            }
        }
    }
}
