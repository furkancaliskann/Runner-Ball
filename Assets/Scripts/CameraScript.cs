using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10.125f;
    public Vector3 locationOffset;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void ResetVariables()
    {
        transform.position = new Vector3(target.position.x - 5, target.position.y + 5f, target.position.z - 5);
    }
}
