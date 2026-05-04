using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class Terminal : MonoBehaviour
{
    // GameObject
    [SerializeField] GameObject terminalUI;
    [SerializeField] GameObject winScreen;
    private PlayerControls playerControls;
    private InputAction uI;

    // Reference to player script
    public InteractObj interactObj;

    // QTE variables
    public Slider mainSlider;
    public TextMeshProUGUI keyToPress;
    public bool freeze;
  

    KeyCode key;
    KeyCode[] availableOptions =  {KeyCode.R, KeyCode.JoystickButton1};


    // range to access terminal when near
    
    float radius = 6f;
    // layer to determine which object can access
    
    public LayerMask Player;
    // Timer for UI minigame
    
    private float timer = 0f;

    void Start()
    {
        timer = timer += Time.unscaledDeltaTime;

        int rand = Random.Range(0, 2);
        key = availableOptions[rand];
        keyToPress.text = availableOptions[rand].ToString();

        mainSlider.value = 10;
    }
    
    void Awake()
    {
        terminalUI.SetActive(false);
        playerControls = new PlayerControls();
    }


    void Update()
    {
        if (!freeze)
        {
            mainSlider.value = Mathf.MoveTowards(mainSlider.value, 0, Time.unscaledDeltaTime);
        }

        if (freeze)
        {
            timer += Time.unscaledDeltaTime;

            if (timer > 3f)
            {
                TerminalClose();
                timer = 0f;
            }
        }

        if (Input.GetKeyDown(key) && mainSlider.value > 0)
        {
            mainSlider.value += 1;
            if (mainSlider.value == 10)
            {
                keyToPress.text = "System Override!";
                freeze = true;
                winScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (mainSlider.value == 0)
        {
            keyToPress.text = "Access Denied!";
            freeze = true;
        }


        if (Physics.CheckSphere(transform.position, radius, Player))
        {
            if (interactObj)
            {
                Console();
            }
        }
    }

    private void OnEnable()
    {
        uI.Enable();
    }

    private void OnDisable()
    {
        uI.Disable();
    }

    public void Console()
    {
        if (interactObj.isInteracting == true && Physics.CheckSphere(transform.position, radius, Player))
        {
            TerminalOpen();
        }

    }

    public void TerminalOpen()
    {
        terminalUI.SetActive(true);
        Time.timeScale = 0.0f;
        int rand = Random.Range(0, 2);
        key = availableOptions[rand];
        keyToPress.text = availableOptions[rand].ToString();
        mainSlider.value = 4;
        freeze = false;

        // play terminal SFX
    }

    public void TerminalClose()
    {
        terminalUI.SetActive(false);
        Time.timeScale = 1f;
        freeze = true;
    }

    
}
