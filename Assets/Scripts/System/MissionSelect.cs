using UnityEngine;

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
    }

    public void TerminalClose()
    {
        missionSelect.SetActive(false);
        Time.timeScale = 1f;
    }
}
