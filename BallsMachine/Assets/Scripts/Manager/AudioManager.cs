using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : SingletonMono<AudioManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<SoundData> AudioList = new List<SoundData>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAuidio(EAudio name, float volume = 1)
    {
        SoundData data = AudioList.Find(x => x.Name == name);
        if (data != null)
        {
            _audioSource.PlayOneShot(data.Clip, volume);
        }
    }
}

[Serializable]
public class SoundData
{
    [SerializeField] private EAudio _name;
    [SerializeField] private AudioClip _clip;

    public EAudio Name => _name;
    public AudioClip Clip => _clip;
}
