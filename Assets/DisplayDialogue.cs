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

    private void Awake() {
        Instance = this;
    }

    public void Display(string text) {
        FadeIn();
        Tween.Bounce(transform);
        uiText.text = text;
    }

    public void OnPointerClick(PointerEventData eventData) {
        transform.DOScale(0f, 1f).SetEase(Ease.InBounce);
        FadeOut();
    }
}
