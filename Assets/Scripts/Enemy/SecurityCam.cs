using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject camReset;
    public float camRotateY;
    [SerializeField] private float camRotateTime = 5f;
    private float zapTime = 0f;
    private Vector3 camRotationOrigin;

    private AudioSource audioSource;
    public AudioClip[] secCamClips;

    void Awake()
    {
        
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
            // Debug.Log(hit.transform.name);

            if (hit.collider.CompareTag("Player"))
                {
                    transform.LookAt(Player.transform.position);
                    zapTime += Time.deltaTime;

                    if (Player != null && zapTime >= 3f)
                    {
                        player.ChangeHealth(-10);
                        Debug.Log("currentHealth:" + player.currentHealth);
                        zapTime = 0f;
                        camRotateTime = 0f;
                        camRotateY = -0.1f;
                        ResetCameraRotation();

                        SoundEffectsOSManager.PlayOSSound(SoundType.ZAP, .8f);
                    }
                }
        }

    }

    private void RotateObject()
    {
        // Rotate the Camera
        transform.Rotate(0, camRotateY, 0f);



        if (camRotateTime <= 0.1f)
        {
            camRotateTime = 5f;
            camRotateY = -camRotateY;
            // SoundEffectsOSManager.PlayOSSound(SoundType.SCAN, .4f);
        }
    }

    public void ResetCameraRotation()
    {
        transform.LookAt(camReset.transform);
    }

    public void CamDead()
    {
        transform.LookAt(Player.transform);
        {
            camRotateTime = 0f;
            camRotateY = 0f;
        }
    }
}


    

