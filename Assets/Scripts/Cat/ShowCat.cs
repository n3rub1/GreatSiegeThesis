using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCat : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private PercentageManager percentageManager;
    [SerializeField] private GameObject catObject;

    void Update()
    {
        if(gameManager.GetDayNumber() >= 10)
        {
            Debug.Log(percentageManager.GetPercentages()[0]);
            Debug.Log(percentageManager.GetPercentages()[1]);
            Debug.Log(percentageManager.GetPercentages()[2]);
            Debug.Log(percentageManager.GetPercentages()[3]);

            if (percentageManager.GetPercentages()[3] > 50)
            {
                Debug.Log(percentageManager.GetPercentages()[0]);
                Debug.Log(percentageManager.GetPercentages()[1]);
                Debug.Log(percentageManager.GetPercentages()[2]);
                Debug.Log(percentageManager.GetPercentages()[3]);
                catObject.SetActive(true);
            }
            else
            {
                catObject.SetActive(false);
            }

        }       
    }
}
