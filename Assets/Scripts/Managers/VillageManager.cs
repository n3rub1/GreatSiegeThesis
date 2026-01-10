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
    [SerializeField] private List<GameObject> triggers;

    private void Awake()
    {
        villageDestruction.SetActive(false);
        villagersDead.SetActive(false);
        villagersAlive.SetActive(true);

    }

    public void UnlockDestructionBasedOnTrigger(int triggerNumber)
    {
        if(triggerNumber == 0 || triggerNumber == 2 || triggerNumber == 4 || triggerNumber == 6 || triggerNumber == 8)
        {
            villageDestruction.SetActive(true);
            villagersDead.SetActive(true);
            villagersAlive.SetActive(false);

        }

    }

    public void LockDestructionBasedOnTrigger(int triggerNumber)
    {
        if (triggerNumber == 1 || triggerNumber == 3 || triggerNumber == 5 || triggerNumber == 7)
        {
            villageDestruction.SetActive(false);
            villagersDead.SetActive(false);
            villagersAlive.SetActive(true);
        }


    }

    public void BlockAreaBasedOnTriggerNumber(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case 0:
                {
                    areaBlockers[0].SetActive(true);
                    triggers[0].SetActive(false);
                    break;
                }
            case 1:
                {
                    areaBlockers[1].SetActive(true);
                    triggers[1].SetActive(false);
                    break;
                }
            case 2:
                {
                    areaBlockers[2].SetActive(true);
                    triggers[2].SetActive(false);
                    break;
                }
            case 3:
                {
                    areaBlockers[3].SetActive(true);
                    triggers[3].SetActive(false);
                    break;
                }
            case 4:
                {
                    areaBlockers[4].SetActive(true);
                    triggers[4].SetActive(false);
                    break;
                }
            case 5:
                {
                    areaBlockers[5].SetActive(true);
                    triggers[5].SetActive(false);
                    break;
                }

            case 6:
                {
                    areaBlockers[6].SetActive(true);
                    triggers[6].SetActive(false);
                    break;
                }
            case 7:
                {
                    areaBlockers[7].SetActive(true);
                    triggers[7].SetActive(false);
                    break;
                }
            case 8:
                {
                    areaBlockers[8].SetActive(true);
                    triggers[8].SetActive(false);
                    break;
                }
        }
    }
}
