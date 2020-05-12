using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour
{
    PlayerControls controls;

    Vector2 move;
    Vector2 rotate;

    float rotateY;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.GamePlay.Grow.performed += ctx => Grow();

        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;

        controls.GamePlay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.GamePlay.Rotate.canceled += ctx => rotate = Vector2.zero;

        controls.GamePlay.RotateY.performed += ctx => rotateY = ctx.ReadValue<float>();
        controls.GamePlay.RotateY.canceled += ctx => rotateY = 0.0f;
    }

    private void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * 5.0f;
        transform.Translate(m, Space.World);

        Vector2 r = new Vector2(rotate.y, rotate.x) * Time.deltaTime * 100.0f;
        transform.Rotate(r, Space.World);

        float rY = rotateY * Time.deltaTime * 100.0f;
        transform.RotateAroundLocal(Vector3.up, rY);
    }


    void Grow()
    {
        transform.localScale *= 1.1f;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}