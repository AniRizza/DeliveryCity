using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsGamePaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
        GameObject.Find("City Map").GetComponent<AudioSource>().Pause();
    }

    public void ResumeGame() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        GameObject.Find("City Map").GetComponent<AudioSource>().UnPause();
    }

    public void ExitGame() {
        Application.Quit();
    }
}
