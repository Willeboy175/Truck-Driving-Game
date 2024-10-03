using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    public Transform com;
    public Rigidbody rb;
    public Transform drawbar;
    public Transform dolly;
    public WheelCollider[] dollyWheels;
    public WheelCollider[] rearWheels;

    private Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        targetRotation = drawbar.localEulerAngles;

        targetRotation.x = 0;
        targetRotation.z = 0;

        dolly.localEulerAngles = targetRotation;

        for (int i = 0; i < dollyWheels.Length; i++)
        {
            dollyWheels[i].steerAngle = targetRotation.y;
        }

        if (rb.velocity.magnitude < 0.1f)
        {
            for (int i = 0; i < dollyWheels.Length; i++)
            {
                if (i > 1)
                {
                    dollyWheels[i].motorTorque = 1;
                }
                else
                {
                    dollyWheels[i].motorTorque = -1;
                }
            }

            for (int i = 0; i < rearWheels.Length; i++)
            {
                if (i > 1)
                {
                    rearWheels[i].motorTorque = 1;
                }
                else
                {
                    rearWheels[i].motorTorque = -1;
                }
            }
        }
        else
        {
            for (int i = 0; i < dollyWheels.Length; i++)
            {
                dollyWheels[i].motorTorque = 0;
            }

            for (int i = 0; i < rearWheels.Length; i++)
            {
                rearWheels[i].motorTorque = 0;
            }
        }
    }
}
