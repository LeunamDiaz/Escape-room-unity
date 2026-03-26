using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string replaySceneName = "RoomOne";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(replaySceneName);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        Debug.Log("Quit Game pressed (Editor won't close).");
#else
        Application.Quit();
#endif
    }
}
