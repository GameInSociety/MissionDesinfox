using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake() {
        Instance = this;
    }

    [System.Serializable]
    public class Sound {
        public string name;
        public AudioClip clip;
    }

    public AudioSource click_Source;
    public AudioSource sound_Source;
    public AudioSource music_Source;

    public List<Sound> sounds = new List<Sound>(); 
    public List<Sound> musics = new List<Sound>();

    public void PlaySound(string name) {
        var sound = sounds.Find(x=> x.name == name);
        if (name == "Click") {
            click_Source.clip = sound.clip;
            click_Source.Play();
        } else {
            sound_Source.clip = sound.clip;
            sound_Source.Play();
        }
    }

    public void PlayMusic(string name) {
        var music = musics.Find(x=> x.name == name);
        music_Source.clip = music.clip;
        music_Source.Play();
    }
}
