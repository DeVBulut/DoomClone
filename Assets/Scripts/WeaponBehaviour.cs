using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{    
    public void FireWeapon(RaycastHit hit)
    {
        Debug.Log("Gun Shot");
        //GetComponent<AudioSource>().Play();
        if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
        {
            EnemyBehaviour enemyBehaviour = hit.transform.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null && !enemyBehaviour.IsDead()) 
            {
                Debug.Log("Enemy Hit");
                enemyBehaviour.Hit();
            }
        }
    }


}
