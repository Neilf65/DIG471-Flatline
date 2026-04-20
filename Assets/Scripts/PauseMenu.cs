using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private RawImage videoImage;
    private PlayerControls playerControls;
    private InputAction uI;
    
    [SerializeField] private InputActionAsset playerMovement;

    public bool isPaused;
    public bool pausing;
    public bool playstate;
    public void onPaused(InputAction.CallbackContext context)

    {
        pausing = !pausing;
        if(pausing == true)
        {
            PauseGame();
        }
        if (pausing == false)
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
        videoPlayer.loopPointReached += OnVideoEnd;


    }

     public void PauseGame()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        // videoImage.enabled = true;   
        // videoPlayer.frame = 0;
        // videoPlayer.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
     public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    public void Settings()
    {
        settingsMenu.SetActive(true);

    }
    public void Quit()
    {
        Application.Quit();
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        videoImage.enabled = false; // hide video only
    }
}




