using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private Vector3 offset;
    [Range(0.25f, 1f)] [SerializeField] private float smoothForce;
    private Vector3 velocity;

    private void Awake()
    {
        velocity = Vector3.zero;
        offset = transform.position - targetTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = targetTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothForce);
    }
}
