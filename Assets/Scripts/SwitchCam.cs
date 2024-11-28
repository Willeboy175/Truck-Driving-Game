using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCam : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCam;
    public CinemachineVirtualCamera cinemaCam1;
    public CinemachineVirtualCamera cinemaCam2;
    public CinemachineVirtualCamera cinemaCam3;
    public CinemachineVirtualCamera cinemaCam4;
    public CinemachineVirtualCamera cinemaCam5;
    public CinemachineVirtualCamera cinemaCam6;

    private CinemachineBrain brain;

    private PlayerControls controls;

    private InputActionMap cameras;
    private InputAction cam1;
    private InputAction cam2;
    private InputAction cam3;
    private InputAction cam4;
    private InputAction cam5;
    private InputAction cam6;

    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        cameras = controls.Cameras;
        cam1 = controls.Cameras.Cam1;
        cam2 = controls.Cameras.Cam2;
        cam3 = controls.Cameras.Cam3;
        cam4 = controls.Cameras.Cam4;
        cam5 = controls.Cameras.Cam5;
        cam6 = controls.Cameras.Cam6;
    }

    private void OnEnable()
    {
        cam1.Enable();
        cam2.Enable();
        cam3.Enable();
        cam4.Enable();
        cam5.Enable();
        cam6.Enable();
    }

    private void OnDisable()
    {
        cam1.Disable();
        cam2.Disable();
        cam3.Disable();
        cam4.Disable();
        cam5.Disable();
        cam6.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam1.triggered)
        {
            brain.ActiveVirtualCamera.Priority = 10;
            thirdPersonCam.Priority = 11;
        }

        if (cam2.triggered)
        {
            brain.ActiveVirtualCamera.Priority = 10;
            cinemaCam1.Priority = 11;
        }

        if (cam3.triggered)
        {
            brain.ActiveVirtualCamera.Priority = 10;
            cinemaCam2.Priority = 11;
        }

        if (cam4.triggered)
        {
            brain.ActiveVirtualCamera.Priority = 10;
            cinemaCam3.Priority = 11;
        }

        if (cam5.triggered)
        {
            brain.ActiveVirtualCamera.Priority = 10;
            cinemaCam4.Priority = 11;
        }

        if (cam6.triggered)
        {
            
        }
    }
}
