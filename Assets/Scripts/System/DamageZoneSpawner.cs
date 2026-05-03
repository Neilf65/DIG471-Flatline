    using UnityEngine;

public class DamageZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flameSpot;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private GameObject Player;

    [SerializeField] float spawnTimer;
    public float RangeNegX, RangePosX;
    public float RangeNegZ, RangePosZ;

    void Start()
    {
        
        // Spawn timer
        spawnTimer = 5f;
    }

    void Update()
    {
        if (spawnTimer <= 0)
        {
            SpawnFlame();
            spawnTimer = 1f;
            Debug.Log("spawn timer is reset");
        }
    }

    private void SpawnFlame()
    {
        Vector3 spawnVec = new Vector3(transform.position.x + Random.Range( -RangeNegX, RangePosX), transform.position.y, transform.position.z + Random.Range(-RangeNegZ, RangePosZ));    
        Instantiate(flameSpot, spawnVec, Quaternion.identity);
        flameSpot.GetComponent<DamageZone>().playerController = PlayerController;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}
