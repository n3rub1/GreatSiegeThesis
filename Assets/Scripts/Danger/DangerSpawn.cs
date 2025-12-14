using UnityEngine;
using System.Collections;

public class DangerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject deadScreenPanel;
    [SerializeField] private GameObject player;
    [SerializeField] private GoogleSheetLogger logger;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int additionalDifficulty;

    private bool canDie = false;


    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnCircleOfDeath();
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnCircleOfDeath()
    {
        //for (int i = 0; i <= gameManager.GetDayNumber() + additionalDifficulty; i++){

        //    float randomX = Random.Range(-35f, 15f);
        //    float randomY = Random.Range(-17f, 32f);
        //    Vector2 randomPosition = new Vector2(randomX, randomY);

        //    // Spawn the red circle immediately
        //    GameObject circleInstance = Instantiate(circlePrefab, randomPosition, Quaternion.identity);

        //    // Start coroutine to spawn particle after 3 seconds
        //    StartCoroutine(SpawnParticleAndDestroy(circleInstance, randomPosition));
        //}
        StartCoroutine(SpawnCirclesOnDeathWithDelay());
    }

    public bool GetCanDie()
    {
        return canDie;
    }

    public void StartDeadPanelSequence()
    {
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Player Died (DangerArea Manager)", "Player Enetered a danger zone and did not move in time");
        GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Player Died (DangerArea Manager)", "Player Entered a danger zone and did not move in time - they died");
        StartCoroutine(IsDead());
    }

    IEnumerator SpawnParticleAndDestroy(GameObject circleInstance, Vector2 position)
    {
        canDie = false;
        // Wait 3 seconds before spawning particle
        yield return new WaitForSeconds(3f);

        // Spawn particle effect at the same position
        canDie = true;
        GameObject particleInstance = Instantiate(particlePrefab, position, Quaternion.identity);

        // Destroy the red circle
        if (circleInstance != null)
            Destroy(circleInstance, 1f);
    }

    IEnumerator IsDead()
    {
        deadScreenPanel.SetActive(true);
        player.transform.position = new Vector3(-236, -29, 0);
        yield return new WaitForSeconds(3f);
        deadScreenPanel.SetActive(false);
    }

    IEnumerator SpawnCirclesOnDeathWithDelay()
    {
        {
            int numberOfCircles = gameManager.GetDayNumber() + additionalDifficulty;

            for (int i = 0; i <= numberOfCircles; i++)
            {
                float randomX = Random.Range(-35f, 15f);
                float randomY = Random.Range(-17f, 32f);
                Vector2 randomPosition = new Vector2(randomX, randomY);

                // Spawn the red circle
                GameObject circleInstance = Instantiate(circlePrefab, randomPosition, Quaternion.identity);

                // Start coroutine to spawn particle after 3 seconds
                StartCoroutine(SpawnParticleAndDestroy(circleInstance, randomPosition));

                // Wait a short random delay before spawning the next circle
                float delayBetweenCircles = Random.Range(0.5f, 1f);
                yield return new WaitForSeconds(delayBetweenCircles);
            }
        }
    }
}
