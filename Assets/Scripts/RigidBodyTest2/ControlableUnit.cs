using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableUnit : MonoBehaviour
{
    [SerializeField]
    private float moveVelocity = 10.0f;
    [SerializeField]
    private float jumpVelocity = 10.0f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2.0f;
    
    private Vector3 moveDirection;
    private float turnSmoothVelocity;

    protected Vector2 direction = Vector2.zero;
    protected Vector2 lookDir = Vector2.zero;
    protected bool jumpVal = false;
    protected float speed = 0.0f;

    public Vector2 Direction {
        get { return direction; }
        set { direction = value; }
    }
    public Vector2 LookDir {
        get { return lookDir; }
        set { lookDir = value; }
    }
    public bool JumpVal {
        get { return jumpVal; }
        set { jumpVal = value; }
    }

    public float Speed { get { return speed; } }

    protected Rigidbody rigidBody;
    protected CameraController2 gameCam;

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    public void SetCam(CameraController2 cam)
    {
        gameCam = cam;
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        InputMove();
    }

    public void InputMove()
    {
        if (null == gameCam)
            return;

        StickToWorldSpace(this.transform, gameCam.transform, ref moveDirection, ref speed);

        if (gameCam.CamState != CameraController2.CamStates.FirstPerson)
        {
            if (speed > 0.0f)
            {
                float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(moveDirection.x, moveDirection.z);
                float angle = Mathf.SmoothDampAngle(rigidBody.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                rigidBody.rotation = (Quaternion.Euler(new Vector3(0, angle, 0)));
            }
        }

        Vector3 moveVel = moveDirection * this.moveVelocity;
        moveVel.y = rigidBody.velocity.y;

        rigidBody.velocity = moveVel;

        // Better Jump https://www.youtube.com/watch?v=7KiK0Aqtmzc
        if (rigidBody.velocity.y < 0.0f)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1.0f) * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0.0f && !jumpVal)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1.0f) * Time.deltaTime;
        }
    }

    public void StickToWorldSpace(Transform root, Transform camera, ref Vector3 moveDirectionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 stickDirection = new Vector3(direction.x, 0.0f, direction.y);

        speedOut = stickDirection.sqrMagnitude;


        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0.0f;
        cameraDirection = cameraDirection.normalized;

        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        Debug.DrawRay(camera.position + Vector3.up, cameraDirection, Color.green);
        Debug.DrawRay(camera.position + Vector3.up, stickDirection, Color.red);

        moveDirectionOut = referentialShift * stickDirection;

        Debug.DrawRay(camera.position + Vector3.up, moveDirectionOut, Color.blue);
    }

    public void Jump()
    {
        Vector3 jumpVel = Vector3.up * jumpVelocity;
        jumpVel.x = rigidBody.velocity.x;
        jumpVel.z = rigidBody.velocity.z;
        rigidBody.velocity = jumpVel;
    }
}
