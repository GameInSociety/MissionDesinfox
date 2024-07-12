using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeminageZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    Image image;
    Color initcolor = Color.white;

    float timer = 0f;
    public float colorSpeed = 1f;

    bool over = false;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        initcolor = image.color;
        image.color = Color.clear;
    }


    // Update is called once per frame
    void Update()
    {
        if ( over) {
            timer -= Time.deltaTime;
            image.color = Color.Lerp(image.color, initcolor, colorSpeed * Time.deltaTime);
        } else {
            image.color = Color.Lerp(image.color, Color.clear, colorSpeed * Time.deltaTime);
        }


    }

    public void OnPointerExit(PointerEventData eventData) {
        over = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        over = true;
    }
}
