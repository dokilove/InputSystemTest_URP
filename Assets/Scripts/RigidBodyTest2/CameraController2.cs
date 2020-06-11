using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private Transform followForm;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;
    [SerializeField]
    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);

    private Vector3 velocityCamSmooth = Vector3.zero;
    private Vector3 lookDir;
    private Vector3 targetPosition;

    private void LateUpdate()
    {
        Vector3 characterOffset = followForm.position + offset;

        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0.0f;
        lookDir.Normalize();
        Debug.DrawRay(this.transform.position, lookDir, Color.green);

        targetPosition = characterOffset + followForm.up * distanceUp - lookDir * distanceAway;
        Debug.DrawLine(followForm.position, targetPosition, Color.magenta);

        SmoothPosition(this.transform.position, targetPosition);

        transform.LookAt(followForm);
    }

    void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }
}
