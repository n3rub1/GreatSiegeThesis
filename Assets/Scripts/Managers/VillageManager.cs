using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{

    [Header("Triggers")]
    [SerializeField] private GameObject villageDestruction;
    [SerializeField] private GameObject villagersDead;
    [SerializeField] private GameObject villagersAlive;
    [SerializeField] private List<GameObject> areaBlockers;


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

    public void BlockExitAreaBasedOnTriggerNumber(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case 1: areaBlockers[0].SetActive(true); break;
            case 2: areaBlockers[2].SetActive(true); break;
            case 3: areaBlockers[4].SetActive(true); break;
            case 4: areaBlockers[6].SetActive(true); break;
            case 5: areaBlockers[8].SetActive(true); break;
            case 6: areaBlockers[10].SetActive(true); break;
        }
    }

    public void BlockEntryAreaBasedOnTriggerNumber(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case 2: areaBlockers[1].SetActive(true); break;
            case 3: areaBlockers[3].SetActive(true); break;
            case 4: areaBlockers[5].SetActive(true); break;
            case 5: areaBlockers[7].SetActive(true); break;
            case 6: areaBlockers[9].SetActive(true); break;
        }
    }
}
