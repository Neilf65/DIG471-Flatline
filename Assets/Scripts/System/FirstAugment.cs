using UnityEngine;

public class FirstAugment : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX, rotationSpeedY;

    void Update()
    {
        transform.Rotate(rotationSpeedX, rotationSpeedY, 0f);
    }
    public void OnCollisionEnter(Collision other)
    {
        PlayerController Player = other.gameObject.GetComponent<PlayerController>();

        if (Player != null)
        {
            Player.dashEnabled = true;
            Destroy(gameObject);
        }
    }
}
