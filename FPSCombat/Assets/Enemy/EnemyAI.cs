using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 20f;
    public float fireRate = 1.5f;
    public float bulletDamage = 10f;

    private Transform player;
    private PlayerStats playerStats;
    private float fireTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
        playerStats = playerObject.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            transform.LookAt(player);

            fireTimer += Time.deltaTime;

            if (fireTimer >= fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Enemy shot player!");
                playerStats.TakeDamage(bulletDamage);
            }
        }
    }
}
