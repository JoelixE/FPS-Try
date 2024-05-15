using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 5.0f;

    [Header("Jump Parameters")]

    [Header("Look Sensitivity")]

    [Header("Inputs Customisation")]

    [Header("Footstep Sounds")]


    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float verticalSpeed = Input.GetAxis("Vertical") * walkSpeed;
    }
}
