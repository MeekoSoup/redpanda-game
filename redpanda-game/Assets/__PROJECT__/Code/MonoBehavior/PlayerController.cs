using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Model Properties

    [Header("Model Properties")]
    public GameObject model;

    #endregion

    #region Movement Properties

    [Header("Movement Properties")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 3.5f;
    public float sprintSpeed = 5f;
    [Tooltip("At what percent of the joystick control should we be running?"), Space]
    public float runThreshold = 0.25f;

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

    private float horizontalInput;
    private float verticalInput;

    #endregion

    #region Required Components

    private Rigidbody rb;
    private Animator anim;

    #endregion

    #region Hidden Properties

    [HideInInspector]
    public bool isMoving;
    public bool isGrounded;

    #endregion

    private void Awake()
    {
        if (model == null)
        {
            Debug.Log("Error: Didn't assign model.");
            return;
        }

        rb = model.GetComponent<Rigidbody>();
        anim = model.GetComponent<Animator>();

        // initialize properties
        isMoving = false;
        isGrounded = false;
    }

    private void Update()
    {
        UpdateInput();
        UpdateMovement();
    }

    private void UpdateInput()
    {
        // get horizontal and vertical input values
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void UpdateMovement()
    {
        UpdateVerticleMovement();
        //UpdateRotation();
    }

    private void UpdateVerticleMovement()
    {
        if (playerCamera == null)
            return;

        if (Mathf.Approximately(verticalInput, 0.0f))
        {
            isMoving = false;
            return;
        }

        isMoving = true;

        float speed = walkSpeed;

        if (verticalInput > runThreshold)
            speed = runSpeed;

        Vector3 dir = playerCamera.transform.forward;
        dir.y = 0;

        transform.Translate(dir * verticalInput * speed * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        Vector3 dir = playerCamera.transform.forward;
        dir.y = 0f;

        transform.rotation = Quaternion.LookRotation(dir);
    }
}
