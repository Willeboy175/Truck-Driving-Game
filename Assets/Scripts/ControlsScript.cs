using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsScript : MonoBehaviour
{
    [Header("Wheel Objects")]
    public GameObject rightWheel;
    public GameObject leftWheel;
    [Space]

    [Header("Debug Values Wheels")]
    public float rightRPM;
    public float leftRPM;
    [Space]

    public float rightForwardForce;
    public float leftForwardForce;
    public float rightForwardSlip;
    public float leftForwardSlip;
    public float rightSidewaySlip;
    public float leftSidewaySlip;
    [Space]

    protected WheelCollider rightCollider;
    protected WheelCollider leftCollider;

    protected Transform rightModel;
    protected Transform leftModel;

    protected WheelHit rightHit;
    protected WheelHit leftHit;

    protected Vector3 rightPos;
    protected Vector3 leftPos;

    protected PlayerControls controls;
    protected InputActionMap drive;
    protected InputAction throttle;
    protected InputAction brake;
    protected InputAction steer;
    protected InputAction shift;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rightCollider = rightWheel.GetComponent<WheelCollider>();
        leftCollider = leftWheel.GetComponent<WheelCollider>();

        rightModel = rightWheel.GetComponentInChildren<Transform>().transform.Find("Wheel");
        leftModel = leftWheel.GetComponentInChildren<Transform>().transform.Find("Wheel");

        rightCollider.suspensionExpansionLimited = true;
        leftCollider.suspensionExpansionLimited = true;
    }

    protected virtual void Awake()
    {
        controls = new PlayerControls();

        drive = controls.Driving;
        throttle = controls.Driving.Throttle;
        brake = controls.Driving.Brake;
        steer = controls.Driving.Steering;
        shift = controls.Driving.Shifting;
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DebugValues();

        rightModel.position = rightPos;
        leftModel.position = leftPos;

        rightModel.localPosition = new Vector3(0, rightModel.localPosition.y, 0);
        leftModel.localPosition = new Vector3(0, leftModel.localPosition.y, 0);
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void DebugValues()
    {
        rightCollider.GetGroundHit(out rightHit);
        leftCollider.GetGroundHit(out leftHit);

        rightCollider.GetWorldPose(out rightPos, out Quaternion rightQuat);
        leftCollider.GetWorldPose(out leftPos, out Quaternion leftqQuat);

        rightForwardForce = rightHit.force;
        leftForwardForce = leftHit.force;

        rightForwardSlip = rightHit.forwardSlip;
        leftForwardSlip = leftHit.forwardSlip;
        
        rightSidewaySlip = rightHit.sidewaysSlip;
        leftSidewaySlip = leftHit.sidewaysSlip;
    }
}
