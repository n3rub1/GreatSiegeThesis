//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerStamina : MonoBehaviour
//{
//    [Header ("Config")]
//    [SerializeField] private PlayerStats stats;

//    public void UseStamina(float amount)
//    {
//        if(stats.Stamina >= amount)
//        {
//            stats.Stamina = Mathf.Max(stats.Stamina = stats.Stamina - amount, 0f);
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.M))
//        {
//            UseStamina(1f);
//        }
//    }
//}
