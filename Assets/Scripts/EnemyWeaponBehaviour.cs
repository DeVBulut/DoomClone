using UnityEngine;
public class EnemyWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.8f; // The range of the sphere overlap
    [SerializeField] private LayerMask detectionLayer; // Layer to detect the player
    [SerializeField] private float hitChance = 70f; // Chance to hit the player (in percentage)
    [SerializeField] private float attackCooldown = 0.8f; // Cooldown time for attacks

        public bool isOnCooldown = false; // Cooldown status
    public GameObject lockedTarget = null; // The locked player target

    private void Update()
    {
        if (!isOnCooldown)
        {
            TryDetectAndAttackPlayer();
        }
    }

    private void TryDetectAndAttackPlayer()
    {
        // Cast a sphere overlap to detect objects within the range
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, detectionLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // Lock onto the player
                lockedTarget = hit.transform.parent.gameObject;

                // Roll for a chance to hit
                float randomValue = Random.Range(0f, 100f);
                if (randomValue <= hitChance)
                {
                    // Get the PlayerController and call the Hit function
                    PlayerController playerController = lockedTarget.GetComponent<PlayerController>();
                    if(playerController != null) {Debug.Log("Controller Locked"); playerController.Hit();}
                    Debug.Log("Enemy Hit You!");
                }
                else
                {
                    Debug.Log("Enemy missed the attack!");
                }

                // Start the cooldown
                StartCoroutine(StartCooldown());
                return; // Only attack one player at a time
            }
        }
    }

    private System.Collections.IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
        lockedTarget = null; // Reset target after cooldown
    }
}
