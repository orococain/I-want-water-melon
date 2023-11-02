using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PlayMenu;
    void Start()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }


    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        PlayMenu.SetActive(false);
    }

    public void Home()
    {
        PlayMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void Play()
    {
        PlayMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
 void Update()
    {
        
    }
}
