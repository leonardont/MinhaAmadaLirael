using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject LevelManager;
    [NonSerialized] public LevelManager lmScript;

    public Animator Animator;

    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "PlayerRun";
    const string PLAYER_RUN_BACK = "PlayerRunBack";
    const string PLAYER_RUN_FRONT = "PlayerRunFront";
    const string PLAYER_JUMP = "PlayerJump";
    private bool isGrounded;
    private bool isJumpPressed;
    private string currentAnimation;
    private float horizontalInput;

    public float speed;
    public float jumpSpeed;

    private CharacterController characterController;
    private SpriteRenderer spriteRenderer;

    private bool rememberFlip = false;

    private float ySpeed;

    void Start()
    {
        lmScript = LevelManager.GetComponent<LevelManager>();
        characterController = GetComponent<CharacterController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
        {
            PlayerPrefs.SetInt("lastRoomEntered", SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Update()
    {
        if (lmScript.canMove == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection.Normalize();

            if (characterController.velocity.x < 0f)
            {
                rememberFlip = true;
            } else if (characterController.velocity.x > 0f) {
                rememberFlip = false;
            }

            if (rememberFlip == true)
            {
                spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }

            Vector3 velocity = movementDirection * magnitude;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                { 
                    isJumpPressed = true;
                }
            }
        } else {
            Animator.Play(PLAYER_IDLE);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit1, 1.2f) || 
            Physics.Raycast(new Vector3(transform.position.x - 0.65f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), out hit2, 1.2f) || 
            Physics.Raycast(new Vector3(transform.position.x + 0.65f, transform.position.y, transform.position.z), transform.TransformDirection(Vector3.down), out hit3, 1.2f) || 
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.65f), transform.TransformDirection(Vector3.down), out hit3, 1.2f) || 
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.65f), transform.TransformDirection(Vector3.down), out hit3, 1.2f))
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }

        if (isGrounded && isJumpPressed)
        {
            ySpeed = jumpSpeed;
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }
        else if (isGrounded)
        {
            if (characterController.velocity.x != 0 || characterController.velocity.z != 0)
            {
                if (characterController.velocity.z > 0.2f) 
                {
                    ChangeAnimationState(PLAYER_RUN_BACK);
                }
                else if (characterController.velocity.z < -0.2f) 
                {
                    ChangeAnimationState(PLAYER_RUN_FRONT);
                }
                else 
                {
                    ChangeAnimationState(PLAYER_RUN);
                }
            } 
            else 
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        else
        {
            if (!isJumpPressed)
            {
                ChangeAnimationState(PLAYER_JUMP);
            }
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        Animator.Play(newAnimation);
    }
}
