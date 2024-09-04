using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    public Transform com;
    public Rigidbody rb;
    public Transform drawbar;
    public Transform dolly;
    public WheelCollider[] wheels;

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
        targetRotation = drawbar.localEulerAngles;

        targetRotation.x = 0;
        targetRotation.z = 0;

        dolly.localEulerAngles = targetRotation;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].steerAngle = targetRotation.y;
        }
    }
}
