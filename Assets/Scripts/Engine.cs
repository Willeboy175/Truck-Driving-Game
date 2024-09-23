using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Header("Torque multiplier at respective RPM in RPMValues")]
    public float[] torqueMultiplier;
    public float[] rPMValues;
    [Space]

    public int maxTorque;
    public float idleRPM;
    [Space]

    [Header("Debug Values Engine")]
    public float engineRPM;
    public float engineTorque;
    public float wheelRPM;

    private float[,] engineTorqueAndRPM; // engineTorqueAndRPM[RPM, Torque]

    // Start is called before the first frame update
    void Start()
    {
        engineTorqueAndRPM = new float[torqueMultiplier.Length, 2];
        EngineTorqueAndRPMSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentRPM(float rightWheelRPM, float leftWheelRPM, float gearRatio, float throttleInput, float engineClutchLockRPM)
    {
        float currentWheelRPM = (rightWheelRPM + leftWheelRPM) / 2;

        engineRPM = currentWheelRPM * gearRatio;

        if (engineRPM < engineClutchLockRPM && throttleInput > 0.01f) engineRPM = engineClutchLockRPM;
        else if (engineRPM < idleRPM) engineRPM = idleRPM;
        else if (engineRPM > rPMValues[9]) engineRPM = rPMValues[9];

        return engineRPM;
    }

    public float GetCurrentTorque()
    {
        if (engineRPM <= engineTorqueAndRPM[0, 0])
        {
            engineTorque = 0.0f;
        }
        else if (engineRPM > engineTorqueAndRPM[0, 0] && engineRPM <= engineTorqueAndRPM[9, 0])
        {
            for (int i = 0; i < torqueMultiplier.Length - 1; i++)
            {
                if (engineRPM > engineTorqueAndRPM[i, 0] && engineRPM <= engineTorqueAndRPM[i + 1, 0])
                {
                    engineTorque = Mathf.Lerp(engineTorqueAndRPM[i + 1, 1], engineTorqueAndRPM[i, 1], (engineRPM - engineTorqueAndRPM[i, 0]) / (engineTorqueAndRPM[i + 1, 1] - engineTorqueAndRPM[i, 1]));
                }
            }
        }
        else
        {
            engineTorque = engineTorqueAndRPM[9, 1];
        }

        return engineTorque;
    }

    private void EngineTorqueAndRPMSetup()
    {
        for (int i = 0; i < rPMValues.Length; i++)
        {
            engineTorqueAndRPM[i, 0] = rPMValues[i];

            print(engineTorqueAndRPM[i, 0]);
        }

        for (int i = 0; i < torqueMultiplier.Length; i++)
        {
            engineTorqueAndRPM[i, 1] = torqueMultiplier[i] * maxTorque;

            print(engineTorqueAndRPM[i, 1]);
        }
    }


}
