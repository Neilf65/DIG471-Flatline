using UnityEngine;

public class CameraOrient : MonoBehaviour
{

    public Transform Target;

    void Update()
    {
        Vector3 relativepos = Target.position + new Vector3(0, 0, 0) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativepos);

        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, 3 * Time.deltaTime);
    }
}
