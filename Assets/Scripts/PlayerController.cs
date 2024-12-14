using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 100)] protected int _maxHealth = 100; // Set default max health to 3
    [SerializeField, Range(0, 100)] protected int _maxArmor = 100;
    public int _currentHealth;
    public int _currentArmor;
    public bool _isDead = false;
    public TextMeshProUGUI healthText; 
    public TextMeshProUGUI armorText; 
    public bool IsDead()
    {
        return _isDead;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        _currentArmor = _maxArmor;
    }
    void Update()
    {
        healthText.text = _currentHealth.ToString();
        armorText.text = _currentArmor.ToString();
    }
    public void Hit()
    {
        Debug.Log("Hit - ");
        if (_isDead == true) return;
        _currentHealth -= 10;
        _currentArmor -= 10;
        Debug.Log("Character hit!");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        _isDead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerWeaponBehaviour>().enabled = false;
    }
    

}
