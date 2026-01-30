using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> AllPages;

    private int index = 0;

    public void NextPage()
    {
        if(index < (AllPages.Count - 1))
        {
            AllPages[index].SetActive(false);
            AllPages[index + 1].SetActive(true);

            index++;
        }

    }

    public void PreviousPage()
    {
        if (index > 0)
        {
            AllPages[index].SetActive(false);
            AllPages[index - 1].SetActive(true);

            index--;
        }
    }
}
