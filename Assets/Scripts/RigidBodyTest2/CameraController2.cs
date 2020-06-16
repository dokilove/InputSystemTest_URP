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


    private Vector3 velocityCamSmooth = Vector3.zero;
    private Vector3 lookDir;
    private Vector3 targetPosition;
    private CamStates camState = CamStates.Behind;

    public enum CamStates
    {
        Behind,
        FirstPerson,
        Target,
        Free
    }

    private void Start()
    {
        lookDir = followForm.forward;
    }

    private void LateUpdate()
    {
        Vector3 characterOffset = followForm.position + new Vector3(0f, distanceUp, 0f);

        switch(camState)
        {
            case CamStates.Behind:
                lookDir = characterOffset - this.transform.position;
                lookDir.y = 0.0f;
                lookDir.Normalize();
                Debug.DrawRay(this.transform.position, lookDir, Color.green);

                targetPosition = characterOffset + followForm.up * distanceUp - lookDir * distanceAway;
                Debug.DrawLine(followForm.position, targetPosition, Color.magenta);
                break;
            case CamStates.Target:
                lookDir = followForm.forward;
                break;
        }

        targetPosition = characterOffset + followForm.up * distanceUp - lookDir * distanceAway;

        SmoothPosition(this.transform.position, targetPosition);

        transform.LookAt(characterOffset);
    }

    private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    public void SwitchTargetView(bool target)
    {
        if (target)
            camState = CamStates.Target;
        else
            camState = CamStates.Behind;
    }
}
