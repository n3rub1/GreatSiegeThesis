using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuestSelector : MonoBehaviour
{

    [SerializeField] List<GameObject> lootBoxes;
    [SerializeField] List<GameObject> catClues;
    [SerializeField] List<GameObject> debris;

    private int maxNumberOfLootBoxes = 16;
    private int maxNumberOfCatClues = 6;
    private int maxNumberOfDebris = 15;



    public void SpawnLootBoxes()
    {
        int[] whichSpawn = RandomNumbers(maxNumberOfLootBoxes);

        foreach (int index in whichSpawn)
        {
            if (index >= 0 && index < lootBoxes.Count)
            {
                lootBoxes[index].SetActive(true);
            }
        }

    }

    public void SpawnCatClues()
    {
        int[] whichSpawn = RandomNumbers(maxNumberOfCatClues);

        foreach (int index in whichSpawn)
        {
            Debug.Log(index);
            if (index >= 0 && index < catClues.Count)
            {
                Debug.Log("Setting active");
                catClues[index].SetActive(true);
            }
        }
    }

    public void SpawnDebris()
    {
        int[] whichSpawn = RandomNumbers(maxNumberOfDebris);

        foreach (int index in whichSpawn)
        {

            if (index >= 0 && index < debris.Count)
            {
                debris[index].SetActive(true);
            }
        }
    }

    private int[] RandomNumbers(int amountOfNumbers)
    {
        List<int> generation = new List<int>();

        for (int i = 0; i < amountOfNumbers; i++)
        {
            int random = Random.Range(0, 100);

            if (random < 50)
            {
                generation.Add(i);
                Debug.Log("ok");
            }
            else
            {
                Debug.Log("less");
            }
        }

        return generation.ToArray();
    }


}
