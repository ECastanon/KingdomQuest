using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI, WinUI;
    private PeopleManager pm;
    public static bool paused = false;
    public bool hasWon = false;
    
    void Start()
    {
        pm = GameObject.Find("PeopleManager").GetComponent<PeopleManager>();
        WinUI = GameObject.Find("Win");
        PauseUI.SetActive(false);
        WinUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
        if (paused)
        {
            HideSprites();
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        } else if (!paused)
        {
            ShowSprites();
            PauseUI.SetActive(false);
            WinUI.SetActive(false);
            Time.timeScale = 1;
        }

        if(pm.population >= 50 && hasWon == false)
        {
            Resume();
            hasWon = true;
            WinUI.SetActive(true);
            Time.timeScale = 0;
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
    private void HideSprites()
    {
        Color tmp = new Color(1,1,1,0);
        GameObject.Find("pngtree-cartoon-food-bread-whole-wheat-bread-png-image_423640").GetComponent<SpriteRenderer>().color = tmp;
        GameObject.Find("istockphoto-1182472970-612x612").GetComponent<SpriteRenderer>().color = tmp;
        GameObject.Find("smile").GetComponent<SpriteRenderer>().color = tmp;
    }
    private void ShowSprites()
    {
        Color tmp = new Color(1,1,1,1);
        GameObject.Find("pngtree-cartoon-food-bread-whole-wheat-bread-png-image_423640").GetComponent<SpriteRenderer>().color = tmp;
        GameObject.Find("istockphoto-1182472970-612x612").GetComponent<SpriteRenderer>().color = tmp;
        GameObject.Find("smile").GetComponent<SpriteRenderer>().color = tmp;
    }
}