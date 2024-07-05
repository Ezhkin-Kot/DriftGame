using UnityEngine;

public class Trail : MonoBehaviour
{
    public bool IsGrounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            IsGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            IsGrounded = false;
        }
    }
}
