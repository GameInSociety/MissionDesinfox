using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel : Displayable
{
    // target image
    public Image targetImage;

    // current level
    public Level level;
    public int documentIndex;
    public float timer = 0f;
    private protected bool active = false;
    public int correctAnswers = 0;

    // interactible elements
    public GameObject loading_group;
    public Image mask_image;
    public Transform interactibleElement_Parent;
    public InteractibleElement interactibleElement_Prefab;
    public List<InteractibleElement> interactibleElements = new List<InteractibleElement>();
    public RectTransform cadre;

    [System.Serializable]
    public class PixelGroup {
        public Vector2 start;
        public Vector2 end;
        public Color color;
        public string hexa;
    }

    public List<PixelGroup> pixelGroups = new List<PixelGroup>();


    public Document GetCurrentDocument() {
        return level.documents[documentIndex];
    }

    public virtual void StartLevel() {
        FadeIn();
        documentIndex = 0;
        correctAnswers = 0;
        active = true;
        timer = 0f;
        level = LevelManager.Instance.currentLevel;
        UpdateCurrentDocument();

    }

    public virtual void Update() {
        if (active) {
            timer += 0f;
        }
    }

    public virtual void NextDocument() {
        ++documentIndex;
        if ( documentIndex == level.documents.Count) {
            EndLevel();
            MissionDisplay.instance.ShowScore(timer, correctAnswers, level.documents.Count);
            return;
        }

        UpdateCurrentDocument();
    }

    public virtual void UpdateCurrentDocument() {

        UpdateImage();
        MissionDisplay.instance.badFeedback_Obj.SetActive(false);
        MissionDisplay.instance.goodFeedback_Obj.SetActive(false);
    }

    public virtual void UpdateImage() {
        targetImage.sprite = GetCurrentDocument().GetSprite(); ;
        targetImage.SetNativeSize();

        if ( mask_image != null ) {
            mask_image.sprite = GetCurrentDocument().GetMask();
            mask_image.SetNativeSize();

            StartCoroutine(CreateInteractibleElements());
        }
    }


    public virtual void EndLevel() {
        FadeOut();
        active = false;
    }

    #region interactible elements
    IEnumerator CreateInteractibleElements() {

        Debug.Log($"Loading Interactible Elements");

        // show loading
        loading_group.SetActive(true);

        // clear all
        pixelGroups.Clear();
        targetImage.color = Color.clear;

        yield return new WaitForEndOfFrame();


        int loadLimit = 0;

        for (int x = 0; x < mask_image.mainTexture.width; x++) {
            for (int y = 0; y < mask_image.mainTexture.height; y++) {

                // get pixel color of mask image
                var color = mask_image.sprite.texture.GetPixel(x, y);
                if (color.a < 0.1f)
                    continue;

                // find matching pixel group
                var pixelGroup = pixelGroups.Find(x => x.color == color);
                if (pixelGroup == null) {
                    // create if none
                    pixelGroup = new PixelGroup();
                    pixelGroup.start = new Vector2(x, y);
                    pixelGroup.color = color;
                    pixelGroups.Add(pixelGroup);
                }

                // set scale
                if (pixelGroup.end.x < x)
                    pixelGroup.end.x = x;
                if (pixelGroup.end.y < y)
                    pixelGroup.end.y = y;

            }

            ++loadLimit;
            if (loadLimit > 30) {
                loadLimit = 0;
                yield return new WaitForEndOfFrame();
            }


        }

        // hide all
        foreach (var item in interactibleElements)
            item.gameObject.SetActive(false);

        // create & place all elements
        int index = 0;
        foreach (var item in pixelGroups) {

            if (index >= GetCurrentDocument().categories.Count)
                break;

            // instantiate 
            if (index >= interactibleElements.Count) {
                InteractibleElement dz = Instantiate(interactibleElement_Prefab, interactibleElement_Parent);
                interactibleElements.Add(dz);
            }

            Vector2 pos = item.start;

            interactibleElements[index].gameObject.SetActive(true);
            interactibleElements[index].GetComponent<RectTransform>().sizeDelta = new Vector2(item.end.x - item.start.x, item.end.y - item.start.y); ;
            interactibleElements[index].GetComponent<RectTransform>().anchoredPosition = item.start;
            interactibleElements[index].Display(index, GetCurrentDocument().categories[index], item.color);

            index++;
            yield return new WaitForEndOfFrame();
        }


        /*var textureScale = new Vector2(mask_image.sprite.texture.width, mask_image.sprite.texture.height);
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
        yield return new WaitForEndOfFrame();*/

        targetImage.DOColor(Color.white, 0.5f);
        loading_group.SetActive(false);
    }
    #endregion
}
