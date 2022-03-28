using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    void Start()
    {
        FindBGMusicAssets();
    }
    public static void FindBGMusicAssets()
    {
        if (SoundManager.audioClipsList.Count <= 0)
        {
            foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClips)
            {
                if (soundAudioClip.sounds.ToString().Contains("BGMusic"))
                {
                    SoundManager.audioClipsList.Add(soundAudioClip.audioClip);
                }
            }
        }
        else
        {
            Debug.Log("Already has all the music assets");
        }
    }
}
