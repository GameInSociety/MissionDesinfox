using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDialogue : Displayable, IPointerClickHandler {
    public static DisplayDialogue Instance;

    public TextMeshProUGUI uiText;

    float timer = 0f;

    bool displayed = false;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if ( displayed ) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                displayed = false;
                FadeOut();
            }
        }

    }

    public void Display(string text) {
        FadeIn();
        Tween.Bounce(transform);
        uiText.text = text;
        timer = 2f;
        displayed = true;
    }

    public void OnPointerClick(PointerEventData eventData) {
        transform.DOScale(0f, 1f).SetEase(Ease.InBounce);
        FadeOut();
    }
}
