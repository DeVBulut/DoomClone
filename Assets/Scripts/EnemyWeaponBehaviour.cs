using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private float attackRange = 1.8f; // The range of the sphere overlap
    [SerializeField] private LayerMask detectionLayer; // Layer to detect the player
    [SerializeField] private float hitChance = 70f; // Chance to hit the player (in percentage)
    [SerializeField] private float attackCooldown = 0.8f; // Cooldown time for attacks

    public bool isOnCooldown = false; // Cooldown status
    public Animator animator;
    public GameObject player;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
                // Roll for a chance to hit
                float randomValue = Random.Range(0f, 100f);
                if (randomValue <= hitChance)
                {
                    // Get the PlayerController and call the Hit function
                    PlayerController playerController = player.GetComponent<PlayerController>();
                    if (playerController != null)
                    {
                        Debug.Log("Controller Locked");
                        playerController.Hit();
                        animator.Play("EnemyShoot");
                    }
                    Debug.Log("Enemy Hit You!");
                }
                else
                {
                    Debug.Log("Enemy missed the attack!");
                }

                // Start the cooldown
                StartCoroutine(StartCooldown());
                return;
            }
        }
    }

    private System.Collections.IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the attack range as a wire sphere when the object is selected in the editor
        Gizmos.color = Color.red; // Set the color to red
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
