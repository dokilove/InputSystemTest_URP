using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedJumpPlayer : MonoBehaviour
{
    private bool onGround;
    private float jumpPressure;
    private float minJump;
    private float maxJumpPressure;
    private Rigidbody rbody;
    private Animator anim;

    [SerializeField]
    InputController inputController;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        jumpPressure = 0.0f;
        minJump = 2f;
        maxJumpPressure = 10f;

        rbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            // holding jump button
            if(inputController.isJumped)
            {
                if (jumpPressure < maxJumpPressure)
                {
                    jumpPressure += Time.deltaTime * 10.0f;
                }
                else
                {
                    jumpPressure = maxJumpPressure;
                }
                anim.SetFloat("JumpPressure", jumpPressure + minJump);
                anim.speed = 1f + (jumpPressure/10f);
            }
            // not holding jump button
            else
            {
                if (jumpPressure > 0f)
                {
                    jumpPressure = jumpPressure + minJump;
                    rbody.velocity = new Vector3(jumpPressure/10f, jumpPressure, 0);
                    jumpPressure = 0.0f;
                    onGround = false;
                    anim.SetFloat("JumpPressure", 0.0f);
                    anim.SetBool("onGround", onGround);
                    anim.speed = 1f;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            anim.SetBool("onGround", onGround);
        }
    }
}
