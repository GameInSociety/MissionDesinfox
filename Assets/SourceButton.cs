using UnityEngine;
using UnityEngine.EventSystems;

public class SourceButton : Displayable, IPointerClickHandler {
    public string type;
    public string url;
    public int index;
    public GameObject[] groups;

    public void Display(string type, string url) {
        this.type = type.ToLower();
        this.url = url;
        
        FadeIn();
        foreach (GameObject group in groups)
            group.SetActive(false);

        switch (type) {
            case "image":
                groups[0].SetActive(true);
                break;
            case "video":
                groups[3].SetActive(true);
                break;
            case "audio":
                groups[2].SetActive(true);
                break;
            case "text":
                groups[1].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        Tween.Bounce(GetTransform);
        DisplayMedia.Instance.LoadMedia(type, url, true);

        Debug.Log($"clicked {type}");
    }
}
