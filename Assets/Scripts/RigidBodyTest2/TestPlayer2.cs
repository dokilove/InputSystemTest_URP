using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer2 : MonoBehaviour
{
    [SerializeField]
    private CameraController2 gameCam;

    [SerializeField]
    private float moveVelocity = 10.0f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;

    private Rigidbody rigidBody;
    private Vector3 moveDirection;
    private float speed = 0.0f;
    float turnSmoothVelocity;

    private Vector2 direction = Vector2.zero;

    public Vector2 Direction { get { return direction; } }
    public float Speed { get { return speed; } }

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameCam.CamState != CameraController2.CamStates.FirstPerson)
        {
            StickToWorldSpace(this.transform, gameCam.transform, ref moveDirection, ref speed);

            if (speed > 0.0f)
            {
                float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(moveDirection.x, moveDirection.z);
                float angle = Mathf.SmoothDampAngle(rigidBody.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                rigidBody.rotation = (Quaternion.Euler(new Vector3(0, angle, 0)));
            }

            rigidBody.velocity = moveDirection * moveVelocity;
        }
    }

    public void StickToWorldSpace(Transform root, Transform camera, ref Vector3 moveDirectionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 stickDirection = new Vector3(direction.x, 0.0f, direction.y);

        speedOut = stickDirection.sqrMagnitude;

        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0.0f;

        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        moveDirectionOut = referentialShift * stickDirection;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        bool target = context.ReadValue<float>() > 0.01f;

        gameCam.SwitchTargetView(target);
    }


    private bool runFpView = false;
    public void OnFirstPersonView(InputAction.CallbackContext context)
    {
        if (speed > 0.0f)
            return;

        bool fpView = context.ReadValue<float>() >= 0.0f;

        if (fpView)
        {
            if (!runFpView)
            {
                runFpView = true;
                gameCam.SwitchFirstPersonView();
            }
        }

        if (context.ReadValue<float>() == 0.0f)
            runFpView = false;
    }
}
