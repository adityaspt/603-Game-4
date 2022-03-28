using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sounds
    {
        BGMusic,
        resourceCollectSFX,
        resourceDropSFX,
        upgradeBuySFX,
        premiumCoinBuySFX,
        upgradeReadySFX,
        clickSFX
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static GameObject bgMusicGameObject;
    public static AudioSource bgMusicAudioSource;

    public static List<AudioClip> audioClipsList = new List<AudioClip>();

    public static void PlaySound(Sounds sounds)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One Shot Sound Object");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sounds));
    }


    public static void PlayBackgroundMusic()
    {
        BackgroundSound.FindBGMusicAssets();
        if (bgMusicGameObject == null)
        {
            bgMusicGameObject = new GameObject("BG Music Object");
            bgMusicAudioSource = bgMusicGameObject.AddComponent<AudioSource>();
            bgMusicAudioSource.clip = SoundManager.audioClipsList[0];
            bgMusicAudioSource.volume = 0.75f;
            bgMusicAudioSource.Play();
            bgMusicAudioSource.loop = true;
        }
    }

    private static AudioClip GetAudioClip(Sounds sounds)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClips)
        {
            if (soundAudioClip.sounds == sounds)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sounds + " not found!");
        return null;
    }
}
