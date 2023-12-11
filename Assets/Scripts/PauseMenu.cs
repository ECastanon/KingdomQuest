using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public static bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        } else if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    //Closes the pause menu
    public void Resume()
    {
        paused = !paused;
    }
    public void Title()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}