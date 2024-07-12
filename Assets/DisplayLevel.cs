using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel : Displayable
{
    public Image targetImage;

    public Level level;

    public int documentIndex;

    public float timer = 0f;

    private protected bool active = false;

    public int correctAnswers = 0;


    public Document GetCurrentDocument() {
        return level.documents[documentIndex];
    }

    public virtual void StartLevel() {
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
            timer += 0f;
        }
    }

    public virtual void NextDocument() {
        ++documentIndex;
        if ( documentIndex == level.documents.Count) {
            EndLevel();
            MissionDisplay.instance.ShowScore(timer, correctAnswers, level.documents.Count);
            return;
        }

        UpdateCurrentDocument();
    }

    public virtual void UpdateCurrentDocument() {

        UpdateImage();

        Invoke("UpdateCurrentDocumentDelay", 0.5f);
    }

    public virtual void UpdateImage() {
        var sprite = GetCurrentDocument().GetSprite();
        targetImage.sprite = sprite;
        targetImage.transform.localScale = Vector3.zero;
        targetImage.transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
        targetImage.DOColor(Color.white, 0.5f);
    }

    public virtual void UpdateCurrentDocumentDelay() {
        ShowImage();
        MissionDisplay.instance.badFeedback_Obj.SetActive(false);
        MissionDisplay.instance.goodFeedback_Obj.SetActive(false);
    }

    public virtual void ShowImage() {
        Tween.Bounce(targetImage.transform);
    }

    public virtual void EndLevel() {
        FadeOut();
        active = false;
    }
}
