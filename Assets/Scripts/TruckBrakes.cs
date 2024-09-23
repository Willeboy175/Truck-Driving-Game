using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBrakes : WheelScript
{
    [Header("Brakes")]
    public float brakeTorque;
    [Space]

    [Header("Debug Values Brakes")]
    public float brakeValue;

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
        brake.Enable();
    }

    protected override void OnDisable()
    {
        brake.Disable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        brakeValue = brake.ReadValue<float>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        rightCollider.brakeTorque = brakeTorque * brakeValue;
        leftCollider.brakeTorque = brakeTorque * brakeValue;
    }
}
