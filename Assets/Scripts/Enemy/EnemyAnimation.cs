using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] EnemyMovement eMovement;
    public Animator eAnimator;
    void Start()
    {
        eMovement = GetComponent<EnemyMovement>();
        eAnimator = GetComponent<Animator>();
    }
}
