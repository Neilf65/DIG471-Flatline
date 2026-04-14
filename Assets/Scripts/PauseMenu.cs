using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private PlayerControls playerControls;
    private InputAction uI;
    
    [SerializeField] private InputActionAsset playerMovement;

    public bool isPaused;
    public bool pausing;
    public bool playstate;
    public void onPaused(InputAction.CallbackContext context)

    {
        pausing = !pausing;
        if(pausing)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    
    private void OnEnable()
    {
        uI = playerControls.UI.Pause;
        uI.Enable();
    }

    private void OnDisable()
    {
        uI.Disable(); 
    }
  
    void Awake()
    {
        pauseMenu.SetActive(false);
        playerControls = new PlayerControls();

       
    }
    private void Update()
    {
        
    }


     public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    
     public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
}


