using UnityEngine;
using System.Collections;

public class SetToGround : MonoBehaviour
{
    public bool RotateToGround;

    void Start()
    {
        InvokeRepeating("SetGround", Random.value, 1.0f);
    }

    void Update()
    {
    }

    void SetGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastQueue.get().Queue(ray, 2, (hit, b) =>
        {
            if (b)
            {
                transform.position = hit.point;
                if (RotateToGround)
                {
                    transform.up = hit.normal;
                }
            }
        },
        ~LayerMask.NameToLayer("Terrain"));
    }
}
