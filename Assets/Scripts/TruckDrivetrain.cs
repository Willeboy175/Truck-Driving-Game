using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDrivetrain : WheelScript
{
    [Header("Gearbox")]
    public float[] gearRatios;
    public float[] reverseGearRatios;
    public float diffRatio;
    public float shiftDelay;
    [Space]

    [Header("Debug Values Drivetrain")]
    public float shiftValue;
    public float throttleValue;
    [Space]

    public double speed;
    public float currentRatio;
    public int currentGear;
    [Space]

    public float currentEngineRPM;
    public float currentEngineTorque;

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
        shift.Enable();
    }

    protected override void OnDisable()
    {
        throttle.Disable();
        shift.Disable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        throttleValue = throttle.ReadValue<float>();
        shiftValue = shift.ReadValue<float>();

        DebugValues();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rightRPM = rightCollider.rpm;
        leftRPM = leftCollider.rpm;
    }

    protected override void DebugValues()
    {
        base.DebugValues();
        speed = rb.velocity.magnitude * 3.6;
    }
}
