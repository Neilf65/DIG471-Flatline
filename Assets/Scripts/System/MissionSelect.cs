using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSelect : MonoBehaviour
{
    [SerializeField] private GameObject missionSelect;
    [SerializeField] private PlayerController player;
    private PlayerControls playerControls;
    public InteractObj interactObj;
    float radius = 6f;
    public LayerMask Player;

    void Start()
    {
        missionSelect.SetActive(false);
    }

    void Update()
    {
        if (Physics.CheckSphere(transform.position, radius, Player))
        {
            if (interactObj)
            {
                Console();
            }
        }
    }

    public void Console()
    {
        if (interactObj.isInteracting == true && Physics.CheckSphere(transform.position, radius, Player))
        {
            TerminalOpen();
            interactObj.isInteracting = false;
        }

    }

    public void TerminalOpen()
    {
        missionSelect.SetActive(true);
        Time.timeScale = 0.01f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void TerminalClose()
    {
        missionSelect.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SecondLevel()
    {
        SceneManager.LoadScene("Level 2 Blockout");
    }

    public void FinalLevel()
    {
        SceneManager.LoadScene("Level 3 Blockout");
    }
}
