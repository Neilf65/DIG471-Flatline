using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public PlayerController playerController;
    private Transform spawnPoint;
    private float lifeTime;


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        lifeTime = 7f;
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
