using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutomaticDrivetrain : TruckDrivetrain
{
    [Header("Automatic gearbox")]
    public float upShiftRPM;
    public float downShiftRPM;
    public float engineClutchLockRPM;
    [Space]

    [Header("Debug Values Automatic Drivetrain")]
    public int currentDriveMode;
    public bool shifting;
    public float currentWheelTorque;

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
            currentGear = 0;
        }

        if (currentDriveMode == 0) // Reverse
        {
            if (currentEngineRPM > upShiftRPM && currentGear < reverseGearRatios.Length - 1) // Shift up
            {
                currentGear += 1;
            }
            else if (currentEngineRPM < downShiftRPM && currentGear > 0) // Shift down
            {
                currentGear -= 1;
            }

            currentRatio = reverseGearRatios[currentGear] * diffRatio;
        }

        return currentRatio;
    }

    protected override void DebugValues()
    {
        base.DebugValues();

        currentWheelTorque = throttleValue * currentEngineTorque * currentRatio;
    }
}
