using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class menuSwap : MonoBehaviour
{
    private float swapTimer = 7f;
    private bool swapNow;
    bool swapDiff;
    [SerializeField] GameObject tlOne;
    [SerializeField] GameObject tlTwo;

    public RawImage image;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private RawImage videoImage;
    void Start()
    {
        swapDiff = true;
    }

    void Awake()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {

    swapTimer -= Time.deltaTime;
    if (swapTimer <= 0f)
    {
        swapNow = true;
        TimeSwap();
        videoImage.enabled = true;   
        videoPlayer.frame = 0;
        videoPlayer.Play();
        swapDiff = !swapDiff;
        swapTimer = 7f;
        Debug.Log("swapping now");
    }

  }

    public void TimeSwap()
    {
        if (swapDiff && swapNow){
        tlOne.SetActive(false);
        tlTwo.SetActive(true);
        swapNow = false;
        }

        if (swapDiff != true && swapNow)
        {
        tlTwo.SetActive(false);
        tlOne.SetActive(true);
        swapNow = false;
        }
    }
  void OnVideoEnd(VideoPlayer vp)
    {
        vp.Stop();
        videoImage.enabled = false; // hide video only
    }
}
