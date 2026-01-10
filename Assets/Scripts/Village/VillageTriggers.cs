using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTriggers : MonoBehaviour
{
    [SerializeField] private VillageManager villageManager;
    [SerializeField] private bool initialized;
    [SerializeField] private int triggerNumber;

    private void Start()
    {
        initialized = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!initialized) return;
            villageManager.UnlockDestructionBasedOnTrigger(triggerNumber);
            villageManager.BlockAreaBasedOnTriggerNumber(triggerNumber);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            villageManager.LockDestructionBasedOnTrigger(triggerNumber);
            //villageManager.BlockExitAreaBasedOnTriggerNumber(triggerNumber);
        }

    }
}
