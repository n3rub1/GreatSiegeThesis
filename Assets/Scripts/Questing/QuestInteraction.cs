using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteraction : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestUI.Instance.questInteraction= this;
            QuestUI.Instance.ChangeQuestMarker();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestUI.Instance.questInteraction = null;
            QuestUI.Instance.CloseQuestPanel();
        }
    }
}
