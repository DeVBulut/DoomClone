using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBehaviour
{
    Animator animator;
    void Start()
    {
        _currentHealth = _maxHealth;
        animator = GetComponent<Animator>();
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
        animator.Play("EnemyDie");
        StartCoroutine(StartDeathSequence());
    }

    private System.Collections.IEnumerator StartDeathSequence()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }


    // Update is called once per frame
}
