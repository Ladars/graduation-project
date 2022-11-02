using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseUI; 
    public Animator transitionAnim;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
         
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.visible = false;
    }
    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.visible = true;
    }
    public void ExitChallege()
    {
        //SceneManager.LoadScene("Village");
        //transitionAnim.SetTrigger("Restart");
        StartCoroutine(transition());
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    IEnumerator transition()
    {
        transitionAnim.SetTrigger("Restart");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Village");
    }
   
}
