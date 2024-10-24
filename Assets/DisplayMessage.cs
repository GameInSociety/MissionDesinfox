using TMPro;

public class DisplayMessage : Displayable
{
    public static DisplayMessage Instance;

    public TextMeshProUGUI uiText;

    public delegate void OnClose();
    public OnClose onClose;

    private void Awake() {
        Instance = this;
    }

    public void Display(string str) {
        FadeIn();

        uiText.text = str;
    }

    public void Close() {
        if ( onClose != null) {
            onClose();
            onClose = null;
        }
    }
}
