using UnityEngine;
using UnityEngine.InputSystem;

public class InteractObj : MonoBehaviour
{
    public bool isInteracting;

    void Awake()
    {
        isInteracting = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        { 
            isInteracting = !isInteracting;
        }
        if (context.canceled)
        {
            isInteracting = false;
        }
        
    } 

    void Update()
    {
        Vector3 rayDir = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        var direction = transform.forward;
        Physics.Raycast(transform.position + Vector3.forward * 1f, rayDir, LayerMask.GetMask("Interactable"));
    }  

}
