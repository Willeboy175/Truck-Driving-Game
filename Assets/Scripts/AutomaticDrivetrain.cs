using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutomaticDrivetrain : TruckDrivetrain
{
    public float upShiftRPM;
    public float downShiftRPM;
    public float engineClutchLockRPM;

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

        currentRatio = AutomaticShift();
        currentEngineRPM = engine.GetCurrentRPM(rightRPM, leftRPM, currentRatio, throttleValue, engineClutchLockRPM);
        currentEngineTorque = engine.GetCurrentTorque();

        rightCollider.motorTorque = throttleValue * currentEngineTorque * currentRatio;
        leftCollider.motorTorque = throttleValue * currentEngineTorque * currentRatio;
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

    protected virtual float AutomaticShift()
    {
        if (currentDriveMode == 2) // Drive
        {
            if (currentEngineRPM > upShiftRPM && currentGear < gearRatios.Length - 1) // Shift up
            {
                currentGear += 1;
            }
            else if (currentEngineRPM < downShiftRPM && currentGear > 0) // Shift down
            {
                currentGear -= 1;
            }

            currentRatio = gearRatios[currentGear] * diffRatio;
        }

        if (currentDriveMode == 1) // neutral
        {
            currentRatio = 0;
        }

        if (currentDriveMode == 0) // Reverse
        {
            currentRatio = 0;
        }

        return currentRatio;
    }
}
