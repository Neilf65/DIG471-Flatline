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
            
        }
        if (pausing != true)
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
        uI = playerControls.UI.Pause;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        videoImage.enabled = true;   
        videoPlayer.frame = 0;
        videoPlayer.Play();
        Time.timeScale = 0f;
    }
    
     public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pausing = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
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
        Time.timeScale = 1f;
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




