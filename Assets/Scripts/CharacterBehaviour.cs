using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField, Range(0, 10)] protected int _maxHealth = 3; // Set default max health to 3
    protected int _currentHealth;
    public bool _isDead = false;
    public bool IsDead()
    {
        return _isDead;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
    }
    public virtual void Die()
    {
        _isDead = true;
        Debug.Log("Character died."); 
    }
    public virtual void Hit()
    {
        if (_isDead == true) return;
        _currentHealth -= 1;
        Debug.Log("Character hit!");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
}
