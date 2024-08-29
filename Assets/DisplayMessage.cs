using TMPro;

public class DisplayMessage : Displayable
{
    public static DisplayMessage Instance;

    public TextMeshProUGUI uiText;

    private void Awake() {
        Instance = this;
    }

    public void Display(string str) {
        FadeIn();

        uiText.text = str;
    }
}
