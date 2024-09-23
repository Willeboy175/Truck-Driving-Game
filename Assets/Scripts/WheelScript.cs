using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : ControlsScript
{
    [Header("Wheel Objects")]
    public GameObject rightWheel;
    public GameObject leftWheel;
    [Space]

    [Header("Debug Values Wheels")]
    public float rightRPM;
    public float leftRPM;
    [Space]

    public float rightForwardForce;
    public float leftForwardForce;
    public float rightForwardSlip;
    public float leftForwardSlip;
    public float rightSidewaySlip;
    public float leftSidewaySlip;
    [Space]

    protected WheelCollider rightCollider;
    protected WheelCollider leftCollider;

    protected Transform rightModel;
    protected Transform leftModel;

    protected WheelHit rightHit;
    protected WheelHit leftHit;

    protected Vector3 rightPos;
    protected Vector3 leftPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        rightCollider = rightWheel.GetComponent<WheelCollider>();
        leftCollider = leftWheel.GetComponent<WheelCollider>();

        rightModel = rightWheel.GetComponentInChildren<Transform>().transform.Find("Wheel");
        leftModel = leftWheel.GetComponentInChildren<Transform>().transform.Find("Wheel");

        rightCollider.suspensionExpansionLimited = true;
        leftCollider.suspensionExpansionLimited = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        DebugValues();

        rightModel.position = rightPos;
        leftModel.position = leftPos;

        rightModel.localPosition = new Vector3(0, rightModel.localPosition.y, 0);
        leftModel.localPosition = new Vector3(0, leftModel.localPosition.y, 0);
    }

    protected virtual void DebugValues()
    {
        rightCollider.GetGroundHit(out rightHit);
        leftCollider.GetGroundHit(out leftHit);

        rightCollider.GetWorldPose(out rightPos, out Quaternion rightQuat);
        leftCollider.GetWorldPose(out leftPos, out Quaternion leftqQuat);

        rightForwardForce = rightHit.force;
        leftForwardForce = leftHit.force;

        rightForwardSlip = rightHit.forwardSlip;
        leftForwardSlip = leftHit.forwardSlip;

        rightSidewaySlip = rightHit.sidewaysSlip;
        leftSidewaySlip = leftHit.sidewaysSlip;
    }
}
