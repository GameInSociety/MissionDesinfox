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

    public delegate void OnClose();
    public OnClose onClose;

    private void Awake() {
        Instance = this;
    }

    public void Display(string text) {
        FadeIn();
        Tween.Bounce(transform);
        uiText.text = text;
        displayed = true;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Close();
    }

    public void Close() {
        displayed = false;
        FadeOut();
        if (onClose != null) {
            onClose();
            onClose = null;
        }
    }
}
