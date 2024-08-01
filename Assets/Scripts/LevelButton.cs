using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class LevelButton : MonoBehaviour, IPointerClickHandler {
    public GameObject locked_group;
    public GameObject idle_group;

    public Level.Type type;

    public TextMeshProUGUI title_text;

    public TextMeshProUGUI lockedScore_text;
    public TextMeshProUGUI score_text;

    public Level level;

    public DisplayScore displayScore;

    public void OnPointerClick(PointerEventData eventData) {
        Tween.Bounce(transform);
        LevelManager.Instance.currentLevel = level;
        SelectionMenu.Instance.FadeOut();
        MissionIntroDisplay.Instance.FadeIn();
        MissionIntroDisplay.Instance.description_text.text = $"{level.description}";
        MissionIntroDisplay.Instance.UpdateUI();

    }

    private void Start() {


        level = LevelManager.Instance.levels.Find(x => x.type == type);
        title_text.text = level.name;

        locked_group.SetActive(false);
        displayScore.UpdateScore(Random.Range(0, 4));
    }



}
