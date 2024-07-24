using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DisplayLevel_QuoiCroire : DisplayLevel {
    public static DisplayLevel_QuoiCroire Instance;

    public Displayable[] source_Displayables;

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public TextMeshProUGUI uiText;

    public enum SourceType {
        Image,
        Text,
        Audio,
        Video,
    }

    public Displayable[] source_Buttons;
    public Displayable[] statement_Buttons;

    private void Awake() {
        Instance = this;
    }

    public override void UpdateCurrentDocument() {


        foreach (var source in source_Displayables) {
            source.Hide();
        }

        base.UpdateCurrentDocument();

        for (int i = 0; i < 4; i++) {

            var sourceType = (SourceType)i;

            switch (sourceType) {
                case SourceType.Image:
                    if (string.IsNullOrEmpty(GetCurrentDocument().imageName))
                        continue;
                    break;
                case SourceType.Text:
                    if (string.IsNullOrEmpty(GetCurrentDocument().text))
                        continue;
                    break;
                case SourceType.Audio:
                    if (string.IsNullOrEmpty(GetCurrentDocument().audio_path))
                        continue;
                    break;
                case SourceType.Video:
                    if (string.IsNullOrEmpty(GetCurrentDocument().video_path))
                        continue; 
                    break;
            }
            Debug.Log("fidnfins");

            source_Buttons[i].FadeIn();

        }


        // statement buttons
        foreach (var item in statement_Buttons) {
            item.Hide();
        }
        int index = 0;
        foreach (var button in statement_Buttons) {
            button.FadeIn();
            var statement = GetCurrentDocument().statements[index];
            button.GetComponentInChildren<TextMeshProUGUI>().text = statement;
            ++index;
        }
    }

    public override void UpdateImage() {
        base.UpdateImage();
    }

    public void CloseSource() {
        videoPlayer.Stop();
        audioSource.Stop();
    }

    public void ShowSource(int i) {
        Debug.Log($"showing : {(SourceType)i}");

        source_Displayables[i].FadeIn();

        switch ((SourceType)i) {
            case SourceType.Image:
                break;
            case SourceType.Text:
                uiText.text = GetCurrentDocument().text;
                break;
            case SourceType.Audio:
                playAudio();
                break;
            case SourceType.Video:
                playVideo();
                break;
            default:
                break;
        }
    }

    void playAudio() {
        string path = $"QuoiCroire/{GetCurrentDocument().audio_path}";
        Debug.Log(path);
        var clip = Resources.Load<AudioClip>(path);
        if (clip == null) {
            Debug.LogError($"no audio clip for path : {GetCurrentDocument().audio_path}");
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    void playVideo() {
        string path = $"QuoiCroire/{GetCurrentDocument().video_path}";
        Debug.Log(path);
        var clip = Resources.Load<VideoClip>(path);
        if (clip == null) {
            Debug.LogError($"no video clip for path : {GetCurrentDocument().video_path}");
        }
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }


}
