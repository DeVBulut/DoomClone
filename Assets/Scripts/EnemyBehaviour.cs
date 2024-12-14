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
    Debug.Log("Enemy has fallen and is now disabled");
    Destroy(this.gameObject);
}



    // Update is called once per frame
}
