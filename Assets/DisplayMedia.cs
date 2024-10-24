using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;
using TMPro;

public class DisplayMedia : Displayable
{
    public static DisplayMedia Instance;

    public delegate void OnDownloadFinish();
    public OnDownloadFinish onMediaDownloaded;

    public void Reset() {
        video_group.SetActive(false);
        image_group.SetActive(false);
        text_group.SetActive(false);
        audio_group.SetActive(false);
        FadeOut();
    }


    /// <summary>
    /// IMAGE
    /// </summary>
    public Image image;
    public GameObject image_group;
    public ScrollRect image_ScrollView;

    /// <summary>
    /// TEXT
    /// </summary>
    public GameObject text_group;
    public TextMeshProUGUI text_UiText;

    /// <summary>
    /// AUDIO
    /// </summary>
    public GameObject audio_group;
    public AudioSource audio_source;

    /// <summary>
    /// VIDEO
    /// </summary>
    public GameObject video_group;
    public VideoPlayer video_player;

    /// <summary>
    /// ZOOM
    /// </summary>
    public Transform slide_Target;
    Vector2 initscale;
    bool canZoom = false;

    /// <summary>
    /// INTERACTIBLE ELEMENTS
    /// </summary>
    public List<ColorCode> colorCodes = new List<ColorCode>();
    [System.Serializable]
    public struct ColorCode {
        public string name;
        public string hexa;
        public int index;
        public Color color;
    }

    public Transform interactibleElement_Parent;
    public InteractibleElement interactibleElement_Prefab;
    public List<InteractibleElement> interactibleElements = new List<InteractibleElement>();
    float lerp = 0.1f;
    public GameObject closable_group;
    private float currentZoom = 0.5f;
    public float targetZoom = 0.5f;
    public float zoomStep = 0.2f;

    [System.Serializable]
    public class PixelGroup {
        public Vector2 start;
        public Vector2 end;
        public Color color;
        public string hexa;
    }

    public List<PixelGroup> pixelGroups = new List<PixelGroup>();

    private void Awake() {
        Instance = this;
    }

    public override void Start() {
        //base.Start();
    }

    private void Update() {
        currentZoom = targetZoom;
        if ( canZoom)
            slide_Target.localScale = Vector2.Lerp(Vector2.one * 0.5f, Vector2.one * 1.5f, currentZoom);
    }

    public void LoadMedia(string type, string url, bool closable) {

        FadeIn();

        targetZoom = 0.5f;

        slide_Target.localScale = Vector3.one;
        canZoom = false;
        
        closable_group.SetActive(closable);
        Debug.Log($"Document Type : {type}");
        type = type.ToLower();

        if (type != "text")
            DisplayLoading.Instance.FadeIn();

        foreach (var item in interactibleElements)
            item.gameObject.SetActive(false);


        video_group.SetActive(false);
        text_group.SetActive(false);
        image_group.SetActive(false);
        audio_group.SetActive(false);

        switch (type) {
            case "image":
                LoadImage(url);
                break;
            case "video":
                LoadVideo(url);
                break;
            case "audio":
                LoadAudio(url);
                break;
            case "text":
                ShowText(url);
                break;
            default:
                break;
        }
    }

    public void ZoomIn() {
        targetZoom -= zoomStep;
        currentZoom = Mathf.Clamp01(currentZoom);
    }

    public void ZoomOut() {
        targetZoom += zoomStep;
        currentZoom = Mathf.Clamp01(currentZoom);
    }

    void EnableZoom() {
        canZoom = true;

    }

    #region video
    void LoadVideo(string url) {
        Debug.Log("download video");
        video_player.url = url;
        StartCoroutine(DownloadVideoCoroutine());
    }

    IEnumerator DownloadVideoCoroutine() {
        video_player.GetComponent<RawImage>().enabled = false;
        video_group.SetActive(true);
        video_player.Prepare();

        while (!video_player.isPrepared)
            yield return new WaitForEndOfFrame();

        video_player.Play();
        video_player.GetComponent<RawImage>().enabled = true;
        Finish_Download();

    }
    #endregion

