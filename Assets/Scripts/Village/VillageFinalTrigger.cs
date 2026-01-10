using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageFinalTrigger : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject villageHorror;
    [SerializeField] private GameObject finalMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameManager.EndGame();
            villageHorror.SetActive(false);
            finalMusic.SetActive(true);
        }
    }

}
