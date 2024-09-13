using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class TruckSteering : ControlsScript
{
    [Header("Steering")]
    public float outerSteerAngle;
    public float innerSteerAngle;
    public float steerSpeed;
    public bool invert;
    [Space]

    [Header("Debug Values Steering")]
    public float inputSteerValue;
    public float steerValue;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
        drive.Enable();
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
        base.Update();
        inputSteerValue = steer.ReadValue<float>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        steerValue = Mathf.MoveTowards(steerValue, inputSteerValue, steerSpeed / 1000);

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
