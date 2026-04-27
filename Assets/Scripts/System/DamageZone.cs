using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public PlayerController playerController;
    private Transform spawnPoint;
    private float lifeTime;
    private float timer;


    void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        lifeTime = 7f;
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.ChangeHealth(-10);
        }
    }

    
}
