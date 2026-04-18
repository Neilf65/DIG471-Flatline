using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
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
        videoPlayer.loopPointReached += OnVideoEnd;


    }
    private void Update()
    {
        
    }


     public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        videoImage.enabled = true;   
        videoPlayer.frame = 0;
        videoPlayer.Play();
        

    }
    
     public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        videoImage.enabled = false; // hide video only
    }
}




