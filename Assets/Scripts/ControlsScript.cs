using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsScript : MonoBehaviour
{
    public GameObject rightWheel;
    public GameObject leftWheel;
    [Space]

    protected WheelCollider rightCollider;
    protected WheelCollider leftCollider;

    protected PlayerControls controls;
    protected InputActionMap drive;
    protected InputAction throttle;
    protected InputAction brake;
    protected InputAction steer;
    protected InputAction shift;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rightCollider = rightWheel.GetComponent<WheelCollider>();
        leftCollider = leftWheel.GetComponent<WheelCollider>();
    }

    protected virtual void Awake()
    {
        controls = new PlayerControls();

        drive = controls.Driving;
        throttle = controls.Driving.Throttle;
        brake = controls.Driving.Brake;
        steer = controls.Driving.Steering;
        shift = controls.Driving.Shifting;
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {

    }
}
