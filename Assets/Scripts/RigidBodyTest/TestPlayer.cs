using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    //public InputAction moveAction;
    //public InputActionMap gameplayActions;
    public InputController inputController;

    Rigidbody rigidBody;
    Vector3 rbVelocity = Vector3.zero;
    public float moveVelocity = 10.0f;
    public float jumpVelocity = 10.0f;
    public float downAccel = 5.0f;


    GroundChecker groundChecker;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        groundChecker = GetComponentInChildren<GroundChecker>();
    }

    private void FixedUpdate()
    {
        rbVelocity = new Vector3(inputController.direction.x, 0.0f, inputController.direction.y) * moveVelocity;
        if (inputController.isJumped)// && groundChecker.isGrounded)
        {
            //rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            rbVelocity.y = jumpVelocity;
        }
        else if (!inputController.isJumped && groundChecker.isGrounded)
        {
            rbVelocity.y = 0.0f;
        }
        else
        {
            rbVelocity.y -= downAccel;
        }

        //Debug.Log(groundChecker.isGrounded);

        rigidBody.velocity = rbVelocity;
    }
}
