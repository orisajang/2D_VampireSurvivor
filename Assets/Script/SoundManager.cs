using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager :Singleton<SoundManager>
{
    AudioSource myAudio;
    [SerializeField] private AudioClip[] _weaponShotClips;
    [SerializeField] private AudioClip _backgroundMusic;

    protected override void Awake()
    {
        base.Awake();
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = _backgroundMusic;
        myAudio.loop = true;
        myAudio.Play();
    }

    public void PlayThisClip(int index)
    {
        if (index < 0 || _weaponShotClips.Length <= index)
        {
            Debug.Log("소리재상 인덱스범위가 잘못됨");
            return;
        }
        myAudio.PlayOneShot(_weaponShotClips[index]);
    }
}
