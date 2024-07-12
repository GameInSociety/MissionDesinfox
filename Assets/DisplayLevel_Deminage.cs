using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class DisplayLevel_Deminage : DisplayLevel
{
    public Displayable[] displayables;
    public TextMeshProUGUI[] uiTexts;

    public Image mask_image;

    public Transform scaler;

    public GameObject prefab;
    public List<GameObject> prefabList = new List<GameObject>();

    public RectTransform cadre;

    public override void Start() {
        base.Start();
        uiTexts = new TextMeshProUGUI[displayables.Length];
        for (int i = 0; i < displayables.Length; i++) {
            uiTexts[i] = displayables[i].GetComponentInChildren<TextMeshProUGUI>();
        }

    }

    public override void Update() {
        base.Update();

    }

    public void PressCatButton(int i) {
        Debug.Log($"Pressed {i}");
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();

        foreach (var displayable in displayables) {
            displayable.Hide();
        }
        StartCoroutine(ShowButtonsCoroutine());
    }

    IEnumerator ShowButtonsCoroutine() {

        var categories = GetCurrentDocument().categories;
        for (int i = 0; i < categories.Count; i++) {
            yield return new WaitForSeconds(0.5f);
            displayables[i].FadeIn();
            uiTexts[i].text = categories[i].name;
        }
    }

    [System.Serializable]
    public class PixelGroup {
        public Vector2 start;
        public Vector2 end;
        public Color c;
        public string hexa;
    }

    public List<PixelGroup> pixelGroups = new List<PixelGroup>();

    public override void UpdateImage() {
        base.UpdateImage();

        targetImage.SetNativeSize();
        mask_image.sprite = GetCurrentDocument().GetMask();
        mask_image.SetNativeSize();

        StartCoroutine(image());
    }

    IEnumerator image() {

        yield return new WaitForSeconds(2f);
        for (int x = 0; x < mask_image.mainTexture.width; x++) {
            for (int y = 0; y < mask_image.mainTexture.height; y++) {

                var color = mask_image.sprite.texture.GetPixel(x, y);
                if (color.a < 0.1f)
                    continue;

                var pixelGroup = pixelGroups.Find(x => x.c == color);
                if (pixelGroup == null) {
                    pixelGroup = new PixelGroup();
                    pixelGroup.start = new Vector2(x, y);
                    pixelGroup.c = color;
                    pixelGroups.Add(pixelGroup);
                }

                if (pixelGroup.end.x < x)
                    pixelGroup.end.x = x;
                if (pixelGroup.end.y < y)
                    pixelGroup.end.y = y;

            }
        }

        foreach (var item in prefabList) {
            item.SetActive(false);
        }

        int index = 0;
        foreach (var item in pixelGroups) {
            if (index >= prefabList.Count) {
                var go = Instantiate(prefab, scaler);
                prefabList.Add(go);
            }

            Vector2 pos = item.start;

            prefabList[index].GetComponent<RectTransform>().sizeDelta = new Vector2(item.end.x - item.start.x, item.end.y - item.start.y); ;
            prefabList[index].GetComponent<RectTransform>().anchoredPosition = item.start;
            prefabList[index].GetComponent<Image>().color = item.c;
            index++;
            yield return new WaitForEndOfFrame();
        }


        var textureScale = new Vector2(mask_image.sprite.texture.width, mask_image.sprite.texture.height);


        targetImage.rectTransform.sizeDelta = cadre.sizeDelta;

        yield return new WaitForEndOfFrame();

        Debug.Log($"init scale : {mask_image.sprite.texture.width}");
        Debug.Log($"new scale : {targetImage.rectTransform.rect.width}");

        Vector2 s = targetImage.rectTransform.sizeDelta;

        var nl = targetImage.rectTransform.rect.width / textureScale.x;

        Debug.Log($"lerp : {nl}");

        s.y = targetImage.rectTransform.rect.width / textureScale.x * textureScale.y;
        targetImage.rectTransform.sizeDelta = s;

        yield return new WaitForEndOfFrame();




        var rap = targetImage.rectTransform.rect.size / textureScale;
        Debug.Log($"rap : {rap}");

        scaler.localScale = rap;
    }
}
