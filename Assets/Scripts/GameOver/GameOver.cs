using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public int fortScene = 2;

    public void BackToFort()
    {
        SceneManager.LoadScene(fortScene);
    }
}
