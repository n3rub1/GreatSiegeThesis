using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatClueManager : Singleton<CatClueManager>
{
    private int catClueNumber = 0;

    public int getCatClueNumber()
    {
        return catClueNumber;
    }

    public void setCatClueNumber()
    {
        catClueNumber++;
    }
}
