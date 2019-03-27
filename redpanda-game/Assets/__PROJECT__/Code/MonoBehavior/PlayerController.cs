using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Model Properties

    [Header("Model Properties")]
    public GameObject model;
    public float height = 3f;
    public float radius = 1f;

    #endregion

    #region Movement Properties

    [Header("Movement Properties")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 10f;
    [Tooltip("At what percent of the joystick control should we be running?"), Space]
    public float runThreshold = 0.25f;
    public float rotationSpeed = 360f;

    [Header("Jump Properties")]
    public float jumpSpeed = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground & Collision Properties")]
    public LayerMask groundLayer;

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
    public bool isMoving, 
        isGrounded, 
        isSprinting, 
        isJumping;

    public float speed;

    private Vector3 previousPosition, currentPosition, velocity;
    private GameObject groundObject;

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
        isGrounded = true;

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
        if (Input.GetKeyDown(jumpKey) && IsGrounded())
            isJumping = true;

        isSprinting = Input.GetKey(sprintKey);
    }

    private void UpdateMovement()
    {
        isMoving = !Mathf.Approximately(speed, 0f);
        
        UpdateVerticleMovement();
        UpdateRotation();
        UpdateJumping();
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector3.down;
        float distance = .1f;

        Debug.DrawRay(position, direction * distance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, distance, groundLayer) && groundObject != null)
            return true;

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        LayerMask layer = collision.gameObject.layer;
        bool matched = ((1 << layer) & groundLayer) != 0;

        if (matched)
        {
            groundObject = collision.gameObject;
        }
    }

    private void UpdateJumping()
    {
        if (!isJumping)
            return;

        if (!IsGrounded())
            return;

        isJumping = false;

        rb.velocity = Vector3.up * jumpSpeed;
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
            dir += playerCamera.transform.forward * input.normalized.y;
        if (!Mathf.Approximately(input.x, 0f))
            dir += playerCamera.transform.right * input.normalized.x;
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
        Quaternion lookRotation = Quaternion.identity;
        if (velocity != Vector3.zero)
            lookRotation = Quaternion.LookRotation(velocity);

        model.transform.rotation = Quaternion.RotateTowards(rot, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void UpdateAnimations()
    {
        //anim.SetFloat("InputVertical", speed, 0.1f, Time.deltaTime); // dampening
        if (isSprinting) speed += 0.5f;
        anim.SetFloat("InputVertical", speed);
        anim.SetFloat("VerticalVelocity", rb.velocity.y);
        anim.SetBool("IsGrounded", IsGrounded());

    }
}
