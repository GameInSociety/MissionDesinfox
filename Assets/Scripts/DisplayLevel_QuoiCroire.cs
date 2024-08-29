using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DisplayLevel_QuoiCroire : DisplayLevel {
    public static DisplayLevel_QuoiCroire Instance;

    public Displayable[] source_Displayables;

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public TextMeshProUGUI uiText;

    int correctIndex = 0;

    bool canPress = false;

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

        canPress = true;

        base.UpdateCurrentDocument();

        for (int i = 0; i < doc.types.Count; i++) {

            // get source type 
            var url = doc.medias[i];
            string type = doc.types[i].ToLower();

            Debug.Log($"type : {doc.types[i]} / media : {url}");

            sourceButtons[i].Display(type, url);
        }


        // statement buttons
        foreach (var item in statement_Buttons) {
            item.Hide();
        }
        int index = 0;
        foreach (var button in statement_Buttons) {
            button.FadeIn();
            var statement = GetCurrentDocument().statements[index];
            if ( statement.StartsWith("(TARGET) ")) {
                correctIndex = index;
                statement = statement.Remove(0, "(TARGET) ".Length);
            }
            button.GetComponentInChildren<TextMeshProUGUI>().text = statement;
            ++index;
            button.GetTransform.SetSiblingIndex(Random.Range(0, 4));
        }
    }

    public void Submit(int i) {

        if (!canPress) {
            return;
        }

        if (i == correctIndex) {
            ++correctAnswers;
            MissionDisplay.instance.Document_Sucess();
        } else {
            MissionDisplay.instance.Document_Fail();
        }
        foreach (var source in source_Displayables) {
            source.FadeOut();
        }
        foreach (var button in sourceButtons) {
            button.FadeOut();
        }

        canPress = false;

    }

    public override void LoadMedia() {
        //base.UpdateImage();
    }

    public void CloseSource() {
        videoPlayer.Stop();
        audioSource.Stop();
    }

    public override void Update() {
        base.Update();

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
