using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool dblJumpEnabled;
    public bool dashEnabled;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
