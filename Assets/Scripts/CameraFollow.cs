using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private Vector3 offset;
    [Range(0.25f, 1f)] [SerializeField] private float smoothForce;
    private Vector3 velocity;
    //private Vector3 rotationVelocity;

    private void Awake()
    {
        velocity = Vector3.zero;
        //rotationVelocity = Vector3.zero;
        offset = transform.position - targetTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = targetTransform.position + offset;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, targetTransform.localEulerAngles.y, transform.localEulerAngles.z);
        //transform.localEulerAngles = Vector3.SmoothDamp(transform.localEulerAngles, targetAngle, ref rotationVelocity, smoothForce);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothForce);
    }
}
