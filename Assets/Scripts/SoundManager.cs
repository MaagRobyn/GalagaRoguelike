using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class SoundManager
    {
        public static SoundManager Instance { get; set; }
        private float MasterVolume = 1;
        private float SoundVolume = 1;
        private float MusicVolume = 1;
        public SoundManager() 
        {
            Instance = this;
        }

        public void SetVolume(int volume, SoundType type = SoundType.Master) 
        {
            switch (type)
            {
                case SoundType.Master:
                    MasterVolume = volume > 100 ? 1 : volume / 100;
                    break;
                case SoundType.Music:
                    MusicVolume = volume > 100 ? 1 : volume / 100;
                    break;
                case SoundType.Sound:
                    SoundVolume = volume > 100 ? 1 : volume / 100;
                    break;
            }
        }

        public int GetVolume(SoundType type = SoundType.Master)
        {
            var output = GetVolumeFloat(type) * 100;
            return (int)output;
        }
        public float GetVolumeFloat(SoundType type = SoundType.Master)
        {

            var output = type switch
            {
                SoundType.Master => MasterVolume,
                SoundType.Music => MusicVolume,
                SoundType.Sound => SoundVolume,
                _ => 0
            };
            return output;
        }

        public void PlaySound(AudioSource sound, SoundType type = SoundType.Sound, float volumeModifier = 1.0f)
        {
            var volumeTypeMod = type switch
            {
                SoundType.Sound => SoundVolume,
                SoundType.Music => MusicVolume,
                _ => 1.0f
            };
            // If the volume would be more than 1.0f, set it to 1.0f
            sound.volume = 
                volumeModifier * MasterVolume * volumeTypeMod <= 1.0f 
                ? volumeModifier * MasterVolume * volumeTypeMod 
                : 1.0f;
            sound.Play();
        }


        public enum SoundType
        {
            Master = 0,
            Music = 1,
            Sound = 2
        }
    }
}
