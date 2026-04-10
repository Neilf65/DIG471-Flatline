using UnityEngine;

public class BatteryCollectible : MonoBehaviour
{
    // floats
    [SerializeField] private float rotationSpeedX = 0.5f;
    [SerializeField] private float rotationSpeedY = 0.5f;
    [SerializeField] private float slowMultiplier = 1f;
    [SerializeField] private float length; 
    // reference to player
    PlayerController _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate Battery
        transform.Rotate(rotationSpeedX, rotationSpeedY, 0f);

        // Bounce the item up and down
        float PositionPingPongY = (Mathf.PingPong(Time.time, length) - length / 2) / slowMultiplier;
        transform.Translate(new Vector3(0, PositionPingPongY, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        //Collect Battery
    }
}
