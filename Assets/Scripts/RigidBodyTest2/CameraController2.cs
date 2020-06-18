using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct CameraPosition
{
    private Vector3 position;
    private Transform xForm;

    public Vector3 Position { get { return position; } set { position = value; } }
    public Transform XForm { get { return xForm; } set { xForm = value; } }

    public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
    {
        position = pos;
        xForm = transform;
        xForm.name = camName;
        xForm.parent = parent;
        xForm.localPosition = position;
    }
}

public class CameraController2 : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private TestPlayer2 follow;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;
    [SerializeField]
    private float firstPersonLookSpeed = 1.5f;
    [SerializeField]
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 60.0f);
    [SerializeField]
    private float fpsRotationDegreePerSecond = 120.0f;

    private Transform followForm;

    private Vector3 velocityCamSmooth = Vector3.zero;
    private Vector3 lookDir;
    private Vector3 targetPosition;
    private float xAxisRot = 0.0f;
    private CameraPosition firstPersonCamPos;

    [SerializeField]
    private CamStates camState = CamStates.Behind;
    public CamStates CamState { get { return camState; } }

    public enum CamStates
    {
        Behind,
        FirstPerson,
        Target,
        Free
    }

    private void Start()
    {
        followForm = follow.transform;

        lookDir = followForm.forward;

        firstPersonCamPos = new CameraPosition();
        firstPersonCamPos.Init("First Person Camera", new Vector3(0f, 1.4f, 0.8f), new GameObject().transform, followForm);
    }

    private void LateUpdate()
    {
        Vector3 characterOffset = followForm.position + new Vector3(0f, distanceUp, 0f);
        Vector3 lookAt = characterOffset;

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

                targetPosition = characterOffset + followForm.up * distanceUp - lookDir * distanceAway;
                break;
            case CamStates.FirstPerson:
                // 위 아래 보기
                xAxisRot += (-follow.Direction.y * firstPersonLookSpeed);
                xAxisRot = Mathf.Clamp(xAxisRot, firstPersonXAxisClamp.x, firstPersonXAxisClamp.y);
                firstPersonCamPos.XForm.localRotation = Quaternion.Euler(xAxisRot, 0.0f, 0.0f);

                // 이 오브젝트의 회전을 firstPersonCampos에 맞춰서 돌려준다
                Quaternion rotationShift = Quaternion.FromToRotation(this.transform.forward, firstPersonCamPos.XForm.forward);
                this.transform.rotation = rotationShift * this.transform.rotation;

                // 캐릭터 좌우 회전
                Vector3 rotationAmount = Vector3.Lerp(Vector3.zero,
                    new Vector3(0.0f, fpsRotationDegreePerSecond * (follow.Direction.x < 0.0f ? -1f : 1f), 0.0f),
                    Mathf.Abs(follow.Direction.x));
                Quaternion deltaRoattion = Quaternion.Euler(rotationAmount * Time.deltaTime);
                follow.transform.rotation = follow.transform.rotation * deltaRoattion;

                // 카메라를 firstPersonCamPos로
                targetPosition = firstPersonCamPos.XForm.position;

                lookAt = Vector3.Lerp(targetPosition + followForm.forward, this.transform.position + this.transform.forward, camSmoothDampTime * Time.deltaTime);

                // lookAt
                lookAt = Vector3.Lerp(this.transform.position + this.transform.forward, lookAt, Vector3.Distance(this.transform.position, firstPersonCamPos.XForm.position));

                break;
        }

        SmoothPosition(this.transform.position, targetPosition);

        transform.LookAt(lookAt);
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

    public void SwitchFirstPersonView()
    {

        switch(camState)
        {
            case CamStates.Target:
                break;
            case CamStates.FirstPerson:
                camState = CamStates.Behind;
                break;
            case CamStates.Behind:
                xAxisRot = 0.0f;
                camState = CamStates.FirstPerson;
                break;
        }
    }
}
