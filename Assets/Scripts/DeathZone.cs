using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Respawn");
            if (other.TryGetComponent<CarEntity>(out CarEntity carEntity))
            {
                carEntity.SpawnImitation();
            }
        }
    }
}
