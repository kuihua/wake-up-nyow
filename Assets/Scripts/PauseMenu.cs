using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused) {
                Resume(); 
            } else {
                Pause();
            }
        }
        
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadOptions() {
        // Debug.Log("Loading options...");
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void LoadPauseMenu() {
        // Debug.Log("Loading pause menu...");
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void BackToTitle() {
        Time.timeScale = 1f;
        gameIsPaused = false;
        // Debug.Log("Going back to title...");
        SceneManager.LoadSceneAsync("StartScreen");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
