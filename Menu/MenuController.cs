using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Option")]
    public string SceneName;
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider=null;

    public GameObject BGM;
    public void startGame()
    {
        SceneManager.LoadScene(SceneName);
    }
   public void exitGame()
    {
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
      
        volumeTextValue.text = volume.ToString();
    }
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
    }
}
