using System;
using UnityEditor;
using UnityEngine;

namespace __MainDir.Scripts.SceneTransition
{
    public enum TransitionType
    {
        Warp,
        Scene
    }
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private TransitionType transitionType;
        [SerializeField] private string sceneName;
        [SerializeField] private Vector3 newSceneTargetPosition;

        [SerializeField] private Transform _destination;

        public void InitiateTransition(Transform objectToTransition)
        {
            switch (transitionType)
            {
                case TransitionType.Warp:
                    objectToTransition.position = _destination.position;
                    break;
                case TransitionType.Scene:
                    GameSceneManager.Instance.ChangeScene(sceneName, newSceneTargetPosition);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDrawGizmos()
        {
            if (transitionType == TransitionType.Scene)
            {
                Handles.Label(transform.position, "to " + sceneName);
            }
            else if (transitionType == TransitionType.Warp)
            {
                Gizmos.DrawLine(transform.position, _destination.position);
            }
        }
    }
}