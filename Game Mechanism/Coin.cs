using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public TimeCounter timeCounter;
    public PlayerSoundEffect soundEffect;

    private void Start()
    {
        timeCounter = FindObjectOfType<TimeCounter>();
        soundEffect = FindObjectOfType<PlayerSoundEffect>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeCounter.limitedTime += 5;
            timeCounter.getTime();
            Destroy(gameObject);
            soundEffect.CoinSoundEvent();
        }
    }
}
