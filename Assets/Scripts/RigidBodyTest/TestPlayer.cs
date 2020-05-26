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

    float distanceToGround;
    Transform cameraTransform;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    float turnSmoothVelocity;

    private void FixedUpdate()
    {

        Vector3 forward = (this.transform.position - cameraTransform.position);
        forward.y = 0.0f;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        //Vector3 right = Vector3.Cross(Vector3.up, forward);
               
        rbVelocity = (forward * inputController.direction.y + right * inputController.direction.x).normalized;
        //rbVelocity = new Vector3(inputController.direction.x, 0.0f, inputController.direction.y) * moveVelocity;

        Vector2 direction = new Vector2(rbVelocity.z, rbVelocity.x).normalized;
        rbVelocity *= moveVelocity;

        if (direction.sqrMagnitude > 0.0f)
        {
            float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.y);
            float angle = Mathf.SmoothDampAngle(rigidBody.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            rigidBody.rotation = (Quaternion.Euler(new Vector3(0, angle, 0)));
        }

        if (inputController.isJumped && Grounded())
        {
            //rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            rbVelocity.y = jumpVelocity;
        }
        else if (!inputController.isJumped && Grounded())
        {
            rbVelocity.y = 0.0f;
        }
        else
        {
            rbVelocity.y -= downAccel;
        }

        rigidBody.velocity = rbVelocity;
    }

    bool Grounded()
    {
        distanceToGround = downAccel * 0.04f;
        Debug.DrawRay(transform.position + Vector3.up * distanceToGround * 0.5f, Vector3.down * distanceToGround, Color.green);
        return Physics.Raycast(transform.position + Vector3.up * distanceToGround * 0.5f, Vector3.down, distanceToGround, 1 << 8);
    }

    public void SetCameraTransform(Transform camera)
    {
        cameraTransform = camera;
    }
}
