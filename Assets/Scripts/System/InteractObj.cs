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
            isInteracting = !isInteracting;
        }
        
    }   

}
