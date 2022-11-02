using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{
    public AudioClip JumpSoundclip;
    public AudioClip WalkSoundclip;
    public AudioClip RunSoundclip;
    public AudioClip HurtSoundclip;
    public AudioSource source;
    public AudioClip CoinSound;
    public AudioClip LoseSound;
    public AudioClip VictorySound;
    public AudioClip gunSound;
    public AudioClip clickSound;
    public void jumpSoundEvent()
    {
        source.PlayOneShot(JumpSoundclip);
    }
    public void walkSoundEvent()
    {
        source.PlayOneShot(WalkSoundclip);
    }
    public void runSoundEvent()
    {
        source.PlayOneShot(RunSoundclip);
    }
    public void hurtSoundEvent()
    {
        source.PlayOneShot(HurtSoundclip);
    }
    public void CoinSoundEvent()
    {
        source.PlayOneShot(CoinSound);
    }
    public void FinalPointSoundEvent()
    {
        source.PlayOneShot(VictorySound);
    }
    public void GameOverSoundEvent()
    {
        source.PlayOneShot(LoseSound);
    }

    public void ClickSoundEvent()
    {
        source.PlayOneShot(clickSound);
    }
    public void shootSoundEvent()
    {
        source.PlayOneShot(gunSound);
    }
}
