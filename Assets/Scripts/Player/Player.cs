using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    //returns the stats variable since it is private
    public PlayerStats Stats => stats;

}
