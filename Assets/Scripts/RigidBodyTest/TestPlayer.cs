using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour, PlayerControls.IGamePlayActions
{
    public InputAction moveAction;
    public InputActionMap gameplayActions;

    PlayerControls controls;

    Rigidbody rigidBody;
    Vector3 rbVelocity = Vector3.zero;
    float downAccel = 5.0f;

    bool isJumped;
    Vector2 direction;

    GroundChecker groundChecker;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        groundChecker = GetComponentInChildren<GroundChecker>();
    }

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
            controls.GamePlay.SetCallbacks(this);
        }
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    private void FixedUpdate()
    {
        rbVelocity = new Vector3(direction.x, 0.0f, direction.y) * 10.0f;
        if (isJumped)// && groundChecker.isGrounded)
        {
            //rigidBody.AddForce(Vector3.up * 25.0f, ForceMode.Impulse);
            rbVelocity.y = 10.0f;
        }
        else if (!isJumped && groundChecker.isGrounded)
        {
            rbVelocity.y = 0.0f;
        }
        else
        {
            rbVelocity.y -= downAccel;
        }

        Debug.Log(groundChecker.isGrounded);

        rigidBody.velocity = rbVelocity;
    }

    public void OnGrow(InputAction.CallbackContext context)
    {
        isJumped = context.ReadValue<float>() > 0.0f;        
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
    }

    public void OnRotateY(InputAction.CallbackContext context)
    {
    }
}
