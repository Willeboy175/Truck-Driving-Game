using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsScript : MonoBehaviour
{
    protected PlayerControls controls;

    protected InputActionMap drive;
    protected InputAction throttle;
    protected InputAction brake;
    protected InputAction steer;
    protected InputAction shift;

    protected InputActionMap menu;
    protected InputAction back;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    protected virtual void Awake()
    {
        controls = new PlayerControls();

        drive = controls.Driving;
        throttle = controls.Driving.Throttle;
        brake = controls.Driving.Brake;
        steer = controls.Driving.Steering;
        shift = controls.Driving.Shifting;

        menu = controls.Menus;
        back = controls.Menus.GoBack;
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
