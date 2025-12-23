using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOverrideManager : Singleton<AIOverrideManager>
{
    [SerializeField] UIManager uiManager;
    [SerializeField] DialogManager dialogManager;
    private bool AIVoiceActing;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AIVoiceActing = true;
    }

    public bool GetAIVoiceActing()
    {
        return AIVoiceActing;
    }

    public void InvertAIVoiceActing()
    {
        AIVoiceActing = !AIVoiceActing;
        uiManager.ChangeAIVoiceOverText(AIVoiceActing);

        if (!AIVoiceActing) dialogManager.StopPlayClip();
    }


}
