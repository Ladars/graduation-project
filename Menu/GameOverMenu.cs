using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public string SceneName;
    public Animator transitionAnim;
    private void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        //health = FindObjectOfType<Health>();
        //Scene scene = SceneManager.GetActiveScene();
        //Debug.Log("Active Scene is '" + scene.name + "'.");
        //SceneName = scene.name;
        Cursor.visible = true;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneName);
    }
    public void exitGame()
    {
        SceneManager.LoadScene("Village");
    }
  
    public void loadGame()
    {
        // SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        StartCoroutine(transition());
    }
    IEnumerator transition()
    {
        transitionAnim.SetTrigger("Restart");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
}
