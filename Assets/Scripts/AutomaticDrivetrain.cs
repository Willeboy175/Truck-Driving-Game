using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("UI")]
    public TextMeshProUGUI textSpeed;
    public TextMeshProUGUI textRPM;
    public TextMeshProUGUI textDriveMode;
    public TextMeshProUGUI textGear;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        shift.started += OnShift;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        shift.started -= OnShift;
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

        UserInterface();
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

        else if (currentDriveMode == 1) // neutral
        {
            currentRatio = 0;
            currentGear = 0;
        }

        else if (currentDriveMode == 0) // Reverse
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

    protected virtual void UserInterface()
    {
        int roundedSpeed = Mathf.RoundToInt((float)speed);
        textSpeed.SetText("Speed: " + roundedSpeed + " km/h");

        int roundedRPM = Mathf.RoundToInt(currentEngineRPM);
        textRPM.SetText("Engine: " + roundedRPM + " RPM");

        if (currentDriveMode == 2) // Drive
        {
            textDriveMode.SetText("Drivemode: D");
            textGear.SetText("Gear: " + (currentGear + 1));
        }

        else if (currentDriveMode == 1) // neutral
        {
            textDriveMode.SetText("Drivemode: N");
            textGear.SetText("Gear: " + currentGear);
        }

        else if (currentDriveMode == 0) // Reverse
        {
            textDriveMode.SetText("Drivemode: R");
            textGear.SetText("Gear: R" + (currentGear + 1));
        }
    }

    protected override void DebugValues()
    {
        base.DebugValues();

        currentWheelTorque = throttleValue * currentEngineTorque * currentRatio;
    }
}
