using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveMentBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;  
    private Rigidbody _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidBody.velocity = transform.forward * moveSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            transform.Rotate(0, 90, 0);
        }
    }
}
