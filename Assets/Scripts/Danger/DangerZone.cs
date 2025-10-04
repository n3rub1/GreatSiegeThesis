using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] GameManager gameManager;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering is tagged "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered a danger zone!");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left the danger zone.");
        }
    }
}
