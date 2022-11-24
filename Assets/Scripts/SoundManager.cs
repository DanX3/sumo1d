using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sources")]
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource damageReceivedSound;
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource loopingSource;

    [Header("Clips")]
    [SerializeField] private AudioClip gong;
    [SerializeField] private AudioClip fireworks;

    [SerializeField] private AudioClip applaudingCrowd;
    [SerializeField] private AudioClip laughingCrowd;
    [SerializeField] private AudioClip shortApplaudingCrowd;
    [SerializeField] private AudioClip gaspingCrowd;

    [SerializeField] private List<AudioClip> attackSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> damageReceivedSounds = new List<AudioClip>();


    private void PlayOneShotSound(AudioClip clip)
    {
        oneShotSource.clip = clip;
        oneShotSource.Play();
    }

    private void PlayLoopingSound(AudioClip clip)
    {
        loopingSource.clip = clip;
        loopingSource.Play();
    }

    public void PlayGong()
    {
        PlayOneShotSound(gong);
    }

    public void PlayGaspingCrowd()
    {
        PlayOneShotSound(gaspingCrowd);
    }

    public void PlayLaughingCrowd()
    {
        PlayOneShotSound(laughingCrowd);
    }

    public void PlayApplaudingCrowd()
    {
        PlayOneShotSound(applaudingCrowd);
    }

    public void PlayShortApplaudingCrowd()
    {
        PlayOneShotSound(shortApplaudingCrowd);
    }

    public void PlayFireworks()
    {
        PlayLoopingSound(fireworks);
    }

    public void PlayRandomAttackSound()
    {
        var random = Random.Range(0, attackSounds.Count);
        attackSound.clip = attackSounds[random];
        attackSound.Play();
    }

    public void PlayRandomDamageReceivedSound()
    {
        var random = Random.Range(0, damageReceivedSounds.Count);
        damageReceivedSound.clip = damageReceivedSounds[random];
        damageReceivedSound.Play();
    }

    public void PlayOpponentDefeated()
    {
        PlayApplaudingCrowd();
        PlayFireworks();
    }
}
