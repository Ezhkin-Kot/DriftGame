using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public enum DriveTrainType
{
    FWD, 
    RWD, 
}
public class CarMovementController : MonoBehaviour
{
    [SerializeField] private UIController _UIController;
    [SerializeField] DriveTrainType drivetrainType;
    [SerializeField] private Rigidbody rb;

    [SerializeField] WheelCollider wheelFL;
    [SerializeField] WheelCollider wheelFR;
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;

    private float rbVelocityLimit = 15f;
    [Range(300f, 800f)] [SerializeField] private float accelerationStraight = 400f;

    private float currentYrotation;
    private float targetYrotation;

    public bool IsActive;

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private TrailRenderer trailRendererRR;
    [SerializeField] private TrailRenderer trailRendererRL;

    private Trail trailRR;
    private Trail trailRL;

    private float currentScore;
    public float CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            _UIController.UpdateScoreView((int)value);
            RaceLoop.Instance.Score = (int)value;
        }
    }
    [SerializeField] private float driftScoreFactor;

    private float horizontalForce;
    private float verticalForce;

    private static bool HandBrakeState;
    private static bool MenuWindowState = false;


    private void Awake()
    {
        trailRR = trailRendererRR.GetComponent<Trail>();
        trailRL = trailRendererRL.GetComponent<Trail>();

        wheelFL.transform.localEulerAngles =Vector3.zero;
        wheelFR.transform.localEulerAngles =Vector3.zero;
    }

    public void UpdateInputAxis(float horizontal, float vertical)
    {
        horizontalForce = horizontal;
        verticalForce = vertical;
    }

    private void FixedUpdate()
    {
        if (!IsActive)
        {
            PerformBrake(1f);
            trailRendererRR.emitting = false;
            trailRendererRL.emitting = false;
            return;
        }

        float currentVelocity = rb.velocity.magnitude;

        targetYrotation = horizontalForce * curve.Evaluate(currentVelocity);

        RotateWheels(targetYrotation);

        wheelFL.steerAngle = targetYrotation;
        wheelFR.steerAngle = targetYrotation;

        currentYrotation = targetYrotation;

        if (verticalForce > 0)
        {
            ResetBrakes();

            if (drivetrainType == DriveTrainType.FWD)
            {
                wheelFL.motorTorque = verticalForce * accelerationStraight * (2.8f - currentVelocity / rbVelocityLimit);
                wheelFR.motorTorque = verticalForce * accelerationStraight * (2.8f - currentVelocity / rbVelocityLimit);

                wheelRL.motorTorque = verticalForce * accelerationStraight * (2.2f - currentVelocity / rbVelocityLimit);
                wheelRR.motorTorque = verticalForce * accelerationStraight * (2.2f - currentVelocity / rbVelocityLimit);
            }
            else if (drivetrainType == DriveTrainType.RWD)
            {
                wheelFL.motorTorque = verticalForce * accelerationStraight * (2.2f - currentVelocity / rbVelocityLimit);
                wheelFR.motorTorque = verticalForce * accelerationStraight * (2.2f - currentVelocity / rbVelocityLimit);

                wheelRL.motorTorque = verticalForce * accelerationStraight * (2.8f - currentVelocity / rbVelocityLimit);
                wheelRR.motorTorque = verticalForce * accelerationStraight * (2.8f - currentVelocity / rbVelocityLimit);
            }
        }
        else
        {
            PerformBrake(0.05f);
        }

        float moveDirection = Vector3.Dot(transform.forward, rb.velocity);

        if (moveDirection < -0.5f && verticalForce > 0)
        {
            PerformBrake(Mathf.Abs(verticalForce));
        }
        else if (moveDirection > 0.5f && verticalForce < 0)
        {
            PerformBrake(Mathf.Abs(verticalForce));
        }
        else if (verticalForce < 0)
        {
            PerformMoveBackwards(verticalForce);
        }

        if (Input.GetKey(KeyCode.Space) || HandBrakeState)
        {
            HandBrake();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !MenuWindowState)
        {
            MenuWindowState = true;
            _UIController.GameOverShow(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && MenuWindowState)
        {
            MenuWindowState = false;
            _UIController.GameOverHide();
        }

        HandledDriftData();
}

    private void HandledDriftData()
    {
        float driftDirection = Vector3.Dot(transform.right, rb.velocity);
        int driftValue = Mathf.Abs((int)driftDirection);

        if (driftValue > 3 && trailRR.IsGrounded && trailRL.IsGrounded)
        {
            trailRendererRR.emitting = true;
            trailRendererRL.emitting = true;

            CurrentScore += Time.deltaTime * driftScoreFactor;
        }
        else 
        {
            trailRendererRR.emitting = false;
            trailRendererRL.emitting = false;
        }
    }

    private void RotateWheels(float targetYangle)
    {
        wheelFL.transform.localEulerAngles = new Vector3(wheelFL.transform.localEulerAngles.x, targetYangle, wheelFL.transform.localEulerAngles.z);
        wheelFR.transform.localEulerAngles = new Vector3(wheelFR.transform.localEulerAngles.x, targetYangle, wheelFR.transform.localEulerAngles.z);
    }

    public static void HandBrakeEnable() { HandBrakeState = true; }
    public static void HandBrakeDisable() { HandBrakeState = false; }

    public void HandBrake()
    {
        ResetTorque();
        wheelRL.brakeTorque = 42000f;
        wheelRR.brakeTorque = 42000f;
    }
    private void ResetBrakes()
    {
        wheelFL.brakeTorque = 0f;
        wheelFR.brakeTorque = 0f;
        wheelRL.brakeTorque = 0f;
        wheelRR.brakeTorque = 0f;
    }

    private void ResetTorque()
    {
        wheelFL.motorTorque = 0f;
        wheelFR.motorTorque = 0f;
        wheelRL.motorTorque = 0f;
        wheelRR.motorTorque = 0f;
    }

    private void ResetWheelsRotation()
    {
        wheelFL.transform.localEulerAngles = Vector3.zero;
        wheelFR.transform.localEulerAngles = Vector3.zero;
    }

    private void ResetRigidbody()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void PerformBrake(float force)
    {
        wheelFL.brakeTorque = force * 150f * 0.3f * accelerationStraight;
        wheelFR.brakeTorque = force * 150f * 0.3f * accelerationStraight;
        wheelRL.brakeTorque = force * 150f * 0.7f * accelerationStraight;
        wheelRR.brakeTorque = force * 150f * 0.7f * accelerationStraight;
    }

    private void PerformMoveBackwards(float force)
    {
        ResetBrakes();
        wheelFL.motorTorque = force * accelerationStraight * 0.4f;
        wheelFR.motorTorque = force * accelerationStraight * 0.4f;
        wheelRL.motorTorque = force * accelerationStraight * 0.4f;
        wheelRR.motorTorque = force * accelerationStraight * 0.4f;
    }

    public void ResetCarMovement()
    {
        ResetBrakes();
        ResetTorque();
        ResetWheelsRotation();
        ResetRigidbody();
    }
}
