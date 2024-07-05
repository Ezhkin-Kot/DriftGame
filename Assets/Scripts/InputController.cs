using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private CarMovementController carMovementController;

    public bool IsActive;
    public Joystick joystick;
    
    void Update()
    {
        if (!IsActive)
        {
            carMovementController.UpdateInputAxis(0f, 0f);
        }
        else
        {
            float horizontalForce;
            float verticalForce;
            if (joystick != null)
            {
                horizontalForce = joystick.Horizontal;
                verticalForce = joystick.Vertical;
            }
            else
            {
                horizontalForce = Input.GetAxis("Horizontal");
                verticalForce = Input.GetAxis("Vertical");
            }

            carMovementController.UpdateInputAxis(horizontalForce, verticalForce);
        }
    }
}
