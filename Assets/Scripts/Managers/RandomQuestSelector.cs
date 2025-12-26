using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuestSelector : MonoBehaviour
{

    [SerializeField] List<GameObject> lootBoxes = new List<GameObject>();
    [SerializeField] List<GameObject> catClues = new List<GameObject>();
    [SerializeField] List<GameObject> debris = new List<GameObject>();

    [SerializeField] private int spawnLootPercentage;
    [SerializeField] private int spawnCatPercentage;
    [SerializeField] private int spawnDebrisPercentage;

    private int maxNumberOfLootBoxes;
    private int maxNumberOfCatClues;
    private int maxNumberOfDebris;



    public void SpawnLootBoxes()
    {
        maxNumberOfLootBoxes = lootBoxes.Count;
        int[] whichSpawn = RandomNumbers(maxNumberOfLootBoxes, spawnLootPercentage);
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
        maxNumberOfCatClues = catClues.Count;
        int[] whichSpawn = RandomNumbers(maxNumberOfCatClues, spawnCatPercentage);
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
        maxNumberOfDebris = debris.Count;
        int[] whichSpawn = RandomNumbers(maxNumberOfDebris, spawnDebrisPercentage);
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
