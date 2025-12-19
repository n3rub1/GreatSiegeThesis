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
        int[] whichSpawn = RandomNumbers(maxNumberOfLootBoxes, 30);
        DespawnLootBoxes();
        foreach (int index in whichSpawn)
        {
            if (index >= 0 && index < lootBoxes.Count)
            {
                lootBoxes[index].SetActive(true);
            }
        }

    }

    public void DespawnLootBoxes()
    {
        foreach (GameObject lootBox in lootBoxes)
        {
            lootBox.SetActive(false);
        }
    }

    public void SpawnCatClues()
    {
        int[] whichSpawn = RandomNumbers(maxNumberOfCatClues, 50);
        DespawnCatClues();

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

    public void DespawnCatClues()
    {
        foreach (GameObject catClue in catClues)
        {
            catClue.SetActive(false);
        }
    }

    public void SpawnDebris()
    {
        int[] whichSpawn = RandomNumbers(maxNumberOfDebris, 50);
        DespawnSpawnDebris();

        foreach (int index in whichSpawn)
        {

            if (index >= 0 && index < debris.Count)
            {
                debris[index].SetActive(true);
            }
        }
    }

    public void DespawnSpawnDebris()
    {
        foreach (GameObject oneDebris in debris)
        {
            oneDebris.SetActive(false);
        }
    }

    private int[] RandomNumbers(int amountOfNumbers, int chanceToSpawn)
    {
        List<int> generation = new List<int>();
        int minimumFirstTime = 100;

        for (int i = 0; i < amountOfNumbers; i++)
        {
            int random = Random.Range(minimumFirstTime, 100);
            minimumFirstTime = 0;

            if (random < chanceToSpawn)
            {
                generation.Add(i);
            }
        }

        return generation.ToArray();
    }


}
