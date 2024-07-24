using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractibleElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public Image image;
    Color initcolor = Color.white;

    public float colorSpeed = 1f;

    public Category category;

    public bool over = false;

    public bool locked = false;

    public Sprite idle_Sprite;
    public Sprite locked_Sprite;

    public static bool canPressZone = true;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();
        image.color = Color.clear;
    }


    public void Display(int i, Category cateogry, Color c) {
        gameObject.SetActive(true);
        locked = false;
        over = false;
        index = i;
        initcolor = c;
        image.color = Color.clear;
        image.sprite = idle_Sprite;
    }

    public void Lock() {
        locked = true;
        over = true;
        image.sprite = locked_Sprite;
        initcolor = Color.black;
    }


    // Update is called once per frame
    void Update()
    {
        if ( locked) {
            image.color = Color.Lerp(image.color, initcolor, colorSpeed * Time.deltaTime);
            return;
        }
        if (over ) {
            image.color = Color.Lerp(image.color, initcolor, colorSpeed * Time.deltaTime);
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
        DisplayMessage.Instance.Display("Test Element Info");
    }
}
