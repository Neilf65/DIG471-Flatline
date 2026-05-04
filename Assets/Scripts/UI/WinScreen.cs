using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Terminal terminal;
    [SerializeField] private GameObject endSequence;

    float timer;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    void Awake()
    {
        Terminal terminal = GetComponent<Terminal>();
    }

    public void Update()
    {
        if (terminal.freeze != false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                endSequence.SetActive(true);
            }
        }
    }

}


