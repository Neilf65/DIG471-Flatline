using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public bool isPaused;
    public bool pausing;
    public bool playstate;
    public void onPaused(InputAction.CallbackContext context)

    {
        if(context.performed)
        {
            Debug.Log("pausing {Context performed}");
            pausing = true;
        }
        else if (context.performed)
        {
            pausing = false;
        }
        return;
    }
    
  
    void Start()
    {
        pauseMenu.SetActive(false);

       
    }
    private void Update()
    {
        StartCoroutine("PauseGame");
        StartCoroutine("ResumeGame");
        if (pausing == false)
        {
            ResumeGame();
        }
        else if (pausing != false)
        {
            PauseGame();
        }
    }


     IEnumerator PauseGame()
    {
        if (pausing==true && playstate!=true){

        
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSeconds(0.5f);
        isPaused = false;
        playstate = false;
        }
    }
    
     IEnumerator ResumeGame()
    {
        if (pausing!=true && playstate!=true){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.5f);
        isPaused = true;  
        playstate = false;
        } 
    }
}


