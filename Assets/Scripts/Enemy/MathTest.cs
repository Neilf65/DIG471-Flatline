using UnityEngine;

public class MathTest : MonoBehaviour
{

    public Transform target;


    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        Debug.DrawLine(targetDir, transform.position);

        if (angle < 35.0f)
        {
            print("Close");
        }
    }
}
