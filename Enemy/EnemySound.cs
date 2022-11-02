using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip fireSoundClip;
    public AudioClip slimeDeathSoundClip;
    public AudioClip dogKnightDeathSoundClip;
    public AudioClip dogKnightAttackSoundClip;
    public AudioClip turretAttackSoundClip;
    public AudioClip turretDeathSoundClip;
    public AudioClip eyeBatAttackSoundClip;
    public AudioClip eyeBatDeathSoundClip;
    public void fireBallSoundEvent()
    {
        source.PlayOneShot(fireSoundClip);
    }
    public void slimeDeathSoundEvnet()
    {
        source.PlayOneShot(slimeDeathSoundClip);
    }
    public void dogKnightDeathSoundEvnet()
    {
        source.PlayOneShot(dogKnightDeathSoundClip);
    }
    public void dogKnightAttackSoundEvnet()
    {
        source.PlayOneShot(dogKnightAttackSoundClip);
    }
    public void turretAttackSoundEvnet()
    {
        source.PlayOneShot(turretAttackSoundClip);
    }
    public void turretDeathSoundEvnet()
    {
        source.PlayOneShot(turretDeathSoundClip);
    }
    public void eyeBatAttackSoundEvent()
    {
        source.PlayOneShot(eyeBatAttackSoundClip);
    }
    public void eyeBatDeathSoundEvent()
    {
        source.PlayOneShot(eyeBatDeathSoundClip);
    }
}
