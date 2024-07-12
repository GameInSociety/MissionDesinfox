using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionIntroDisplay : Displayable
{
    public static MissionIntroDisplay Instance;

    public TextMeshProUGUI title_text;
    public TextMeshProUGUI description_text;
    public DisplayScore displayScore;

    private void Awake() {
        Instance = this;
    }

    public void StartMission() {
        MissionIntroDisplay.Instance.FadeOut();
        MissionDisplay.instance.DisplayMission();
    }
}
