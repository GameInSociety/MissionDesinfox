using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenu : Displayable
{
    public static SelectionMenu Instance;

    private void Awake() {
        Instance = this;
    }

    public override void Start() {
        base.Start();
    }


}
