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

    bool isJumped;
    Vector2 direction;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        rigidBody.MovePosition(this.transform.position + new Vector3(direction.x, 0.0f, direction.y) * 10.0f * Time.deltaTime);
        if (isJumped)
        {
            rigidBody.AddForce(Vector3.up, ForceMode.Impulse);
        }
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
