using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public float limitedTime;
    public TMP_Text timeLeft;
    private int seconds;
    public Animator textAnimation;
    public TMP_Text animationText;

    private void Update()
    {
        limitedTime -= Time.deltaTime;
        seconds = (int)(limitedTime );
        timeLeft.text = seconds.ToString();
    }
    public void getTime()
    {
        animationText.text = "+5";
        textAnimation.SetTrigger("getTime");
    }
    public void loseTime()
    {
        animationText.text = "-3";
        textAnimation.SetTrigger("getTime");
    }
    public void slimeKillTime()
    {
        animationText.text = "+7";
        limitedTime += 7;
        textAnimation.SetTrigger("getTime");
    }
    public void dogKnightKillTime()
    {
        animationText.text = "+7";
        limitedTime += 7;
        textAnimation.SetTrigger("getTime");
    }
    public void turretKillTime()
    {
        animationText.text = "+10";
        limitedTime += 10;
        textAnimation.SetTrigger("getTime");
    }
    public void eyeBatKillTime()
    {
        animationText.text = "+30";
        limitedTime += 30;
        textAnimation.SetTrigger("getTime");
    }
}
