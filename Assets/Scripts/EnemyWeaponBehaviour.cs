using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    public float detectionRadius = 10f; // Radius to detect the player
    public float shootingCooldown = 2f; // Cooldown time for shooting
    public float bulletSpeed = 20f; // Bullet speed
    public Transform bulletSpawnPoint; // Where the bullets are spawned from
    public GameObject bulletPrefab; // Bullet prefab to instantiate
    public float missChance = 0.6f; // 60% chance to miss the shot
    public Transform player; // Reference to the player

    private float lastShootTime = 0f; // Time when the enemy last shot
    private bool playerInRange = false; // To track if the player is in range
    private bool playerVisible = false; // Whether the player is locked in or not

    void Update()
    {
        DetectPlayer();
        
        if (playerInRange && playerVisible && Time.time - lastShootTime >= shootingCooldown)
        {
            ShootAtPlayer();
        }
    }

    void DetectPlayer()
    {
        // Detect the player within the radius using OverlapSphere
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        playerInRange = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                playerInRange = true;
                player = hitCollider.transform; // Set the player reference
                LockOnPlayer();
                return;
            }
        }

        playerInRange = false; // No player detected
        playerVisible = false; // Reset visibility
    }

    void LockOnPlayer()
    {
        // Lock onto the player only if they are in range and within line of sight
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerVisible = true;
            }
            else
            {
                playerVisible = false;
            }
        }
        else
        {
            playerVisible = false;
        }
    }

    void ShootAtPlayer()
    {
        lastShootTime = Time.time;

        // Check if the shot misses with a 60% chance
        bool isMissed = Random.value < missChance;
        if (isMissed)
        {
            Debug.Log("Shot missed!");
            return; // Shot missed, no bullet is fired
        }

        // If the shot hits, fire the bullet
        FireBullet();
    }

    void FireBullet()
    {
        // Create the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Get direction to the player
        Vector3 direction = (player.position - bulletSpawnPoint.position).normalized;

        // Add velocity to the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * bulletSpeed;
        }

        Debug.Log("Bullet fired at player.");
    }

    // Draw the detection radius in the Scene view (for debugging purposes)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
