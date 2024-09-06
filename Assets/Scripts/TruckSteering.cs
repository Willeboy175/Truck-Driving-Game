using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class TruckSteering : ControlsScript
{
    public GameObject rightWheel;
    public GameObject leftWheel;

    public float outerSteerAngle;
    public float innerSteerAngle;
    public float steerSpeed;

    public bool invert;

    private WheelCollider rightCollider;
    private WheelCollider leftCollider;

    public float inputValue;
    public float steerValue;

    // Start is called before the first frame update
    protected override void Start()
    {
        rightCollider = rightWheel.GetComponent<WheelCollider>();
        leftCollider = leftWheel.GetComponent<WheelCollider>();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        steer.Enable();
    }

    protected override void OnDisable()
    {
        steer.Disable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        inputValue = steer.ReadValue<float>();

        steerValue = Mathf.MoveTowards(steerValue, inputValue, steerSpeed);
    }

    void FixedUpdate()
    {
        WheelSteer(rightCollider, rightWheel, steerValue, outerSteerAngle, innerSteerAngle, invert, true); // Right wheel
        WheelSteer(leftCollider, leftWheel, steerValue, outerSteerAngle, innerSteerAngle, invert, false); // Left wheel
    }

    // If rightOrLeft is true that means it is the right wheel and vice versa
    protected virtual void WheelSteer(WheelCollider wheel, GameObject model, float target, float outerAngle, float innerAngle, bool invert, bool rightOrLeft)
    {
        float steerTarget = 0;

        if (rightOrLeft) // Right wheel
        {
            if (target > 0) // Turning to the right
            {
                steerTarget = innerAngle;
            }
            else if (target < 0) // Turning to the left
            {
                steerTarget = outerAngle;
            }
        }
        else // Left wheel 
        {
            if (target > 0) // Turning to the right
            {
                steerTarget = outerAngle;
            }
            else if (target < 0) // Turning to the left
            {
                steerTarget = innerAngle;
            }
        }

        if (invert) // Inverted
        {
            steerTarget *= -1;
        }

        wheel.steerAngle = target * steerTarget;
        model.transform.localEulerAngles = new Vector3(0, target * steerTarget, 0);
    }
}
