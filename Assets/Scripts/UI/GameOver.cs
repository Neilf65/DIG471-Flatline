using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private RawImage videoImage;
    [SerializeField] private VideoPlayer videoPlayer;

    public void Awake()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoImage.enabled = true;
        videoPlayer.frame = 0;
        videoPlayer.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        videoImage.enabled = false; // hide video only
    }
}
