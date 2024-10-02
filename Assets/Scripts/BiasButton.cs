using TMPro;
using UnityEngine.EventSystems;

public class BiasButton : Displayable, IPointerClickHandler {
    public int index;
    public Category category;

    public TextMeshProUGUI uiText;

    public string description;

    public void Display(Category category) {
        FadeIn();
        uiText.text = category.name;

    }

    public void ShowExplanation() {
        DisplayMessage.Instance.Display(MissionIntroDisplay.Instance.biaisDefs[index]);
    }

    public void OnPointerClick(PointerEventData eventData) {
        FadeIn();
        Tween.Bounce(GetTransform);
        if (LevelManager.Instance.currentLevel.type == Level.Type.OpVsInfo) {
            DisplayLevel_OpVsInfo.Instance.PressButton(index);
        } else {
            DisplayLevel_Bias.Instance.PressButton(index);
        }
    }
}
