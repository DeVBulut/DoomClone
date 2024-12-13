using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Controller : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        PlayerController.Instance.KeysCollected.Add(this);
        gameObject.SetActive(false);
        OpenDoor();
    }
    private void OpenDoor()
    {
        gameObject.SetActive(false);
    }
}
