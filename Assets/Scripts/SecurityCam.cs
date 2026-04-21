using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float camRotateY;
    [SerializeField] private float camRotateTime = 5f;
    private float zapTime = 0f;
    private Vector3 camRotationOrigin;

    void Awake()
    {
        camRotationOrigin = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
    void Update()
    {
        RotateObject();
        camRotateTime -= Time.deltaTime;
        var direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, 10f))
        {
            PlayerController player = Player.GetComponent<PlayerController>();
            // print("Found an object - distance: " + hit.distance);
            Debug.Log(hit.transform.name);

            if (hit.collider.CompareTag("Player"))
                {
                    transform.LookAt(Player.transform.position);
                    zapTime += Time.deltaTime;

                    if (player != null && zapTime >= 5f)
                    {
                        player.ChangeHealth(-10);
                        Debug.Log("currentHealth:" + player.currentHealth);
                        zapTime = 0f;
                        transform.LookAt(camRotationOrigin);
                        camRotateTime = 0f;
                        camRotateY = -0.1f;
                    }

                    else if (player == null)
                    {
                        RotateObject();
                        zapTime = 0f;
                        transform.LookAt(camRotationOrigin);
                        camRotateTime = 0f;
                        camRotateY = -0.1f;
                    }
                }
        }

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


    

