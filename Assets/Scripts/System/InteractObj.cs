using UnityEngine;
using UnityEngine.InputSystem;

public class InteractObj : MonoBehaviour
{
    public PlayerControls playerControls;
    private InputAction _interface;
    private PlayerController playerController;
    public bool isInteracting;

    void Awake()
    {
        playerControls = new PlayerControls();
        isInteracting = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        { 
            isInteracting = !isInteracting;
            
        }
        
    }   

}
