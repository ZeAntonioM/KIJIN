using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject timer;

    private void Awake() {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {

            ResetLevel();
        }

    }

  
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pauseMenuUI.SetActive(false);
        player.GetComponent<PlayerRespawn>().Respawn();

        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
        if (timer != null) timer.GetComponent<Timer>().ResetTimer();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
