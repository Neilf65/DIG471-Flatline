using UnityEngine;

public class TPPosition : MonoBehaviour
{
    public Transform tpPosition;
    Vector3 tpPlacement;
    GameObject playerTransform;

    void Update()
    {
       Vector3 TpTransform =  new Vector3(tpPosition.position.x, tpPosition.position.y, 0);
    }
}
