using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Model Properties

    [Header("Model Properties")]
    public GameObject model;

    #endregion

    #region Movement Properties

    [Header("Movement Properties")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 10f;
    [Tooltip("At what percent of the joystick control should we be running?"), Space]
    public float runThreshold = 0.25f;
    public float rotationSpeed = 360f;

    #endregion

    #region Camera Controls

    [Header("Camera Controls"), Space]
    public GameObject playerCamera;

    #endregion

    #region Input Properties

    [Header("Input Controls")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode dashKey = KeyCode.LeftShift;

    private Vector2 input;

    #endregion

    #region Required Components

    private Rigidbody rb;
    private Animator anim;

    #endregion

    #region Hidden Properties

    [HideInInspector]
    public bool isMoving, isGrounded, isSprinting;

    public float speed;

    private Vector3 previousPosition, currentPosition, velocity;

    #endregion

    private void Awake()
    {
        if (model == null)
        {
            Debug.Log("Error: Didn't assign model.");
            return;
        }

        rb = GetComponent<Rigidbody>();
        anim = model.GetComponent<Animator>();

        // initialize properties
        isMoving = false;
        isGrounded = false;

        previousPosition = Vector3.zero;
        currentPosition = Vector3.zero;
        velocity = Vector3.zero;
    }

    private void Update()
    {
        UpdateInput();
        UpdateMovement();
        UpdateAnimations();
    }

    private void UpdateInput()
    {
        // get horizontal and vertical input values
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);
        speed = Mathf.Clamp01(speed);

        isSprinting = Input.GetKey(sprintKey);
    }

    private void UpdateMovement()
    {
        isMoving = !Mathf.Approximately(speed, 0f);

        UpdateVerticleMovement();
        UpdateRotation();
    }

    private void UpdateVerticleMovement()
    {
        if (playerCamera == null)
            return;

        if (!isMoving)
            return;

        float speedMod = walkSpeed;

        if (input.y > runThreshold)
            speedMod = runSpeed;

        if (isSprinting)
            speedMod = sprintSpeed;

        Vector3 dir = Vector3.zero;
        if (!Mathf.Approximately(input.y, 0f))
            dir += playerCamera.transform.forward * Mathf.Sign(input.y);
        if (!Mathf.Approximately(input.x, 0f))
            dir += playerCamera.transform.right * Mathf.Sign(input.x);
        dir.y = 0;
        dir.Normalize();

        transform.Translate(dir * speed * speedMod * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        // Gets velocity, but only the direction part of it
        currentPosition = transform.position;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;
        velocity.Normalize();
        velocity.y = 0;

        // if velocity is zero, don't change rotation
        if (Mathf.Approximately(0f, velocity.x) && Mathf.Approximately(0f, velocity.z))
            return;

        Quaternion rot = model.transform.rotation;
        Quaternion lookRotation = Quaternion.LookRotation(velocity);

        model.transform.rotation = Quaternion.RotateTowards(rot, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void UpdateAnimations()
    {
        //anim.SetFloat("InputVertical", speed, 0.1f, Time.deltaTime); // dampening
        if (isSprinting) speed += 0.5f;
        anim.SetFloat("InputVertical", speed);

    }
}
