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

    public SourceButton[] sourceButtons = new SourceButton[4];

    public Displayable[] statement_Buttons;

    private void Awake() {
        Instance = this;
    }

    public override void UpdateCurrentDocument() {
        var doc = GetCurrentDocument();

        foreach (var source in source_Displayables) {
            source.Hide();
        }
        foreach (var button in sourceButtons) {
            button.Hide();
        }

        base.UpdateCurrentDocument();

        for (int i = 0; i < doc.sources.Count; i++) {

            // get source type 
            var source = doc.sources[i];
            var sourceType = (SourceType)i;

            if (source.EndsWith(".png")) {
                // image
                sourceType = SourceType.Image;
            } else if (source.EndsWith(".mp3")) {
                // audio
                sourceType = SourceType.Audio;
            } else if (source.EndsWith(".mp3")) {
                // video
                sourceType = SourceType.Video;
            } else {
                // text
                sourceType = SourceType.Text;
            }

            sourceButtons[i].Display(i, sourceType);
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
            button.GetTransform.SetSiblingIndex(Random.Range(0, 4));
        }
    }

    public void Submit(int i) {
        if (i == 0) {
            ++correctAnswers;
            MissionDisplay.instance.DisplayGoodFeedback();
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
        }
    }

    public override void UpdateImage() {
        //base.UpdateImage();
    }

    public void CloseSource() {
        videoPlayer.Stop();
        audioSource.Stop();
    }

    public void ShowSource(string source, SourceType type) {

        source_Displayables[(int)type].FadeIn();
        Debug.Log($"showing source : {source} for type {type}");

        switch (type) {
            case SourceType.Image:
                source = source.Remove(source.Length - 4);
                Debug.Log($"iamge source : {source}");
                string path = $"QC_Sources/{source}";
                var sprite = Resources.Load<Sprite>(path);

                if (sprite == null) {
                    Debug.LogError($"no texture for document {name} / QC image : {source}");
                    return;
                }

                targetImage.sprite = sprite;
                break;
            case SourceType.Text:
                uiText.text = source;
                break;
            case SourceType.Audio:
                playAudio(source);
                break;
            case SourceType.Video:
                playVideo(source);
                break;
            default:
                break;
        }
    }

    void playAudio(string source) {
        string path = $"QC_Sources/{source}";
        Debug.Log(path);
        var clip = Resources.Load<AudioClip>(path);
        if (clip == null) {
            Debug.LogError($"no audio clip for path : {source}");
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    void playVideo(string source) {
        string path = $"QC_Sources/{source}";
        Debug.Log(path);
        var clip = Resources.Load<VideoClip>(path);
        if (clip == null) {
            Debug.LogError($"no video clip for path : {source}");
        }
        videoPlayer.clip = clip;
        videoPlayer.Play();
    }


}
