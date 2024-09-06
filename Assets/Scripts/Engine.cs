using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Tooltip("torque value for every 100 rpm")]
    public float[] torqueValues;
    public float idleRPM;
    public float maxRPM;
    public float ShiftUpRPM;
    public float ShiftDownRPM;
    [Space]

    [Header("DebugValues")]
    public float engineRPM;
    public float wheelRPM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentRPM(float rightWheelRPM, float leftWheelRPM, float gearRatio)
    {
        float currentWheelRPM = (rightWheelRPM + leftWheelRPM) / 2;

        engineRPM = currentWheelRPM * gearRatio;

        return engineRPM;
    }
}
