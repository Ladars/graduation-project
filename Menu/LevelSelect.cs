using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public GameObject QuestUI;
    public PlayerSoundEffect soundEffect;
    private void Start()
    {
        soundEffect = FindObjectOfType<PlayerSoundEffect>();
    }
    public void loadLevel1()
    {
        soundEffect.ClickSoundEvent();
        QuestUI.SetActive(false);
        SceneManager.LoadScene("Level2");
    }
    public void loadLevel2()
    {
        soundEffect.ClickSoundEvent();
        QuestUI.SetActive(false);
        SceneManager.LoadScene("Level3");
    }
    public void closed()
    {
        soundEffect.ClickSoundEvent();
        QuestUI.SetActive(false);
    }
}
