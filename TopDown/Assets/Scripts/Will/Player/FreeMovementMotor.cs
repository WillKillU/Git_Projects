using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FreeMovementMotor : MovementMotor {

    public float    walkSpeed          =   5.0f;
    public float    walkingSnappyness  =   50f;
    public float    turningSmoothing   =   0.3f;

    private Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3     targetVelocity  =   movementDirection * walkSpeed; 
        Vector3     deltaVelocity   =   targetVelocity - rb.velocity;

        if(rb.useGravity)
        {
            deltaVelocity.y     =   0;
        }

        rb.AddForce(deltaVelocity * walkingSnappyness, ForceMode.Acceleration);

        Vector3     faceDir     =   facingDirection;

        if(faceDir.Equals(Vector3.zero))
        {
            faceDir             =   movementDirection;
        }

        if(faceDir.Equals(Vector3.zero))
        {
            rb.angularVelocity  =   Vector3.zero;
        }
        else
        {
            float rotationAngle =   AngleAroundAxis(transform.forward, faceDir, Vector3.up);
            rb.angularVelocity  =   (Vector3.up * rotationAngle * turningSmoothing);
        }
	}

    static float AngleAroundAxis(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        dirA        = dirA - Vector3.Project(dirA, axis);
        dirB        = dirB - Vector3.Project(dirB, axis);

        float angle = Vector3.Angle(dirA, dirB);

        return angle * (Vector3.Dot(axis, Vector3.Cross(dirA, dirB)) < 0 ? -1 : 1);
    }
}
