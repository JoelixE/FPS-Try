using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;

    [Header("Inputs Customisation")]
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [Space]
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";
    [SerializeField] private Camera mainCamera;
    [Space]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Footstep Sounds")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSound;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float sprintStepInterval = 0.3f;
    [SerializeField] private float velocityTreshold = 2.0f;


    private bool isMoving;
    private int lastPlayIndex = -1;
    private float nextSteepTime;
    private float verticalRotation;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleFootsteps();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis(verticalMoveInput);
        float horizontalInput = Input.GetAxis(horizontalMoveInput);
        float speedMultiplier = Input.GetKey(sprintKey) ? sprintMultiplier : 1f;

        float verticalSpeed = Input.GetAxis(verticalMoveInput) * walkSpeed * speedMultiplier;
        float horizontalSpeed = Input.GetAxis(horizontalMoveInput) * walkSpeed * speedMultiplier;

        Vector3 horizontalMovement = new Vector3 (horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        HandleGravityAndJumping();

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        characterController.Move(currentMovement * Time.deltaTime);

        isMoving = verticalInput != 0 || horizontalInput != 0;
    }

    void HandleGravityAndJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (Input.GetKeyDown(jumpKey))
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis(MouseYInput) * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void HandleFootsteps()
    {
        float currentStepInterval = (Input.GetKey(sprintKey) ? sprintStepInterval : walkStepInterval);

        if(characterController.isGrounded && isMoving && Time.time > nextSteepTime && characterController.velocity.magnitude > velocityTreshold)
        {
            PlayFoostepSounds();
            nextSteepTime = Time.time + currentStepInterval;
        }
    }

    void PlayFoostepSounds()
    {
        int randomIndex;

        if (footstepSound.Length == 1)
        {
            randomIndex = 0;
        }
        else
        {
            randomIndex = Random.Range(0, footstepSound.Length -1);
            if (randomIndex >= lastPlayIndex)
            {
                randomIndex++;
            }
        }

        lastPlayIndex = randomIndex;
        footstepSource.clip = footstepSound[randomIndex];
        footstepSource.Play();
    }
}
