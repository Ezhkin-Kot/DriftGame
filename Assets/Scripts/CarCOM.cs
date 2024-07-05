using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCOM : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        rb.centerOfMass = new Vector3(0f, -0.18f, 0f);
    }
    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(rb.worldCenterOfMass, .2f);
        }
    }
}
