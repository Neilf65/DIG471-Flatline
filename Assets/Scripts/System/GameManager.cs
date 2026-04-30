using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool dblJumpEnabled;
    public bool dashEnabled;

    [SerializeField] GameObject TimelineOne;
    [SerializeField] GameObject TimelineTwo;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
       TimeSwap(); 
    }

    private void TimeSwap()
    {
        if (FindFirstObjectByType<PlayerController>().timelineDif){
        TimelineOne.SetActive(false);
        TimelineTwo.SetActive(true);
        }

        if (FindFirstObjectByType<PlayerController>().timelineDif == false)
        {
        TimelineTwo.SetActive(false);
        TimelineOne.SetActive(true);
        }
    }
}