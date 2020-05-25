using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded = false;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 8)
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == 8)
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == 8)
    //    {
    //        isGrounded = false;
    //    }
    //}

    public float distanceToGround = 0.2f;

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up * distanceToGround * 0.5f, Vector3.down, distanceToGround, 1 << 8);
        Debug.DrawRay(transform.position + Vector3.up * distanceToGround * 0.5f, Vector3.down * distanceToGround, Color.red);
    }
}
