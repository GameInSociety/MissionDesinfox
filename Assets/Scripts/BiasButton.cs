using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BiasButton : Displayable, IPointerClickHandler {
    public int index;
    public Category category;

    public TextMeshProUGUI uiText;

    public void Display(Category category) {
        FadeIn();
        uiText.text = category.name;

    }

    public void OnPointerClick(PointerEventData eventData) {
        FadeIn();
        Tween.Bounce(GetTransform);
        DisplayLevel_Bias.Instance.PressButton(index);
    }
}