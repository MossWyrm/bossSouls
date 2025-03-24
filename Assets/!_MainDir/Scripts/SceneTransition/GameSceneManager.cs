using __MainDir.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    private string _currentScene;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
    }
    
    public void ChangeScene(string sceneName, Vector3 newSceneTargetPosition)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.UnloadSceneAsync(_currentScene);
        _currentScene = sceneName;
        GameManager.Instance.player.transform.position = newSceneTargetPosition;
    }
}
