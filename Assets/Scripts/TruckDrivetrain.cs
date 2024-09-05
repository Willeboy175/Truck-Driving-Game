using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TruckDrivetrain : ControlsScript
{
    public GameObject rightWheel;
    public GameObject leftWheel;

    public float force;
    public float throttleValue;

    public float rightRPM;
    public float leftRPM;

    public float rightForwardforce;
    public float leftForwardforce;

    public float rightForwardSlip;
    public float leftForwardSlip;

    public float rightSidewaysSlip;
    public float leftSidewaysSlip;

    private WheelCollider rightCollider;
    private WheelCollider leftCollider;

    // Start is called before the first frame update
    void Start()
    {
        rightCollider = rightWheel.GetComponent<WheelCollider>();
        leftCollider = leftWheel.GetComponent<WheelCollider>();
    }

    protected override void Awake()
    {
        drive = new PlayerControls();
        drive.Driving.Enable();
        throttle = drive.Driving.Throttle;
    }

    protected override void OnEnable()
    {
        throttle.Enable();
    }

    protected override void OnDisable()
    {
        throttle.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        throttleValue = throttle.ReadValue<float>();

        rightRPM = rightCollider.rpm;
        leftRPM = leftCollider.rpm;

        rightCollider.GetGroundHit(out WheelHit rightHit);
        leftCollider.GetGroundHit(out WheelHit leftHit);

        rightForwardforce = rightHit.force;
        leftForwardforce = leftHit.force;

        rightForwardSlip = rightHit.forwardSlip;
        leftForwardSlip = leftHit.forwardSlip;

        rightSidewaysSlip = rightHit.sidewaysSlip;
        leftSidewaysSlip = leftHit.sidewaysSlip;

    }

    void FixedUpdate()
    {
        rightCollider.motorTorque = throttleValue * force;
        leftCollider.motorTorque = throttleValue * force;
    }
}
