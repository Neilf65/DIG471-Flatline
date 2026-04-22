using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class HealthGauge : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    // Video Player
    [SerializeField] private VideoPlayer videoPlayer;
    // Video Clip
    [SerializeField] private VideoClip fineVid;
    [SerializeField] private VideoClip okayVid;
    [SerializeField] private VideoClip hurtVid;

    // Raw Image
    [SerializeField] private RawImage fineImage;
    [SerializeField] private RawImage okayImage;
    [SerializeField] private RawImage hurtImage;

    VideoPlayer[] videoArray = new VideoPlayer[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerController player = GetComponent<PlayerController>();
        fineImage.enabled = true;
        videoPlayer.frame = 0;
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VideoType()
    {
        if (player.currentHealth >= 67)
        {
            videoPlayer.Stop();
            videoPlayer.clip = fineVid;
            videoPlayer.Play();
        }
        
        if (player.currentHealth <= 66 && player.currentHealth >= 34)
        {
            videoPlayer.Stop();
            videoPlayer.clip = okayVid;
            videoPlayer.Play();
        }

        if (player.currentHealth <= 33)
        {
            videoPlayer.Stop();
            videoPlayer.clip = hurtVid;
            videoPlayer.Play();
        }
    }
}
