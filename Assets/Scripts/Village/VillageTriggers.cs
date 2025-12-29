using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTriggers : MonoBehaviour
{
    [SerializeField] private VillageManager villageManager;
    [SerializeField] private bool initialized;

    private void Start()
    {
        initialized = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!initialized) return;
        villageManager.UnlockDestructionBasedOnTrigger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        villageManager.LockDestructionBasedOnTrigger();
    }
}
