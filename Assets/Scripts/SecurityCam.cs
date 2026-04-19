using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    GameObject Camera;
    [SerializeField] private float camRotateY;
    [SerializeField] private float camRotateTime = 5f;
    bool camRotSwap;
    Transform camRotation;

    void Update()
    {
        camRotateTime -= Time.deltaTime;
        var direction = transform.forward;
        RaycastHit hit;
        Vector3 RayDirection = new Vector3(0, 0, 0);

        if (Physics.Raycast(transform.position, direction, out hit, 50f))
        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            print("Found an object - distance: " + hit.distance);
            Debug.Log(hit.transform.name);

            if (player != null)
            {
                player.ChangeHealth(-10);
                Debug.Log("currentHealth:" + player.currentHealth);
            }
        }

        RotateObject();
    }

    private void RotateObject()
    {
        // Rotate the Camera
        Vector3 camRotation = new Vector3(transform.rotation.x, 0, 0); 
        transform.Rotate(0, camRotateY, 0f);



        if (camRotateTime <= 0.1f)
        {
            camRotateTime = 5f;
            camRotateY = -camRotateY;
        }
    }
}
