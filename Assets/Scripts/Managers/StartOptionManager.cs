using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOptionManager : MonoBehaviour
{
    public static StartOptionManager Instance;

    public bool isStartIDN;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void setIsStartIDN(bool setIsIDN)
    {
        isStartIDN = setIsIDN;
    }

    public bool getIsStartIDN()
    {
        return isStartIDN;
    }

}
