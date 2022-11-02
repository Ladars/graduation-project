using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public Health health;
    private TimeCounter timeCounter;
    private PlayerSoundEffect soundEffect;
    private void Start()
    {
        health = FindObjectOfType<Health>();
        timeCounter = FindObjectOfType<TimeCounter>();
        soundEffect = FindObjectOfType<PlayerSoundEffect>();
    }
    private void Update()
    {
        gameOver();
    }
    public void gameOver()
    {
        //if (health.health <= 0)
        //{
        //    //soundEffect.GameOverSoundEvent();
        //    PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        //    //SceneManager.LoadScene("GameOver");
        //    StartCoroutine(transition());
        //}
        if (timeCounter.limitedTime <= 0 || health.health<=0)
        {
            soundEffect.GameOverSoundEvent();
            PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene("GameOver");
            StartCoroutine(gameOvertransition());
        }
    }
    public void FinalPoint() //сно╥жу╣Ц
    {
        soundEffect.FinalPointSoundEvent();
        StartCoroutine(transition());
    }
    IEnumerator transition()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Village");
    }
    IEnumerator gameOvertransition()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }
}
