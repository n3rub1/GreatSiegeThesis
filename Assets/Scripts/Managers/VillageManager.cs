using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{

    [Header("Triggers")]
    [SerializeField] private GameObject villageDestruction;
    [SerializeField] private GameObject villagersDead;
    [SerializeField] private GameObject villagersAlive;


    private void Awake()
    {
        villageDestruction.SetActive(false);
        villagersDead.SetActive(false);
        villagersAlive.SetActive(true);

    }

    public void UnlockDestructionBasedOnTrigger()
    {

                villageDestruction.SetActive(true);
                villagersDead.SetActive(true);
                villagersAlive.SetActive(false);

    }

    public void LockDestructionBasedOnTrigger()
    {

                villageDestruction.SetActive(false);
                villagersDead.SetActive(false);
                villagersAlive.SetActive(true);

    }
}
