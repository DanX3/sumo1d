using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sources")]
    [SerializeField] private AudioSource attackSource;
    [SerializeField] private AudioSource damageReceivedSource;
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource crowdVariationsSource;
    [SerializeField] private AudioSource powerupSource;

    [Header("Clips")]
    [SerializeField] private AudioClip gong;
    [SerializeField] private AudioClip fireworks;

    [SerializeField] private AudioClip applaudingCrowd;
    [SerializeField] private AudioClip laughingCrowd;
    [SerializeField] private AudioClip shortApplaudingCrowd;
    [SerializeField] private AudioClip gaspingCrowd;

    [SerializeField] private List<AudioClip> attackSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> damageReceivedSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> powerupsSounds = new List<AudioClip>();


    private void PlayOneShotSound(AudioClip clip, float volume = 1f)
    {
        oneShotSource.clip = clip;
        oneShotSource.volume = volume;
        oneShotSource.Play();
    }

    private void PlayCrowdVariationSound(AudioClip clip, float volume = 1f)
    {
        oneShotSource.clip = clip;
        oneShotSource.volume = volume;
        oneShotSource.Play();
    }

    public void PlayGong()
    {
        PlayOneShotSound(gong);
    }

    public void PlayGaspingCrowd()
    {
        PlayCrowdVariationSound(gaspingCrowd);
    }

    public void PlayLaughingCrowd()
    {
        PlayCrowdVariationSound(laughingCrowd, 0.2f);
    }

    public void PlayApplaudingCrowd()
    {
        PlayCrowdVariationSound(applaudingCrowd, 0.2f);
    }

    public void PlayShortApplaudingCrowd()
    {
        PlayCrowdVariationSound(shortApplaudingCrowd);
    }

    public void PlayFireworks()
    {
        PlayOneShotSound(fireworks);
    }



    public void PlayRandomAttackSound()
    {
        var random = Random.Range(0, attackSounds.Count);
        attackSource.clip = attackSounds[random];
        attackSource.Play();
    }

    public void PlayRandomDamageReceivedSound()
    {
        var random = Random.Range(0, damageReceivedSounds.Count);
        damageReceivedSource.clip = damageReceivedSounds[random];
        damageReceivedSource.Play();
    }

    public void PlayRandomPowerupsSound()
    {
        var random = Random.Range(0, powerupsSounds.Count);
        powerupSource.clip = powerupsSounds[random];
        powerupSource.Play();
    }

    public void PlayOpponentDefeated()
    {
        PlayApplaudingCrowd();
        PlayFireworks();
    }
}
