using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject lookTarget;

    void Start()
    {
        lookTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        lookTarget.transform.position = new Vector3(0, 2f, 4.5f);
    }

    void Update()
    {
        if (lookTarget)
        {
            var direction = lookTarget.transform.position - transform.position;
            direction.x = 0;
            if (direction.y < 60)
            {
                var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
            }
        }
    }
}
