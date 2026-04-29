using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections.Generic;


public class HealthGauge : MonoBehaviour
{

    [SerializeField] private PlayerController player;

    // Video Player
    private List<VideoPlayer> videoPlayerList;
    
    // Video Clip
    public List<VideoClip> videoClipList;

    // Raw Image
    public RawImage image;
    public RawImage[] imageArray;

    void Start()
    {
        image.enabled = true;
        videoPlayerList[0].Play();
    }

    void Update()
    {
        StatusChange();
    }

    public void StatusChange()
    {
        if (player.currentHealth >= 67)
        {
            image.texture = imageArray[0].texture;
        }

        if (player.currentHealth <= 67 && player.currentHealth >= 33)
        {
            image.texture = imageArray[1].texture;

        }

        if (player.currentHealth <= 32)
        {
            image.texture = imageArray[2].texture;
        }
    }

}
