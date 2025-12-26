using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class CircleOfDeath : MonoBehaviour
{
    [Header("Flash")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int flashesInLastSecond = 6;

    private DangerZone dangerZone;
    private DangerSpawn dangerSpawn;
    private GameObject particlePrefab;

    private float armDelay;
    private float destroyAfterArm;

    private bool armed;
    private bool playerInside;

    public void Init(DangerZone dangerZone, DangerSpawn dangerSpawn, GameObject particlePrefab, float armDelay, float destroyAfterArm)
    {
        this.dangerZone = dangerZone;
        this.dangerSpawn = dangerSpawn;
        this.particlePrefab = particlePrefab;
        this.armDelay = armDelay;
        this.destroyAfterArm = destroyAfterArm;

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ArmSequence());
    }

    private IEnumerator ArmSequence()
    {
        float preFlash = Mathf.Max(0f, armDelay - 1f);
        if (preFlash > 0f) yield return new WaitForSeconds(preFlash);

        if (spriteRenderer != null)
            yield return FlashLastSecond();

        armed = true;

        if (particlePrefab != null)
            Instantiate(particlePrefab, transform.position, Quaternion.identity);

        if (playerInside)
            dangerSpawn.KillPlayer("Player stayed inside a danger circle until it armed.");

        Destroy(gameObject, destroyAfterArm);
    }

    private IEnumerator FlashLastSecond()
    {
        int toggles = flashesInLastSecond * 2;
        float interval = 1f / toggles;

        spriteRenderer.enabled = true;

        for (int i = 0; i < toggles; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
        }

        spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        playerInside = true;

        if (armed)
            dangerSpawn.KillPlayer("Player entered an already-armed danger circle.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        playerInside = false;
    }
}
