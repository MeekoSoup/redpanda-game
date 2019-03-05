using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rbody;

    public float walkH = 20f;
    public float walkV = 50f;

    public float runModH = 1.5f;
    public float runModV = 2f;

    public float fidgetTime = 12f;

    private float inputH;
    private float inputV;

    private float fidgetTimer;

    private bool running = false;
    private bool jumping = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        fidgetTimer = fidgetTime;
    }

    private void Update()
    {
        // Debug commands
        if (Input.GetKeyDown("1") && CheckIdle())
        {
            anim.Play("WAIT01", -1, 0f);
        }
        else if (Input.GetKeyDown("2") && CheckIdle())
        {
            anim.Play("WAIT02", -1, 0f);
        }
        else if (Input.GetKeyDown("3") && CheckIdle())
        {
            anim.Play("WAIT03", -1, 0f);
        }
        else if (Input.GetKeyDown("4") && CheckIdle())
        {
            anim.Play("WAIT04", -1, 0f);
        }

        // hold shift to run
        if (Input.GetKey(KeyCode.LeftShift))
            running = true;
        else
            running = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            anim.SetTrigger("jump");
        }

        // get axis input
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputHorizontal", inputH);
        anim.SetFloat("inputV", inputV);
        anim.SetFloat("inputVertical", inputV);
        anim.SetBool("running", running);

        float moveX = inputH * walkH * Time.deltaTime;
        float moveZ = inputV * walkV * Time.deltaTime;

        if (running)
        {
            moveX *= runModH;
            moveZ *= runModV;
        }

        if (moveZ <= 0f)
            moveX = 0f;


        Vector3 vtemp = new Vector3(moveX, 0f, moveZ);
        vtemp = transform.TransformVector(vtemp);
        rbody.AddForce(vtemp, ForceMode.Impulse);

        if (fidgetTimer < 0)
        {
            ResetFigetTimer();
            if (CheckIdle())
                PlayFidgetAnimation();
        }
        fidgetTimer -= Time.deltaTime;
    }

    public void PlayFidgetAnimation()
    {
        int r = Random.Range(0, 4);

        switch (r)
        {
            case 0:
                anim.Play("WAIT01", -1, 0);
                break;
            case 1:
                anim.Play("WAIT02", -1, 0);
                break;
            case 2:
                anim.Play("WAIT03", -1, 0);
                break;
            case 3:
                anim.Play("WAIT04", -1, 0);
                break;
        }
    }

    private void ResetFigetTimer()
    {
        fidgetTimer = fidgetTime;
    }

    public bool CheckIdle()
    {
        if (inputV > 0.1 || inputV < -0.1)
            return false;
        if (jumping)
            return false;
        if (running)
            return false;

        return true;
    }

    public void DoneJumpingAnimation()
    {
        jumping = false;
    }
}
