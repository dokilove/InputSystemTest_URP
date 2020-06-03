using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    public InputController inputController;

    Rigidbody rigidBody;
    Vector3 rbVelocity = Vector3.zero;
    public float moveVelocity = 10.0f;
    public float jumpVelocity = 10.0f;
    public float downAccel = 5.0f;

    public float turnSmoothTime = 0.1f;

    public float groundHeight;

    float distanceToGround;
    Transform cameraTransform;

    CameraController cameraController;

    float turnSmoothVelocity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
               
        rbVelocity = (cameraController.cameraForward * inputController.direction.y + cameraController.cameraRight * inputController.direction.x).normalized;
        //rbVelocity = new Vector3(inputController.direction.x, 0.0f, inputController.direction.y) * moveVelocity;

        Vector2 direction = new Vector2(rbVelocity.x, rbVelocity.z).normalized;
        rbVelocity *= moveVelocity;

        if (direction.sqrMagnitude > 0.0f)
        {
            float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.y);
            float angle = Mathf.SmoothDampAngle(rigidBody.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            rigidBody.rotation = (Quaternion.Euler(new Vector3(0, angle, 0)));
        }

        if (inputController.isJumped && Grounded())
        {
            rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            //rbVelocity.y = jumpVelocity;
        }
        else if (!inputController.isJumped && Grounded())
        {
            rbVelocity.y = 0.0f;
        }
        else
        {
            rbVelocity.y -= downAccel;
        }

        //if (Grounded())
        //{
        //    rbVelocity.y = 0.0f;
        //}
        //else
        //{
        //    rbVelocity.y -= downAccel;
        //}

        //if (inputController.isJumped)
        //{
        //    rbVelocity.y += jumpVelocity;        
        //}

        rigidBody.velocity = rbVelocity;
        Debug.Log(rigidBody.velocity.y);
    }

    bool Grounded()
    {
        distanceToGround = downAccel * 0.04f;
        Debug.DrawRay(transform.position + Vector3.up * distanceToGround * 0.5f, Vector3.down * distanceToGround, Color.green);
        bool result = Physics.Raycast(transform.position + Vector3.up * distanceToGround * 0.5f, Vector3.down, distanceToGround, 1 << 8 | 1 << 9);
        if (result)
            groundHeight = transform.position.y;
        return result;
    }

    public void SetCameraController(CameraController controller)
    {
        cameraController = controller;
    }
}
