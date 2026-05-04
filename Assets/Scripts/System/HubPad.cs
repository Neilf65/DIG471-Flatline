using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPad : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main Hub");
        }
    }
}
