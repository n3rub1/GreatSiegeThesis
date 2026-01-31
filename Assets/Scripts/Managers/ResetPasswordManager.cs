using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPasswordManager : MonoBehaviour
{

    private void Awake()
    {
        StartOptionManager.Instance.ResetAll();
    }

}
