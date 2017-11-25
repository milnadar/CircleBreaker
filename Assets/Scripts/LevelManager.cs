using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void StartGame()
    {
        LoadLevel("GameLevel");
    }

    public void LoadLevel(string levelName) {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(levelName);
    }

    public void QuitRequest() {
        Application.Quit();
    }

}
