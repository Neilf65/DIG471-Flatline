using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
      SceneManager.LoadScene(1);  
    }

    // Update is called once per frame
    public void Quit()
    {
       Application.Quit(); 
    }
}