    #region image
    void LoadImage(string url) {

        image_group.SetActive(true);
        image.color = Color.clear;
        // check if video / audio / other
        StartCoroutine(DownLoadImage(url));
    }

    IEnumerator DownLoadImage(string url) {

        bool loadMask = MissionDisplay.instance.currentLevel.level.type == Level.Type.FakeInfo;
        DisplayLoading.Instance.FadeIn();

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        www.SendWebRequest();

        while (!www.isDone)
            yield return new WaitForEndOfFrame();

        Debug.Log($"Finished Downloading Image");

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            DisplayMessage.Instance.Display($"Erreur en cherchant l'image\n{url}:\n{www.error}");
        } else {
            var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            if (myTexture == null) {
                Debug.LogError($"pas de texture bug");
            }
            image.sprite = Sprite.Create(myTexture, new Rect(Vector2.zero, new Vector2(myTexture.width, myTexture.height)), Vector2.zero);

            var ar = image.GetComponent<AspectRatioFitter>();
            ar.aspectRatio = (float)myTexture.width / myTexture.height;

        }

        /////////////

        
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url)) {
            Debug.Log($"sending request");


            yield return request.SendWebRequest();
            yield return new WaitForEndOfFrame();

           ;
        }

        image.SetNativeSize();
        if (loadMask) {
            yield return DownloadMask(MissionDisplay.instance.currentLevel.GetCurrentDocument().medias[1]);
        } else {
            image.DOColor(Color.white, 0.5f);
            Finish_Download();
            EnableZoom();
        }



    }
    #endregion

    public Image maskdebug;

    #region mask
    IEnumerator DownloadMask(string url) {

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        www.SendWebRequest();

        while (!www.isDone) {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log($"finished loading");


        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
            DisplayMessage.Instance.Display($"Erreur en cherchant l'image\n{url}:\n{www.error}");
        } else {
            var mask_texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            mask_texture.filterMode = FilterMode.Point;
            maskdebug.sprite = Sprite.Create(mask_texture, new Rect(Vector2.zero,new Vector2(mask_texture.width, mask_texture.height)), Vector2.zero);

            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, image.sprite.texture.height * image.rectTransform.rect.width / image.sprite.texture.width);

            Vector2 originalResolution = new Vector2(mask_texture.width, mask_texture.height);

            //Vector2 colorPixelPos = new Vector2(50, 60);
            //Vector2 actualPixelPos = new Vector2(colorPixelPos.x * scaledResolution.x / originalResolution.x, colorPixelPos.y * scaledResolution.y / originalResolution.y);

            yield return new WaitForEndOfFrame();
            yield return CreateInteractibleElements(mask_texture);
        }

        image.DOColor(Color.white, 0.5f);
        Finish_Download();
        EnableZoom();
    }
    #endregion

    #region interactible elements
    IEnumerator CreateInteractibleElements(Texture2D maskTexture) {

        // show loading

        // clear all
        pixelGroups.Clear();
        image.SetNativeSize();
        interactibleElement_Parent.localScale = Vector3.one;

        yield return new WaitForEndOfFrame();


        int loadLimit = 0;

        for (int x = 0; x < maskTexture.width; x++) {
            for (int y = 0; y < maskTexture.height; y++) {

                // get pixel color of mask image

                int h = y;

                Vector2 originalResolution = new Vector2(maskTexture.width, maskTexture.height);
                Vector2 scaledResolution = new Vector2(maskdebug.rectTransform.rect.width, maskdebug.rectTransform.rect.height);
                Vector2 colorPixelPos = new Vector2(x,h);
                // Vector2 actualPixelPos = new Vector2(colorPixelPos.x * scaledResolution.x / originalResolution.x, colorPixelPos.y * scaledResolution.y / originalResolution.y);
                Vector2 actualPixelPos = new Vector2(x, h);
                Color color = maskTexture.GetPixel((int)actualPixelPos.x, (int)actualPixelPos.y);
                if (color.a < 0.1f)
                    continue;

                // find matching pixel group
                var hexa = UnityEngine.ColorUtility.ToHtmlStringRGB(color);
                var pixelGroup = pixelGroups.Find(x => x.hexa == hexa);
                if (pixelGroup == null) {
                    // create if none
                    pixelGroup = new PixelGroup();
                    pixelGroup.start = actualPixelPos;
                    pixelGroup.color = color;
                    string colorCode = hexa;
                    pixelGroup.hexa = colorCode;
                    pixelGroups.Add(pixelGroup);
                }

                // set scale
                if (pixelGroup.end.x < actualPixelPos.x)
                    pixelGroup.end.x = actualPixelPos.x;
                if (pixelGroup.end.y < actualPixelPos.y)
                    pixelGroup.end.y = actualPixelPos.y;
            }

            /*++loadLimit;
            if (loadLimit%30 == 0) {
                yield return new WaitForEndOfFrame();
                DisplayLoading.Instance.Push(0.8f+ (float)loadLimit / 3000f);
            }*/


        }

        // hide all
        foreach (var item in interactibleElements)
            item.gameObject.SetActive(false);

        // create & place all elements
        int index = 0;
        foreach (var pixelGroup in pixelGroups) {

            if (index >= MissionDisplay.instance.currentLevel.GetCurrentDocument().interactibleElements.Count)
                break;

            // instantiate 
            if (index >= interactibleElements.Count) {
                Debug.Log($"New Interactible Element");

                InteractibleElement dz = Instantiate(interactibleElement_Prefab, interactibleElement_Parent);
                interactibleElements.Add(dz);
            }

            Vector2 pos = pixelGroup.start;

            interactibleElements[index].gameObject.SetActive(true);
            interactibleElements[index].GetComponent<RectTransform>().sizeDelta = new Vector2(pixelGroup.end.x - pixelGroup.start.x, pixelGroup.end.y - pixelGroup.start.y); ;
            interactibleElements[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(pixelGroup.start.x, -(maskTexture.height- pixelGroup.start.y) +(pixelGroup.end.y - pixelGroup.start.y));

            var colorCode = colorCodes.Find(x => x.hexa == pixelGroup.hexa);

            if ( string.IsNullOrEmpty( colorCode.name) ) {
                DisplayMessage.Instance.Display($"Data Base error : La couleur {pixelGroup.hexa} ne fait pas parti de la liste");
            }

            var doc = MissionDisplay.instance.currentLevel.GetCurrentDocument();

            var colorIndex = doc.colorNames.FindIndex(x => x == colorCode.name);

            string text = doc.interactibleElements[colorIndex];
            interactibleElements[index].Display(index, text, pixelGroup.color);

            index++;

            yield return new WaitForEndOfFrame();
        }

        float prevScale = image.rectTransform.rect.width;

        float rap = image.rectTransform.rect.width / maskTexture.width;
        interactibleElement_Parent.localScale = Vector3.one * rap;
    }
    #endregion

    #region audio
    public void LoadAudio(string url) {
        audio_group.SetActive(true);
        DownloadAudioClip(url);
    }
    IEnumerator DownloadAudioClip(string url) {

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS)) {

            www.SendWebRequest();

            while (!www.isDone) {
                yield return new WaitForEndOfFrame();
            }

            if (www.result == UnityWebRequest.Result.ConnectionError) {
                Debug.Log(www.error);
            } else {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                audio_source.clip = myClip;

            }

            audio_source.Play();
            Finish_Download();
        }
    }
    #endregion

    #region text
    public void ShowText(string text) {
        text_group.SetActive(true);
        text_UiText.text = text;
    }
    #endregion

    public void Finish_Download() {
        image_ScrollView.horizontalNormalizedPosition = 0f;
        image_ScrollView.verticalNormalizedPosition = 1f;
        DisplayLoading.Instance.FadeOut();
        if (onMediaDownloaded != null)
            onMediaDownloaded();
    }
}
