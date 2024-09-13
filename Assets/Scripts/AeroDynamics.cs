using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroDynamics : MonoBehaviour
{
    public float frontalArea;
    public float cd;

    private float velDependentDrag;
    private Rigidbody rB;

    private const float airDensity = 1.292f;

    // Start is called before the first frame update    
    void Start()
    {
        // get the car rigidbody
        rB = GetComponent<Rigidbody>();
        // calculate the velocity dependent lift and drag factors
        // tempDragVar is only used here
        float tempDragVar = airDensity * frontalArea * 0.5f;
        // cd is the coefficient of drag, note the sign control as Z+ is car forward
        velDependentDrag = cd * tempDragVar;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyAeroDrag(rB.velocity.z);
    }

    public void ApplyAeroDrag(float vel)
    {
        float velSq = vel * vel;
        float drag = velDependentDrag * velSq;
        if (vel < 0.0f) drag = -drag;
        rB.AddRelativeForce(0.0f, 0.0f, -drag, ForceMode.Force);
    }
}
