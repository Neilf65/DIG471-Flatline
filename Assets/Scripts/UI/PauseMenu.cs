using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Fields
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private RawImage videoImage;
    private PlayerControls playerControls;
    private InputAction uI;
    
    [SerializeField] private InputActionAsset playerMovement;

    // Booleans
    public bool isPaused;
    public bool pausing;
    public bool playstate;
    public void onPaused(InputAction.CallbackContext context)

    {
        pausing = !pausing;
        if(pausing)
        {
            PauseGame();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (pausing != true)
        {
            ResumeGame();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
        // playerControls = new PlayerControls();
        videoPlayer.loopPointReached += OnVideoEnd;
        AudioListener audioListener = GetComponent<AudioListener>();


    }

     public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        videoImage.enabled = true;   
        videoPlayer.frame = 0;
        videoPlayer.Play();


        //Pause game audio

    }
    
     public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        SoundEffectsOSManager.PlayOSSound(SoundType.PRESS);
    }
    public void QuitGame()
    {
        Application.Quit();

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausing = false;
        AudioListener.pause = false;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        videoImage.enabled = false; // hide video only
    }
}




