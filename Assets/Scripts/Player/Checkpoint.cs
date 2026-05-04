using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   public BoxCollider trigger;


   private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {   
            RespawnController.Instance.respawnPoint = transform;

            trigger.enabled = false;
        }
    }
}
