using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : Interactions
{
    Animator anim;

    private void OnEnable()
    {
        action += OpenChest;
    }
    private void OnDisable()
    {
        action -= OpenChest;
    }
    private void OpenChest()
    {
        Debug.Log("Open Chest");
    }

}
