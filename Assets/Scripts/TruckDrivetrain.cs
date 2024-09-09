using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TruckDrivetrain : ControlsScript
{
    [Header("Gearbox")]
    public float[] gearRatios;
    public float diffRatio;
    public float shiftDelay;

    public float force;
    [Space]

    [Header("Debug values")]
    public float shiftValue;
    public float throttleValue;
    public float brakeValue;
    public int currentGear;
    [Space]

    public double speed;

    public float rightRPM;
    public float leftRPM;
    [Space]

    public float rightForwardforce;
    public float leftForwardforce;

    public float rightForwardSlip;
    public float leftForwardSlip;
    [Space]

    public float rightSidewaysSlip;
    public float leftSidewaysSlip;

    protected Engine engine;
    protected Rigidbody rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        engine = GetComponent<Engine>();

        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
        drive.Enable();
    }

    protected override void OnEnable()
    {
        throttle.Enable();
        brake.Enable();
        shift.Enable();
    }

    protected override void OnDisable()
    {
        throttle.Disable();
        brake.Disable();
        shift.Disable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        throttleValue = throttle.ReadValue<float>();
        brakeValue = brake.ReadValue<float>();
        shiftValue = shift.ReadValue<float>();

        DebugValues();
    }

    protected override void FixedUpdate()
    {
        rightRPM = rightCollider.rpm;
        leftRPM = leftCollider.rpm;

        float engineRPM = engine.GetCurrentRPM(rightRPM, leftRPM, 2);
    }

    protected virtual float Shift(int currentgear, int gears, int nextGear)
    {
        float currentRatio = 0;

        return currentRatio;
    }

    protected virtual void DebugValues()
    {
        speed = rb.velocity.magnitude * 3.6;

        rightCollider.GetGroundHit(out WheelHit rightHit);
        leftCollider.GetGroundHit(out WheelHit leftHit);

        rightForwardforce = rightHit.force;
        leftForwardforce = leftHit.force;

        rightForwardSlip = rightHit.forwardSlip;
        leftForwardSlip = leftHit.forwardSlip;

        rightSidewaysSlip = rightHit.sidewaysSlip;
        leftSidewaysSlip = leftHit.sidewaysSlip;
    }
}
