using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
    }

    private void FaceTarget()
    {
        if (player == null) return;

        // Get the direction to the target
        Vector3 directionToTarget = player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Get the direction to the target
        Vector3 directionToTarget = player.transform.position - transform.position;

        // Set Gizmo color for the direction vector
        Gizmos.color = Color.red;

        // Draw the direction vector as a line
        Gizmos.DrawLine(transform.position, (transform.position + directionToTarget));

        // Draw a sphere at the target's position for visualization
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(player.transform.position, 0.05f);
    }
}
