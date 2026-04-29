using UnityEngine;

public class DamageZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flameSpot;
    [SerializeField] private PlayerController PlayerController;
    private bool isSpawning;
    [SerializeField] float spawnTimer;
    private Vector3 spawnVec;
    private float timer;
    public float RangeNegX, RangePosX;
    public float RangeNegZ, RangePosZ;

    void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        
        // Spawn timer
        isSpawning = false;
        spawnTimer = 5f;
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
    }

    private void SpawnFlame()
    {
        if (spawnTimer <= 0){
        Vector3 spawnVec = new Vector3(transform.position.x + Random.Range( -RangeNegX, RangePosX), transform.position.y, transform.position.z + Random.Range(-RangeNegZ, RangePosZ));    
        Instantiate(flameSpot, spawnVec, Quaternion.identity);
        flameSpot.GetComponent<DamageZone>().playerController = PlayerController;
        Debug.Log("Spawning now");
        }
    }

    void OnTriggerStay(Collider other)
    {   PlayerController Player = GetComponent<PlayerController>();
        if (Player != null)
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
