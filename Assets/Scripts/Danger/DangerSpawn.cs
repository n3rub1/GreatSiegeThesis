using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DangerSpawn : MonoBehaviour
{
    [System.Serializable]
    public struct RectArea
    {
        public Vector2 min;
        public Vector2 max;

        public Vector2 Min
        {
            get { return new Vector2(Mathf.Min(min.x, max.x), Mathf.Min(min.y, max.y)); }
        }

        public Vector2 Max
        {
            get { return new Vector2(Mathf.Max(min.x, max.x), Mathf.Max(min.y, max.y)); }
        }

        public Vector2 RandomPoint()
        {
            Vector2 a = Min;
            Vector2 b = Max;
            return new Vector2(Random.Range(a.x, b.x), Random.Range(a.y, b.y));
        }

        public bool Contains(Vector2 p)
        {
            Vector2 a = Min;
            Vector2 b = Max;
            return p.x >= a.x && p.x <= b.x && p.y >= a.y && p.y <= b.y;
        }
    }

    [System.Serializable]
    public class Region
    {
        public string name;
        public RectArea spawnArea; // big allowed rectangle
        public List<RectArea> safeAreas = new List<RectArea>(); // holes: do NOT spawn here

        public int additionalDifficulty = 0;

        public float waveMin = 5f;
        public float waveMax = 10f;

        public float armDelay = 5f;
        public float destroyAfterArm = 1f;

        public int maxAttemptsPerCircle = 30;

        public bool IsInSafe(Vector2 p)
        {
            for (int i = 0; i < safeAreas.Count; i++)
            {
                if (safeAreas[i].Contains(p)) return true;
            }
            return false;
        }

        public bool TryPickPoint(out Vector2 p)
        {
            for (int attempt = 0; attempt < maxAttemptsPerCircle; attempt++)
            {
                Vector2 candidate = spawnArea.RandomPoint();
                if (!IsInSafe(candidate))
                {
                    p = candidate;
                    return true;
                }
            }

            p = Vector2.zero;
            return false;
        }
    }

    [Header("Prefab / shared VFX")]
    [SerializeField] private GameObject circlePrefab;   // DangerCircle prefab
    [SerializeField] private GameObject particlePrefab;

    [Header("Systems")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DangerZone dangerZone;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;

    [Header("Death UI")]
    [SerializeField] private GameObject deadScreenPanel;
    [SerializeField] private float deadScreenTimer = 5f;

    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Vector3 fortRespawnPosition = new Vector3(-236, -29, 0);
    [SerializeField] private Vector3 ottomanRespawnPosition = new Vector3(-134, -198, 0);


    [Header("Two regions")]
    [SerializeField] private Region regionA = new Region();
    [SerializeField] private Region regionB = new Region();

    private bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(RegionLoop(regionA));
        StartCoroutine(RegionLoop(regionB));
        isSpawning = true;
    }

    private IEnumerator RegionLoop(Region region)
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave(region));

            float wait = Random.Range(region.waveMin, region.waveMax);
            yield return new WaitForSeconds(wait);
        }
    }

    private IEnumerator SpawnWave(Region region)
    {
        int day = gameManager != null ? gameManager.GetDayNumber() : 0;
        int count = day + region.additionalDifficulty;

        for (int i = 0; i < count; i++)
        {
            Vector2 pos;
            if (!region.TryPickPoint(out pos))
                yield break;

            GameObject circleGO = Instantiate(circlePrefab, pos, Quaternion.identity);

            CircleOfDeath circle = circleGO.GetComponent<CircleOfDeath>();
            if (circle == null)
            {
                Debug.LogError("DangerSpawn: circlePrefab missing CircleOfDeath.");
                yield break;
            }

            circle.Init(
                dangerZone: dangerZone,
                dangerSpawn: this,
                particlePrefab: particlePrefab,
                armDelay: region.armDelay,
                destroyAfterArm: region.destroyAfterArm
            );

            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        }
    }

    public void KillPlayer(string context)
    {
        if (dangerZone != null && !dangerZone.TryLockDeath()) return;

        // Optional logging (remove if you don’t need it)
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Player Died (Danger Circles)", context);
        GoogleSheetLogger.I.Log("Player Died (Danger Circles)", context);

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        playerMovement.DisableMovement();
        deadScreenPanel.SetActive(true);
        gameManager.SetQuestAccepted("Dead");

        if(gameManager.GetCurrentLocation() == "Fort St. Elmo")
        {
            player.position = fortRespawnPosition;
        }
        else
        {
            player.position = ottomanRespawnPosition;
        }

        dayNightCycleManager.SetTimerManually(22);
        dayNightCycleManager.SetIsHit(true);
        yield return new WaitForSeconds(deadScreenTimer);

        deadScreenPanel.SetActive(false);
        dayNightCycleManager.SetIsHit(false);
        playerMovement.EnableMovement();

        if (dangerZone != null)
            dangerZone.UnlockDeath();
    }

    public void StopAllSpawns()
    {
        if (isSpawning)
        {
            StopAllCoroutines();
            isSpawning = false;
        }
    }

    public void RestartAllSpawns()
    {
        if (!isSpawning)
        {
            StartCoroutine(RegionLoop(regionA));
            StartCoroutine(RegionLoop(regionB));
            isSpawning = true;
        }
    }
}
