using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int dayNumber = 1;
    public NPCDialog[] npcDialog;

    private void Awake()
    {
        foreach (NPCDialog dialog in npcDialog)
        {
            dialog.ResetData();
        }
    }

    public void updateDay()
    {
        dayNumber += 1;
    }
}
