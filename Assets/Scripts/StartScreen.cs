using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlayEasy() {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void PlayHard() {
        SceneManager.LoadSceneAsync("Hard");
    }

    public void QuitGame() {
        Application.Quit();
    }

    // for win screen
    public void BackToTitle() {
        SceneManager.LoadSceneAsync("StartScreen");
    }
}
