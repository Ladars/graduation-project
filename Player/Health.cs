using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    //public int numOfHeart;

    public Image[] hearts;
    //public Image i
    public Sprite fullHeart;
    //public Sprite emptyHeart;

    public float invincibleTime = 2f;
    private float invincibleCounter;
    private PlayerMovement player;
    private  TimeCounter timeCounter;
    //hurt Effect
    public Animator hurtEffectAnimator;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        timeCounter = FindObjectOfType<TimeCounter>();
        hurtEffectAnimator = GameObject.Find("HurtEffect").GetComponent<Animator>();
    }
    private void Update()
    {
        if (invincibleCounter > 0)
        {
            player.takeDamage = false;
            invincibleCounter -= Time.deltaTime;
        }
        heartDisplay();
        //gameOver();
    }
    private void heartDisplay()  //显示已有生命值
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            else
            {
                hearts[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);//0透明度表示玩家失去一格血
            }
        }
    }
    public void takeDamage(int damage)
    {
        if(invincibleCounter <= 0)
        {
            timeCounter.limitedTime -= 3;
            timeCounter.loseTime();
            health -= damage;
            invincibleCounter = invincibleTime;
            player.takeDamage = true;
            hurtEffectAnimator.SetTrigger("HurtEffect");
        }
    }
    public void takeDamageFromEnemy(int damage)
    {
        if(invincibleCounter <= 0)
        {
            health -= damage;
            invincibleCounter = invincibleTime;
            player.takeDamage = true;
            hurtEffectAnimator.SetTrigger("HurtEffect");
        }
    }

   
    //public void gameOver()
    //{
    //    if (health <= 0)
    //    {
    //        PlayerPrefs.SetInt("SavedScene",SceneManager.GetActiveScene().buildIndex);
    //        SceneManager.LoadScene("GameOver");
    //    }
    //    if (timeCounter.limitedTime <= 0)
    //    {
    //        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
    //        SceneManager.LoadScene("GameOver");
    //    }
    //}
}
