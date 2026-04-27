using UnityEngine;

public class DamageZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flameSpot;
    [SerializeField] private PlayerController playerController;
    private bool isSpawning;
    [SerializeField] float spawnTimer;
    private Vector3 spawnVec;
    private float lifeTime;

    void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        
        // Spawn timer
        isSpawning = false;
        spawnTimer = 5f;
        lifeTime = 20f;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
        SpawnFlame();
        spawnTimer = 1f;
        Debug.Log("Spawn timer is now 0");
        }
        lifeTime -= Time.deltaTime;
    }

    private void SpawnFlame()
    {
        Vector3 spawnVec = new Vector3(transform.position.x + Random.Range( 20, -20), transform.position.y, transform.position.z + Random.Range(20, -20));
        Instantiate(flameSpot, spawnVec, Quaternion.identity);
        Debug.Log("Spawning now");
    }
}
