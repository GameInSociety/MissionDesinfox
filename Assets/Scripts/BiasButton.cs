using TMPro;
using UnityEngine.EventSystems;

public class BiasButton : Displayable, IPointerClickHandler {
    public int index;
    public Category category;

    public TextMeshProUGUI uiText;

    public bool biais = false;

    public string description;

    public void Display(Category category) {
        FadeIn();
        uiText.text = category.name;

    }

    public void ShowExplanation() {
        DisplayMessage.Instance.Display(biais ? MissionIntroDisplay.Instance.biaisDefs[index] : MissionIntroDisplay.Instance.hpDefs[index]);
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
