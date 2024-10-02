using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionDisplay : Displayable
{
    public static MissionDisplay instance;

    public DisplayLevel[] displayLevels;

    public DisplayLevel currentLevel;

    /// <summary>
    /// SCORE
    /// </summary>
    public Displayable score_Displayable;
    public Image scoreBg_Image;
    public Sprite[] scoreBg_Sprites;
    public Image medal_Image;
    public Sprite[] medal_Sprites;
    public TextMeshProUGUI correctResponses_Text;
    public TextMeshProUGUI timeSpent_text;
    public TextMeshProUGUI[] uiTexts;

    public GameObject goodFeedback_Obj;
    public GameObject badFeedback_Obj;

    public Sprite[] character_Sprites;


    public Image character_Image;
    public RectTransform character_RectTransform;

    /// <summary>
    /// LIVES
    /// </summary>
    public Color[] lives_Colors;
    public Image lives_Outline;
    public Image[] lives_Images;
    public int lives = 5;

    public int maxLives = 5;

    private void Awake() {
        instance = this;
    }

    public void DisplayMission() {

        foreach (var displayLevel in displayLevels) {
            displayLevel.Hide();
        }
        FadeIn();
        score_Displayable.Hide();

        Level level = LevelManager.Instance.currentLevel;
        currentLevel = displayLevels[(int)level.type];
        displayLevels[(int)level.type].StartLevel();
        lives_Outline.color = lives_Colors[(int)level.type];

        lives = maxLives;

        UpdateCharacter();
    }

    public void ShowScore(float time, int correctAnsers, int totalAnswers) {
        score_Displayable.FadeIn();
        Level level = LevelManager.Instance.currentLevel;

        foreach (var uiText in uiTexts) {
            uiText.color = lives_Colors[(int)level.type];
        }

        scoreBg_Image.sprite = scoreBg_Sprites[(int)level.type];


        int time_i = Mathf.RoundToInt(time);
        timeSpent_text.text = $"{time_i} secondes";

        correctResponses_Text.text = $"{correctAnsers} / {totalAnswers}";

        float lerp = (float)correctAnsers / totalAnswers;
        int score = (int)lerp * 3;
        Debug.Log($"medal : {score}");
        Debug.Log($"time : {time_i}");
        score = Mathf.Clamp(score, 0, 2);
        medal_Image.sprite = medal_Sprites[score];
    }

    public void Document_Sucess() {
        DisplayMessage.Instance.FadeOut();
        goodFeedback_Obj.SetActive(true);
        UpdateCharacter();
        DisplayMedia.Instance.Reset();

        Invoke("Document_SucessDelay", 1.5f);
    }

    void Document_SucessDelay() {
        DisplayDialogue.Instance.Display($"Bravo !\n{currentLevel.GetCurrentDocument().explanation_Good}");
        DisplayDialogue.Instance.onClose += currentLevel.NextDocument;
    }

    public void Document_Fail() {
        DisplayMessage.Instance.FadeOut();
        DisplayMedia.Instance.Reset();
        badFeedback_Obj.SetActive(true);
        --lives;

        UpdateCharacter();
        Invoke("Document_FailDelay", 1.5f);
    }

    void Document_FailDelay() {
        DisplayDialogue.Instance.Display($"Raté !\n{currentLevel.GetCurrentDocument().explanation_Bad}");

        if ( lives <= 0) {
            currentLevel.BigEndLevel("Mince ! Tu n'as plus de vies. Recommence plus tard !");
            return;
        }

        DisplayDialogue.Instance.onClose += currentLevel.NextDocument;
    }


    public void Help() {
        if (string.IsNullOrEmpty(currentLevel.GetCurrentDocument().clue)) {
            Debug.Log("no clue");
            return;
        }
        DisplayDialogue.Instance.Display(currentLevel.GetCurrentDocument().clue);
    }
    

    void UpdateCharacter() {
        for (int j = 0; j < lives_Images.Length; j++) {
            lives_Images[j].color = lives > j ? Color.white : Color.clear;
        }
        Tween.Bounce(character_RectTransform);
        int i = Mathf.Clamp(lives, 0, 5);
        character_Image.sprite = character_Sprites[5-i];
    }

    public void ExitLevel() {
        Level level = LevelManager.Instance.currentLevel;
        displayLevels[(int)level.type].EndLevel();
        displayLevels[(int)level.type].FadeOut();
        FadeOut();
        score_Displayable.Hide();
        SelectionMenu.Instance.FadeIn();
        if (score_Displayable.state == State.visible)
            score_Displayable.FadeOut();

        foreach (var lvl in LevelManager.Instance.levels) {
            if (!lvl.finished) {
                return;
            }
        }


        DisplayMessage.Instance.Display(MissionIntroDisplay.Instance.gameConclusion);

    }


}
