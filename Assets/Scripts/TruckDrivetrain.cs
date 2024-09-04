using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TruckDrivetrain : MonoBehaviour
{
    public GameObject rightWheel;
    public GameObject leftWheel;

    public float force;
    public float throttleValue;

    private WheelCollider rightCollider;
    private WheelCollider leftCollider;

    private PlayerControls controls;
    private InputAction throttle;

    // Start is called before the first frame update
    void Start()
    {
        rightCollider = rightWheel.GetComponent<WheelCollider>();
        leftCollider = leftWheel.GetComponent<WheelCollider>();
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Driving.Enable();
        throttle = controls.Driving.Throttle;
    }

    private void OnEnable()
    {
        throttle.Enable();
    }

    private void OnDisable()
    {
        throttle.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        throttleValue = throttle.ReadValue<float>();
    }

    void FixedUpdate()
    {
        rightCollider.motorTorque = throttleValue * force;
        leftCollider.motorTorque = throttleValue * force;
    }
}
