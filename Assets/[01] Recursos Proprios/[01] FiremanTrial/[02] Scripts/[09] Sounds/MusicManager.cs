
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FiremanTrial
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips; 
        private AudioSource audioSource;                      
        private static MusicManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (audioSource.isPlaying) return;
            if (audioClips.Count > 0) PlayRandomAudioClip();
        }
        
        private void PlayRandomAudioClip()
        {
            audioSource.clip = GetRandomAudioClip();
            audioSource.Play();
            Invoke(nameof(PlayNextRandomAudio), audioSource.clip.length);
        }
        
        private void PlayNextRandomAudio() => PlayRandomAudioClip();

        private AudioClip GetRandomAudioClip() => audioClips[ Random.Range(0, audioClips.Count)];
    }
}
