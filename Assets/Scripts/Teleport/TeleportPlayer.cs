using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 teleportLocations;
    private Vector3 playerPosition;

    private void Start()
    {
        playerPosition = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = teleportLocations;
        }
    }

}
