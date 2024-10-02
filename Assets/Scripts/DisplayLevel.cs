using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel : Displayable
{
    // current level
    public Level level;
    public int documentIndex;
    public float timer = 0f;
    private protected bool active = false;
    public int correctAnswers = 0;


    public Document GetCurrentDocument() {
        return level.documents[documentIndex];
    }

    public virtual void StartLevel() {
        DisplayMedia.Instance.Hide();
        FadeIn();
        documentIndex = 0;
        correctAnswers = 0;
        active = true;
        timer = 0f;
        level = LevelManager.Instance.currentLevel;
        UpdateCurrentDocument();

    }

    public virtual void Update() {
        if (active) {
            timer += Time.deltaTime;
        }
    }

    string phrase = "";
    public void BigEndLevel(string _phrase) {
        phrase = _phrase;
        EndLevel();
        Invoke("End2", 1f);
        Invoke("End3", 2f);
    }

    public virtual void NextDocument() {
        ++documentIndex;
        if ( MissionDisplay.instance.lives <= 0 || documentIndex == level.documents.Count) {
            Debug.Log($"ENDING LEVEL");
            BigEndLevel("Le niveau est terminé !");
            //MissionDisplay.instance.ShowScore(timer, correctAnswers, level.documents.Count);
            return;
        }

        UpdateCurrentDocument();
    }

    public void EndLevel() {
        level.finished = true;
        active = false;
    }
    void End2() {
        DisplayDialogue.Instance.Display(phrase);

    }
    void End3() {
        MissionDisplay.instance.ShowScore(timer, correctAnswers, level.documents.Count);
    }

    public void ReturnToMenu() {
        MissionDisplay.instance.FadeOut();
        SelectionMenu.Instance.FadeIn();
    }

    public virtual void UpdateCurrentDocument() {

        LoadMedia();
        MissionDisplay.instance.badFeedback_Obj.SetActive(false);
        MissionDisplay.instance.goodFeedback_Obj.SetActive(false);
    }

    public virtual void LoadMedia() {
        DisplayMedia.Instance.onMediaDownloaded += OnMediaDownloaded;
        DisplayMedia.Instance.LoadMedia(GetCurrentDocument().types[0], GetCurrentDocument().medias[0], false);
        
    }

    public virtual void OnMediaDownloaded() {
        DisplayMedia.Instance.onMediaDownloaded = null;
    }


}
