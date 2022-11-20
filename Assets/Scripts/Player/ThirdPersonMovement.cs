using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public AudioManager audioManager;

    public CharacterController controller;
    public Transform camera;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    public float gravity = -9.81f;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isRunning = false;
    public float jumpHeight = 3f;
    public float sprintSpeedMultiplier = 3f;
    public float sprintJumpMultiplier = 1.5f;
    private Vector3 velocity;

    private float originalSpeed;

    private float originalJumpHeight;
    public Animator DogAnimator;
    void Start()
    {
        originalSpeed = speed;
        originalJumpHeight = jumpHeight;
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            DogAnimator.SetBool("IsStartedJumping", false);
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetButtonDown("Fire3"))
        {
            isRunning = true;
            if (direction.magnitude > 0.1f)
            {
                DogAnimator.SetBool("IsRunning", true);
            }
            speed *= sprintSpeedMultiplier;
            jumpHeight *= sprintJumpMultiplier;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            isRunning = false;
            DogAnimator.SetBool("IsRunning", false);
            speed = originalSpeed;
            jumpHeight = originalJumpHeight;
        }

        if (direction.magnitude >= 0.1f)
        {
            if (isRunning)
                DogAnimator.SetBool("IsRunning", true);
            DogAnimator.SetBool("IsWalking", true);
            audioManager.Play("DogSteps");
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * (speed * Time.deltaTime));
        }
        else
        {
            if(isRunning)
                DogAnimator.SetBool("IsRunning", false);
            DogAnimator.SetBool("IsWalking", false);
            audioManager.Stop("DogSteps");
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            DogAnimator.SetBool("IsStartedJumping", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Bark();
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Bark()
    {
        DogAnimator.SetBool("IsBarking", true);
        audioManager.Play("Bark");
    }

    public void StopBarking()
    {
        DogAnimator.SetBool("IsBarking", false);
    }
}
