using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] GameManager gameManager;
    [SerializeField] DangerSpawn dangerSpawn;

    private bool canDie;
    private bool inDangerZone = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dangerSpawn = FindObjectOfType<DangerSpawn>();
    }

    private void Update()
    {
        CheckToDie();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDangerZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inDangerZone = false;
        }
    }

    private void CheckToDie()
    {
        canDie = dangerSpawn.GetCanDie();
        if (inDangerZone && canDie)
        {
            gameManager.SetQuestAccepted("Dead");
            dangerSpawn.StartDeadPanelSequence();
        }
    }
}
