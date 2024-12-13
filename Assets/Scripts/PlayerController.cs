using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : CharacterBehaviour
{
    public static PlayerController Instance;
    public List<Key_Controller> KeysCollected;
    [SerializeField] private GameObject _failureWindow;
    [SerializeField] private TextMeshProUGUI _healthUIText;
   private void Awake()
{
    Instance = this;
    
}
      private void Update()
    {
        _healthUIText.text = "Health: " + _currentHealth + "/" + _maxHealth; 
    }
     
    public override void Die()
    {
        _isDead = true;
        Debug.Log("Starting player");
        _failureWindow.SetActive(true);
        GetComponent<PlayerMovementBehaviour>().enabled = false;
        GetComponent<PlayerWeaponBehaviour>().enabled = false;
        GetComponentInChildren<CameraController>().enabled = false;
    }
    

}
