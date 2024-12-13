using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBehaviour
{
 void Start()
    {
        _currentHealth = _maxHealth;
    }
 public override void Hit()
{
    _currentHealth -= 1;
    if (_currentHealth <= 0)
    {
        Die();
    }
}

public override void Die()
{
    // Make the enemy fall to its side
    transform.Rotate(-75f, 0, 0);
    GetComponent<Rigidbody>().isKinematic = true; // Stop further physics interactions

    // Disable the enemy's movement and weapon behaviors
    GetComponent<EnemyMoveMentBehaviour>().enabled = false;
    GetComponent<EnemyWeaponBehaviour>().enabled = false;

    Debug.Log("Enemy has fallen and is now disabled");
}



    // Update is called once per frame
}
