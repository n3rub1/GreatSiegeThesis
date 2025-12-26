using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject questGiver;
    [SerializeField] private Vector3 teleportLocations;
    private Vector3 playerPosition;
    private Vector3 questGiverPosition;

    private void Start()
    {
        playerPosition = player.transform.position;
        //questGiverPosition = questGiver.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = teleportLocations;
        }
    }

    public void TeleportPlayerManually(Vector3 newTeleportLocation)
    {
        player.transform.position = newTeleportLocation;
    }

    public Vector3 GetCurrentLocation()
    {
        return player.transform.position;
    }

    public void TeleportAndRevert(Vector3 originalPosition, Vector3 newTeleportLocation, bool firstTime)
    {
        
        if (firstTime)
        {
            player.transform.position = newTeleportLocation;
        }
        else
        {
            player.transform.position = originalPosition;
        }

    }

    public void TeleportQuestGiver(Vector3 questGiverLocation)
    {
        questGiver.transform.localPosition = questGiverLocation;
    }

}
