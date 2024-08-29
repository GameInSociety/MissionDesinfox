using TMPro;
using UnityEngine.EventSystems;

public class DeminageButton : Displayable, IPointerClickHandler {

    public Category category;

    public bool locked = false;

    public int index;
    public TextMeshProUGUI uiText;

    public void OnPointerClick(PointerEventData eventData) {
        if (locked) return;
        Tween.Bounce(transform);
        DisplayLevel_Deminage.Instance.PressCatButton(index);
    }

    public void Lock() {
        locked = true;
        FadeOut();
    }

    public void Display(string text) {
        if (locked) {
            FadeTo(.5f);
        } else {
            FadeIn();
        }
        uiText.text = text;
        Tween.Bounce(GetTransform);
    }

}
