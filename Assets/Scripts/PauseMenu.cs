using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.UI.Pause.performed += OnPausePressed;
    }

    void OnDisable()
    {
        controls.UI.Pause.performed -= OnPausePressed;
        controls.Disable();
    }

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void OnPausePressed(InputAction.CallbackContext ctx)
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // FIXED
        isPaused = false;    // FIXED
    }
}


