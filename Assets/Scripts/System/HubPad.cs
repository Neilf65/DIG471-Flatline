using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPad : MonoBehaviour
{
    [SerializeField] PlayerController Player;

    void Start()
    {
        PlayerController Player = GetComponent<PlayerController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main Hub Blockout");
        }
    }
}
