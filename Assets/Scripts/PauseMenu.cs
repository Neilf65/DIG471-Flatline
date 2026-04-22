using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        

    }
    
     public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void QuitGame()
    {
        Application.Quit();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }
    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        videoImage.enabled = false; // hide video only
    }
}




