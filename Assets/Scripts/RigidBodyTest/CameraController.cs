using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform mainTarget;
    public InputController inputController;

    public bool smoothFollow = true;
    public float smooth = 0.05f;
    public float lookSmooth = 100.0f;

    public Vector3 camOffset = new Vector3(0.0f, 0.75f, 0.0f);
    public float camRotX = -20.0f;
    public float camRotY = -180.0f;
    public float camDistance = 4.0f;

    public Vector3 cameraForward;
    public Vector3 cameraRight;

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 camVel = Vector3.zero;


    private void Awake()
    {
        mainTarget.GetComponent<TestPlayer>().SetCameraController(this);
    }

    void Start()
    {
        MoveToTarget();
    }

    private void FixedUpdate()
    {

        cameraForward = (mainTarget.position - this.transform.position);
        cameraForward.y = 0.0f;
        cameraForward = cameraForward.normalized;

        cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);
        //cameraRight = Vector3.Cross(Vector3.up, forward);

        MoveToTarget();
        LookAtTarget();
        OrbitTarget();
    }

    void MoveToTarget()
    {
        targetPos = mainTarget.position + camOffset;
        destination = Quaternion.Euler(camRotX, camRotY, 0.0f) // lookat rotation
            * Vector3.forward * camDistance;
        destination += targetPos;

        if (smoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, smooth);
        }
        else
        {
            transform.position = destination;
        }
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSmooth);
    }

    void OrbitTarget()
    {
        if (inputController.resetCam)
        {
            camRotX = -20.0f;
            camRotY = -180.0f + mainTarget.eulerAngles.y;
        }

        camRotX += -inputController.rotate.y;
        camRotY += -inputController.rotate.x;
    }
}
