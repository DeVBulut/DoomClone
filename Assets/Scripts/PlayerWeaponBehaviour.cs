using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeaponBehaviour : WeaponBehaviour
{
    public Animator animator; 
    public int AmmoCount = 200;
    public TextMeshProUGUI ammoText; 
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && AmmoCount > 0)
        {
            animator.SetTrigger("Shoot");
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                FireWeapon(hit);
                AmmoCount -= 1;
                ammoText.text = AmmoCount.ToString();
            }
        }
    }
  
 
}
