using DG.Tweening;
using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class DisplayLevel_Deminage : DisplayLevel
{
    public static DisplayLevel_Deminage Instance;

   public  bool canPressButton = false;
    public bool canPressZone = false;

    int catsCount = 0;

    public DeminageButton[] buttons;

    public Image mask_image;

    public Transform scaler;

    public Category zoneCategory;

    public DeminageZone zonePrefab;
    public List<DeminageZone> zones = new List<DeminageZone>();

    public RectTransform cadre;

    private void Awake() {
        Instance = this;
    }

    public override void Start() {
        base.Start();
        foreach (var item in buttons) {
            item.Hide();
        }
    }

    public override void StartLevel() {
        base.StartLevel();
        canPressZone = true;
    }

    public void PressZone (int i) {
        if ( i >= GetCurrentDocument().categories.Count) {
            Debug.Log($"ERROR : more zones than categories on db");
            return;
        }
        if (!canPressZone)
            return;
        canPressZone = false;
        canPressButton = true;
        zoneCategory = GetCurrentDocument().categories[i];
        Debug.Log($"pressed zone : {zoneCategory.name}");
        StartCoroutine(ShowButtonsCoroutine());
    }

    public void PressCatButton(int i) {
        if (!canPressButton) return;
        // continue
        canPressButton = false;
        canPressZone = true;

        foreach (var item in buttons) {
            item.FadeOut();
        }
        var pressedCat = GetCurrentDocument().categories[i];
        Debug.Log($"Match {pressedCat.name} / {zoneCategory.name}");

        if ( pressedCat.name == zoneCategory.name) {
            ++catsCount;
            MissionDisplay.instance.DisplayGoodFeedback();
            zones[i].Lock();
            buttons[i].Lock();
            if (catsCount == GetCurrentDocument().categories.Count) {
                ++correctAnswers;
                // finish
                targetImage.DOColor(Color.clear, 0.5f);
                foreach (var item in buttons) {
                    item.FadeOut();
                }
                Invoke("NextDocument", 1f);
                return;
            } else {

            }
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
            foreach (var zone in zones) {
                zone.over = false;
            }

        }

       

       
            
    }

    public override void UpdateCurrentDocument() {
        base.UpdateCurrentDocument();

        catsCount = 0;
        foreach (var displayable in buttons) {
            displayable.locked = false;
            displayable.Hide();
        }
        //StartCoroutine(ShowButtonsCoroutine());
    }

    IEnumerator ShowButtonsCoroutine() {

        var categories = GetCurrentDocument().categories;
        for (int i = 0; i < categories.Count; i++) {
            
            buttons[i].Display(categories[i].name);
            buttons[i].index = i;
            yield return new WaitForSeconds(0.01f);
        }
    }

    [System.Serializable]
    public class PixelGroup {
        public Vector2 start;
        public Vector2 end;
        public Color color;
        public string hexa;
    }

    public List<PixelGroup> pixelGroups = new List<PixelGroup>();

    public override void UpdateImage() {
        //base.UpdateImage();
        var sprite = GetCurrentDocument().GetSprite();
        targetImage.sprite = sprite;
        targetImage.SetNativeSize();
        mask_image.sprite = GetCurrentDocument().GetMask();
        mask_image.SetNativeSize();
        targetImage.color = Color.white;

        StartCoroutine(image());
    }

    void puer() {
        targetImage.transform.localScale = Vector3.zero;
        targetImage.transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
        targetImage.DOColor(Color.white, 0.5f);
    }

    IEnumerator image() {

        pixelGroups.Clear();
        yield return new WaitForEndOfFrame();
        for (int x = 0; x < mask_image.mainTexture.width; x++) {
            for (int y = 0; y < mask_image.mainTexture.height; y++) {

                var color = mask_image.sprite.texture.GetPixel(x, y);
                if (color.a < 0.1f)
                    continue;

                var pixelGroup = pixelGroups.Find(x => x.color == color);
                if (pixelGroup == null) {
                    pixelGroup = new PixelGroup();
                    pixelGroup.start = new Vector2(x, y);
                    pixelGroup.color = color;
                    pixelGroups.Add(pixelGroup);
                }

                if (pixelGroup.end.x < x)
                    pixelGroup.end.x = x;
                if (pixelGroup.end.y < y)
                    pixelGroup.end.y = y;

            }
        }

        foreach (var item in zones) {
            item.gameObject.SetActive(false);
        }

        int index = 0;
        foreach (var item in pixelGroups) {
            if (index >= GetCurrentDocument().categories.Count)
                break;
            if (index >= zones.Count) {
                DeminageZone dz = Instantiate(zonePrefab, scaler);
                zones.Add(dz);
            }

            Vector2 pos = item.start;


            zones[index].gameObject.SetActive(true);
            zones[index].GetComponent<RectTransform>().sizeDelta = new Vector2(item.end.x - item.start.x, item.end.y - item.start.y); ;
            zones[index].GetComponent<RectTransform>().anchoredPosition = item.start;
            zones[index].Display(index, GetCurrentDocument().categories[index], item.color);

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
