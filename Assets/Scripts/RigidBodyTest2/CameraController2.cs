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
    private Transform parentRig;
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float distanceAwayMultiplier = 1.5f;
    [SerializeField]
    private float distanceUpMultiplier = 5f;
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
    [SerializeField]
    private float lookDirDampTime = 0.1f;
    [SerializeField]
    private Vector2 camMinDistFromChar = new Vector2(1f, -0.5f);
    [SerializeField]
    private float rightStickThreshold = 0.1f;
    [SerializeField]
    private const float freeRotationDegreePerSecond = -5f;

    private Transform followForm;

    private Vector3 velocityCamSmooth = Vector3.zero;
    private Vector3 velocityLookDir = Vector3.zero;
    private Vector3 lookDir;
    private Vector3 curLookDir;
    private float xAxisRot = 0.0f;
    private CameraPosition firstPersonCamPos;
    private Vector3 savedRigToGoal;
    public Vector2 rightStickPrevFrame = Vector2.zero;
    private float distanceAwayFree;
    private float distanceUpFree;
    private Vector3 freeLookDir;
    
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
        parentRig = this.transform.parent;

        followForm = follow.transform;

        lookDir = followForm.forward;
        curLookDir = followForm.forward;

        firstPersonCamPos = new CameraPosition();
        firstPersonCamPos.Init("First Person Camera", new Vector3(0f, 1.4f, 0.8f), new GameObject().transform, followForm);
    }

    private void LateUpdate()
    {
        Vector3 characterOffset = followForm.position + new Vector3(0f, distanceUp, 0f);
        Vector3 lookAt = characterOffset;
        Vector3 targetPosition = Vector3.zero;

        switch(camState)
        {
            case CamStates.Behind:
                ResetCamera();

                if (follow.Speed > 0.0f)
                {
                    float dotForward = Vector3.Dot(this.transform.forward, followForm.forward);
                    lookDir = Vector3.Lerp(
                        // 캐릭터가 좌우회전할 때 lookDir을 forward로
                        followForm.right * (follow.Direction.x < 0f ? 1f : -1f),
                        // 캐릭터가 전후이동할 때 lookDir을 forward로
                        followForm.forward * (follow.Direction.y < 0f ? -1f : 1f),
                        Mathf.Abs(dotForward)
                        );
                    Debug.DrawRay(this.transform.position, lookDir, Color.white);

                    curLookDir = Vector3.Normalize(characterOffset - this.transform.position);
                    curLookDir.y = 0.0f;
                    Debug.DrawRay(this.transform.position, curLookDir, Color.green);

                    curLookDir = Vector3.SmoothDamp(curLookDir, lookDir, ref velocityLookDir, lookDirDampTime);
                }

                targetPosition = characterOffset + followForm.up * distanceUp - Vector3.Normalize(curLookDir) * distanceAway;
                //Debug.DrawLine(followForm.position, targetPosition, Color.magenta);
                break;
            case CamStates.Target:
                ResetCamera();

                lookDir = followForm.forward;
                curLookDir = followForm.forward;

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

            case CamStates.Free:
                ResetCamera();

                //Vector3 rigToGoalDirection = Vector3.Normalize(characterOffset - this.transform.position);
                //rigToGoalDirection.y = 0.0f;

                //Vector3 rigToGoal = characterOffset - parentRig.position;
                //rigToGoal.y = 0.0f;

                //Debug.DrawRay(parentRig.position, rigToGoal, Color.yellow);
                //Debug.DrawRay(this.transform.position, rigToGoalDirection, Color.green);

                //// 카메라 높이 및 거리 조절
                //if (follow.LookDir.y < -1f * rightStickThreshold && follow.LookDir.y <= rightStickPrevFrame.y)
                //{
                //    distanceUpFree = Mathf.Lerp(distanceUp, distanceUp * distanceUpMultiplier, Mathf.Abs(follow.LookDir.y));
                //    distanceAwayFree = Mathf.Lerp(distanceAway, distanceAway * distanceAwayMultiplier, Mathf.Abs(follow.LookDir.y));
                //    targetPosition = characterOffset + followForm.up * distanceUpFree - rigToGoalDirection * distanceAwayFree;
                //}
                //else if (follow.LookDir.y > rightStickThreshold && follow.LookDir.y >= rightStickPrevFrame.y)
                //{
                //    distanceUpFree = Mathf.Lerp(Mathf.Abs(transform.position.y - characterOffset.y), camMinDistFromChar.y, follow.LookDir.y);
                //    distanceAwayFree = Mathf.Lerp(rigToGoal.magnitude, camMinDistFromChar.x, follow.LookDir.y);
                //    targetPosition = characterOffset + followForm.up * distanceUpFree - rigToGoalDirection * distanceAwayFree;
                //}
                if (follow.LookDir.y < -1f * rightStickThreshold && follow.LookDir.y <= rightStickPrevFrame.y)
                {
                    distanceUpFree = Mathf.Lerp(distanceUp, distanceUp * distanceUpMultiplier, Mathf.Abs(follow.LookDir.y));
                    distanceAwayFree = Mathf.Lerp(distanceAway, distanceAway * distanceAwayMultiplier, Mathf.Abs(follow.LookDir.y));
                }
                else if (follow.LookDir.y > rightStickThreshold && follow.LookDir.y >= rightStickPrevFrame.y)
                {
                    distanceUpFree = Mathf.Lerp(Mathf.Abs(parentRig.position.y - characterOffset.y), camMinDistFromChar.y, follow.LookDir.y);
                    distanceAwayFree = Mathf.Lerp(freeLookDir.magnitude, camMinDistFromChar.x, follow.LookDir.y);
                }

                //if (follow.LookDir.x != 0.0f || follow.LookDir.y != 0.0f)
                //{
                //    savedRigToGoal = rigToGoalDirection;
                //}
                //Debug.DrawRay(this.transform.position, savedRigToGoal, Color.white);
                //Debug.DrawRay(this.transform.position, rigToGoalDirection, Color.black);
                // 카메라 회전
                parentRig.RotateAround(characterOffset, followForm.up, freeRotationDegreePerSecond * follow.LookDir.x * -1f);

                //if (targetPosition == Vector3.zero)
                //{
                //    targetPosition = characterOffset + followForm.up * distanceUpFree - savedRigToGoal * distanceAwayFree;
                //}

                // 키입력이 있을 때만 카메라 기준 포지션 수정
                if (follow.LookDir.x != 0.0f || follow.LookDir.y != 0.0f)
                {
                    SetFreeLookDir();
                }

                if (targetPosition == Vector3.zero)
                {
                    targetPosition = characterOffset + followForm.up * distanceUpFree - Vector3.Normalize(freeLookDir) * distanceAwayFree;
                }
                break;
        }

        SmoothPosition(parentRig.position, targetPosition);

        transform.LookAt(lookAt);

        rightStickPrevFrame = follow.LookDir;
    }

    private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        parentRig.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void ResetCamera()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
    }

    private void SetFreeLookDir()
    {
        freeLookDir = Vector3.Normalize(followForm.position + new Vector3(0f, distanceUp, 0f) - parentRig.position);
        freeLookDir.y = 0.0f;
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
            case CamStates.Free:
                xAxisRot = 0.0f;
                camState = CamStates.FirstPerson;
                break;
        }
    }

    public void SwitchFreeView()
    {
        camState = CamStates.Free;
        savedRigToGoal = Vector3.zero;
        SetFreeLookDir();
    }
}
