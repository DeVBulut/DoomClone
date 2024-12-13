using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementSpeed, _rotationSpeed;
    [SerializeField] GameObject _wallObject;
    [SerializeField] private float _speed;
    private Rigidbody _rigidBody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            movementDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.A))
            movementDirection += Vector3.left;
        if (Input.GetKey(KeyCode.S))
            movementDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            movementDirection += Vector3.right;
        movementDirection = movementDirection.normalized;

        Vector3 velocity = (movementDirection.z * transform.forward + movementDirection.x * transform.right) * _speed;

        _rigidBody.velocity = velocity;

    }
 private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EndPoint")
        {
            Debug.Log("Hurray, you made it to the end!");
        }
    }
    private void Update()
    {
        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Mouse X");
        rotation.x = Input.GetAxis("Mouse Y");
        transform.Rotate(rotation * Time.deltaTime * _rotationSpeed);

    }
}
