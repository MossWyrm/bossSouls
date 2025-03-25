using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.parent.GetComponent<SceneTransition>().InitiateTransition(other.transform);
        }
    }
}
