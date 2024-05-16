using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public GameObject textBalloonPrefab;

    private int playerHealth;
    private int coinCount;
    private Image heartFill;
    public GameObject collectParticles;
    public GameObject keyCollectParticles;

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip[] femaleGrunts;
    public AudioClip[] collectGlass;

    void Start()
    {
        lmScript = LevelManager.GetComponent<LevelManager>();
        characterController = GetComponent<CharacterController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        playerHealth = PlayerPrefs.GetInt("playerHealth");

        if (playerHealth <= 0)
        {
            PlayerPrefs.SetInt("playerHealth", 10);
            playerHealth = PlayerPrefs.GetInt("playerHealth");
        }

        heartFill = GameObject.Find("HeartFill").GetComponent<Image>();

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
                    audioSource.PlayOneShot(jumpSound);
                    isJumpPressed = true;
                }
            }
        } else {
            Animator.Play(PLAYER_IDLE);
        }

        heartFill.fillAmount = PlayerPrefs.GetInt("playerHealth") / 10f;
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

    void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "door1":
                Destroy(GameObject.FindWithTag("TextBalloon"));
                GameObject textBalloonD1 = Instantiate(textBalloonPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                textBalloonD1.SendMessage("Setup", "Aqui é meu quarto. Não quero voltar ainda...");
                break;

            case "door2":
                Destroy(GameObject.FindWithTag("TextBalloon"));
                GameObject textBalloonD2 = Instantiate(textBalloonPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                textBalloonD2.SendMessage("Setup", "Algo aconteceu aqui dentro... Por que será que isolaram esse quarto?");
                break;

            case "door5":
                if (lmScript.doorLocked == true)
                {
                    GameObject textBalloonD5 = Instantiate(textBalloonPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                    textBalloonD5.SendMessage("Setup", "Preciso de uma chave se eu quiser entrar aqui. É a sala do dono.");
                }
                break;

            case "Glass":
                lmScript.coinCount++;
                PlayerPrefs.SetInt("CoinCount", lmScript.coinCount);
                GameObject clone = (GameObject)Instantiate(collectParticles, collision.transform.position, Quaternion.identity);
                audioSource.PlayOneShot(collectGlass[Random.Range(0, 2)]);
                Destroy(clone, 0.5f);
                Destroy(collision.gameObject);
                break;

            case "Enemy":
                if (playerHealth > 1)
                {
                    playerHealth -= 1;
                    PlayerPrefs.SetInt("playerHealth", playerHealth);
                    audioSource.PlayOneShot(femaleGrunts[Random.Range(0, 5)]);
                } else {
                    playerHealth -= 1;
                    PlayerPrefs.SetInt("playerHealth", playerHealth);
                    lmScript.canMove = false;
                }
                break;

            case "Key1":
                PlayerPrefs.SetInt("keyTopHalfCollected", 1);
                GameObject clone2 = (GameObject)Instantiate(keyCollectParticles, collision.transform.position, Quaternion.identity);
                Destroy(clone2, 0.5f);
                Destroy(collision.gameObject);
                break;

            case "Key2":
                PlayerPrefs.SetInt("keyBottomHalfCollected", 1);
                GameObject clone3 = (GameObject)Instantiate(keyCollectParticles, collision.transform.position, Quaternion.identity);
                Destroy(clone3, 0.5f);
                Destroy(collision.gameObject);
                break;

            default:
                break;
        }
    }
}
