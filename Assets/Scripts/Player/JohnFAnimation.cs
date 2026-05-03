using UnityEngine;
using UnityEngine.InputSystem;

public class JohnFAnimation : MonoBehaviour
{
    [SerializeField] PlayerController player;
    public Animator jAnimator;
    private PlayerControls playerControls;
    private InputAction actions;

    void Start()
    {

        player = GetComponent<PlayerController>();
        jAnimator = GetComponent<Animator>();
    }

}
