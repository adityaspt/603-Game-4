using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets i
    {
        get
        {
            // if (_i == null) _i = (Instantiate(Resources.Load("SoundAssets")) as GameObject).GetComponent<SoundAssets>();
            if (_i == null) _i = GameObject.FindObjectOfType<SoundAssets>(); //Word sambhar Changed code
            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClips;
    private static SoundAssets _i;


    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sounds sounds;
        public AudioClip audioClip;
    }

}
