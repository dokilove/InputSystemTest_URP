using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isGrounded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }


    //private void Update()
    //{
    //    isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.1f, 1 << 8);
    //}
}
