﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer2 : MonoBehaviour
{
    [SerializeField]
    private InputController inputController;
    [SerializeField]
    private CameraController2 gameCam;

    [SerializeField]
    private float moveVelocity = 10.0f;
    [SerializeField]
    private float turnSmoothTime = 0.1f;

    private Rigidbody rigidBody;
    private Vector3 moveDirection;
    float turnSmoothVelocity;

    private void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StickToWorldSpace(this.transform, gameCam.transform, ref moveDirection);

        if (moveDirection.sqrMagnitude > 0.0f)
        {
            float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(moveDirection.x, moveDirection.z);
            float angle = Mathf.SmoothDampAngle(rigidBody.rotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            rigidBody.rotation = (Quaternion.Euler(new Vector3(0, angle, 0)));
        }

        rigidBody.velocity = moveDirection * moveVelocity;
    }

    public void StickToWorldSpace(Transform root, Transform camera, ref Vector3 moveDirectionOut)
    {
        Vector3 rootDirection = root.forward;
        Vector3 stickDirection = new Vector3(inputController.direction.x, 0.0f, inputController.direction.y);

        //speedOut = stickDirection.sqrMagnitude;

        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0.0f;

        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

        moveDirectionOut = referentialShift * stickDirection;
    }
}
