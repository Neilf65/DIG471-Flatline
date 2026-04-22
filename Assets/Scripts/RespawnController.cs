using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance;
    public Transform respawnPoint;
    public GameObject player;
    public PlayerController playerController;

    private void Awake()
    {
        Instance = this;
        PlayerController playerController = GetComponent<PlayerController>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        CharacterController controller = player.GetComponent<CharacterController>();
        
        if (collision.CompareTag("Player"))
        {
            controller.enabled = false;
            collision.transform.position = respawnPoint.position;
            controller.enabled = true;
            playerController.ChangeHealth(-20);
        }
    }
}
