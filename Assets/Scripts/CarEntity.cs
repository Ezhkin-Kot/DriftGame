using UnityEngine;

public class CarEntity : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float startYrotation;
    [SerializeField] private float startYoffset;
    [SerializeField] private ParticleSystem particleSys;

    private CarMovementController controller;

    private int carStuckFrames;

    private void Awake()
    {
        controller = GetComponent<CarMovementController>();

        SpawnImitation();
        Physics.SyncTransforms();
    }

    public void SpawnImitation()
    {
        controller.ResetCarMovement();
        transform.eulerAngles = new Vector3(0f, spawnPoint.transform.eulerAngles.y + startYrotation, 0f);
        transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + startYoffset, spawnPoint.position.z);

        carStuckFrames = 0;

        particleSys.transform.position = spawnPoint.transform.position;
        particleSys.Play();
    }

    private void FixedUpdate()
    {
        if ((transform.localEulerAngles.x < 5f || transform.localEulerAngles.x > 355f) && (transform.localEulerAngles.z < 5f || transform.localEulerAngles.z > 355f))
        {
            carStuckFrames = 0;
            return;
        }
        else
        {
            carStuckFrames++;
        }
        if (carStuckFrames > 125)
        {
            SpawnImitation();
        }
    }
}
