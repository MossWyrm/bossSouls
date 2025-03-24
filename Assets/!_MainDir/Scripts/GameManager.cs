using UnityEngine;

namespace __MainDir.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public GameObject player;
    }
}
