using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectionMenu : Displayable
{
    public static SelectionMenu Instance;

    public Image sound_Image;
    public Sprite[] sound_Sprites;

    public bool sound_Enabled = true;

    public VideoPlayer[] players;

    private void Awake() {
        Instance = this;
    }

    public override void Start() {
        base.Start();
    }

    public override void Show() {
        base.Show();
        SoundManager.Instance.PlayMusic("Menu");
    }

    public void SwitchSound() {
        sound_Enabled = !sound_Enabled;
        sound_Image.sprite = sound_Sprites[sound_Enabled ? 0 : 1];
        if ( sound_Enabled) {
            foreach (var item in players) {
                item.audioOutputMode = VideoAudioOutputMode.Direct;
            }
        } else {
            foreach (var item in players) {
                item.audioOutputMode = VideoAudioOutputMode.None;
            }
        }
        SoundManager.Instance.music_Source.volume = sound_Enabled ? 1 : 0;
        SoundManager.Instance.sound_Source.volume = sound_Enabled ? 1 : 0;
        SoundManager.Instance.click_Source.volume = sound_Enabled ? 1 : 0;
    }


}
