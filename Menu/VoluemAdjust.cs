using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoluemAdjust : MonoBehaviour
{
    public AudioSource audioSource;
    private float musicVolume = 0.5f;
    [SerializeField] private Slider volumeSlider;
    public void volumeChange()
    {
        musicVolume = volumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }
}
