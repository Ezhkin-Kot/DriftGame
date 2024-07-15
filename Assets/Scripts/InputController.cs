using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private CarMovementController carMovementController;
    private float HorizontalAxis;
    private float VerticalAxis;
    private bool RtBtnState;
    private bool LtBtnState;
    private bool AccBtnState;
    private bool RevBtnState;

    public void RightButtonDown() { RtBtnState = true;}
    public void RightButtonUp() { RtBtnState = false; }
    public void LeftButtonDown() { LtBtnState = true; }
    public void LeftButtonUp() { LtBtnState = false; }
    public void AcceleratorButtonDown() { AccBtnState = true; }
    public void AcceleratorButtonUp() { AccBtnState = false; }
    public void ReverseButtonDown() { RevBtnState = true; }
    public void ReverseButtonUp() { RevBtnState = false; }

    public bool IsActive;
    
    void Update()
    {
        if (HorizontalAxis > 0 && !RtBtnState)
        {
            HorizontalAxis -= Time.deltaTime * 3;
            if (HorizontalAxis < 0.01f) { HorizontalAxis = 0f; }
            HorizontalAxis = Mathf.Clamp(HorizontalAxis, -1f, 1f);
        }
        if (HorizontalAxis < 0 && !LtBtnState )
        {
            HorizontalAxis += Time.deltaTime * 3;
            if (HorizontalAxis > -0.01f) { HorizontalAxis = 0f; }
            HorizontalAxis = Mathf.Clamp(HorizontalAxis, -1f, 1f);
        }
        if (VerticalAxis > 0 && !AccBtnState)
        {
            VerticalAxis -= Time.deltaTime * 2;
            if (VerticalAxis < 0.0001f) { VerticalAxis = 0f; }
            VerticalAxis = Mathf.Clamp(VerticalAxis, -1f, 1f);
        }
        if (VerticalAxis < 0 && !RevBtnState)
        {
            VerticalAxis += Time.deltaTime * 2;
            if (VerticalAxis > -0.0001f) { VerticalAxis = 0f; }
            VerticalAxis = Mathf.Clamp(VerticalAxis, -1f, 1f);
        }
        if (RtBtnState)
        {
            HorizontalAxis += Time.deltaTime * 3;
            HorizontalAxis = Mathf.Clamp(HorizontalAxis, -1f, 1f);
        }
        if (LtBtnState)
        {
            HorizontalAxis -= Time.deltaTime * 3;
            HorizontalAxis = Mathf.Clamp(HorizontalAxis, -1f, 1f);
        }
        if (AccBtnState)
        {
            VerticalAxis += Time.deltaTime * 2;
            VerticalAxis = Mathf.Clamp(VerticalAxis, -1f, 1f);
        }
        if (RevBtnState)
        {
            VerticalAxis -= Time.deltaTime * 2;
            VerticalAxis = Mathf.Clamp(VerticalAxis, -1f, 1f);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            carMovementController.ToMenu();
        }

        if (!IsActive)
        {
            carMovementController.UpdateInputAxis(0f, 0f);
        }
        else
        {
            float horizontalForce;
            float verticalForce;
            //horizontalForce = Input.GetAxis("Horizontal");
            //verticalForce = Input.GetAxis("Vertical");
            horizontalForce = HorizontalAxis;
            verticalForce = VerticalAxis;
            //Debug.Log("H: " + horizontalForce + "V: " + verticalForce);

            carMovementController.UpdateInputAxis(horizontalForce, verticalForce);
        }
    }
}
