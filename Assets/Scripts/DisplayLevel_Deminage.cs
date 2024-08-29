using System.Collections;
using UnityEngine;

public class DisplayLevel_Deminage : DisplayLevel
{
    public static DisplayLevel_Deminage Instance;

    public  bool canPressButton = false;

    int catsCount = 0;

    public DeminageButton[] buttons;

    public Category zoneCategory;


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
    }

    public void PressZone (int i) {
        /*if ( i >= GetCurrentDocument().elements.Count) {
            Debug.Log($"ERROR : more zones than categories on db");
            return;
        }
        canPressButton = true;
        zoneCategory = GetCurrentDocument().elements[i];
        Debug.Log($"pressed zone : {zoneCategory.name}");
        StartCoroutine(ShowButtonsCoroutine());*/
    }

    public void PressCatButton(int i) {
       /* if (!canPressButton) return;
        // continue
        canPressButton = false;

        foreach (var item in buttons) {
            item.FadeOut();
        }
        var pressedCat = GetCurrentDocument().elements[i];
        Debug.Log($"Match {pressedCat.name} / {zoneCategory.name}");

        if ( pressedCat.name == zoneCategory.name) {
            ++catsCount;
            MissionDisplay.instance.DisplayGoodFeedback();
            interactibleElements[i].Lock();
            buttons[i].Lock();
            if (catsCount == GetCurrentDocument().elements.Count) {
                ++correctAnswers;
                // finish
                targetImage.DOColor(Color.clear, 0.5f);
                foreach (var item in interactibleElements) {
                    item.gameObject.SetActive(false);
                }
                Invoke("NextDocument", 1f);
                return;
            } else {

            }
        } else {
            MissionDisplay.instance.DisplayBadFeedback();
            foreach (var zone in interactibleElements) {
                zone.over = false;
            }

        }*/

       

       
            
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
        yield return new WaitForSeconds(0.01f);

        /* categories = GetCurrentDocument().elements;
        for (int i = 0; i < categories.Count; i++) {
            
            buttons[i].Display(categories[i].name);
            buttons[i].index = i;
            yield return new WaitForSeconds(0.01f);
        }*/
    }

}
