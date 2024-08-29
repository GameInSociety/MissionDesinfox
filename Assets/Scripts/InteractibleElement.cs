using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TerrainUtils;
using UnityEngine.UI;

public class InteractibleElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public Image image;

    public float colorSpeed = 1f;

    public Category category;

    public bool over = false;

    public bool locked = false;
    public static bool canPressZone = true;

    public string text;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        image.color = Color.clear;
    }


    public void Display(int i, string _text, Color c) {
        gameObject.SetActive(true);
        locked = false;
        text = _text;
        over = false;
        index = i;
        image.color = Color.clear;
    }

    public void Lock() {
        locked = true;
        over = true;
    }


    // Update is called once per frame
    void Update()
    {
        if ( locked) {
            return;
        }
        if (over ) {
            image.color = Color.Lerp(image.color, Color.cyan, colorSpeed * Time.deltaTime);
        } else {
            image.color = Color.Lerp(image.color, Color.clear, colorSpeed * Time.deltaTime);
        }


    }

    public void OnPointerExit(PointerEventData eventData) {
        if (locked) return;
        if (!canPressZone) {
            return;
        }
        over = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (locked) return;
        if (!canPressZone) {
            return;
        }
        over = true;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (locked) return;
        if(!canPressZone) {
            return;
        }
        Tween.Bounce(transform);
        DisplayDialogue.Instance.Display(text);
    }
}
