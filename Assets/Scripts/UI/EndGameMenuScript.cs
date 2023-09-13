using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenuScript : MonoBehaviour
{
    public GameObject endGameMenuUI;

    public void EndGame() {
        endGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameObject.Find("City Map").GetComponent<AudioSource>().volume = 0.1f;
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlaySound("EndGame");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
