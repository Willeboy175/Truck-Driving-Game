using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutomaticDrivetrain : TruckDrivetrain
{
    public int currentDriveMode;
    public bool shifting;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        shift.started += OnShift;
        shift.canceled += OnShift;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        shift.started -= OnShift;
        shift.canceled -= OnShift;
    }

    protected virtual void OnShift(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shifting = true;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (shifting)
        {
            currentDriveMode = DriveMode(currentDriveMode, 2, shiftValue);
            shifting = false;
        }

        //force = AutomaticShift(currentDriveMode, currentGear, gearRatios, shiftUpRPM, shiftDownRPM, engineRPM, engineTorque);

        rightCollider.motorTorque = throttleValue * force;
        leftCollider.motorTorque = throttleValue * force;
    }

    protected virtual int DriveMode(int currentMode, int modes, float shifting)
    {
        if (shifting > 0 && currentMode < modes) // Shift up
        {
            currentMode += 1;
        }

        if (shifting < 0 && currentMode > 0) // Shift down
        {
            currentMode -= 1;
        }

        return currentMode;
    }

    protected virtual float AutomaticShift(int driveMode, int currentGear, float[] gears, float shiftUpRPM, float shiftDownRPM, float engineRPM, float engineTorque)
    {
        if (driveMode == 2) // Drive
        {

        }

        if (driveMode == 1) // neutral
        {

        }

        if (driveMode == 0) // Reverse
        {

        }

        float currentRatio = 0;

        return currentRatio;
    }
}
