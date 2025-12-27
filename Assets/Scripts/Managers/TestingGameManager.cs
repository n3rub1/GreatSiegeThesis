using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGameManager : MonoBehaviour
{

    public bool isGameOverTesting = false;
    public bool isResourceTesting = false;

    public bool GetIsGameOverTesting()
    {
        return isGameOverTesting;
    }

    public bool GetIsResourceTesting()
    {
        return isResourceTesting;
    }

}
