using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject LevelManager;
    [NonSerialized] public LevelManager lmScript;

    public float speed;
    public float health;

    public AudioSource audioSource;
    public AudioClip powerBuild;
    public AudioClip earthQuake;
    public AudioClip earthBreak;
    public AudioClip mageScream;

    private Transform player;

    private float lastPositionX;
    private float targetX;

    private Animator animator;
    private string currentAnimation;

    private bool bossIsAttacking;
    public GameObject bossAttackSphere;
    public GameObject attackHit;
    public GameObject bossObstacle;
    public GameObject disappearObstacle;

    public GameObject deathParticles;
    
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle1disappear;
    public GameObject obstacle2disappear;

    private SpriteRenderer spriteRenderer;

    const string BOSS_IDLE = "BossIdle";
    const string BOSS_RUN = "BossRun";
    const string BOSS_ATTACK = "BossAttack";

    float minDistanceThreshold = 2f;

    public float fillAmount;
    public GameObject ending;
    public bool dead;

    Coroutine attackCr;

    void Start()
    {
        attackCr = StartCoroutine(AttackCoroutine());

        lmScript = LevelManager.GetComponent<LevelManager>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Lirael").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        lastPositionX = transform.position.x;

        speed = 0.8f;

        switch(PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                health = 50f;
                break;

            case 1:
                health = 70f;
                break;

            case 2:
                health = 100f;
                break;

            default:
                health = 50f;
                break;
        }
    }

    void Update()
    {
        float targetX = player.position.x;

        if (bossIsAttacking == false && spriteRenderer) {

            float currentSpeed = Mathf.Abs(transform.position.x - lastPositionX);
            lastPositionX = transform.position.x;
            
            Vector3 newPos = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);

            if (Mathf.Abs(player.position.x - transform.position.x) > minDistanceThreshold) {
                if (currentSpeed > 0.005f) {
                    spriteRenderer.flipX = targetX > transform.position.x;
                    ChangeAnimationState(BOSS_RUN);
                } else {
                    ChangeAnimationState(BOSS_IDLE);
                }
            } else {
                ChangeAnimationState(BOSS_IDLE);
            }

        } else if (spriteRenderer) {
            ChangeAnimationState(BOSS_ATTACK);
        }

        switch(PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                GameObject.Find("Health").GetComponent<Image>().fillAmount = health / 50f;
                break;

            case 1:
                GameObject.Find("Health").GetComponent<Image>().fillAmount = health / 70f;
                break;

            case 2:
                GameObject.Find("Health").GetComponent<Image>().fillAmount = health / 100f;
                break;

            default:
                GameObject.Find("Health").GetComponent<Image>().fillAmount = health / 50f;
                break;
        }

        if (health <= 0 && dead == false)
        {
            StopCoroutine(attackCr);

            StartCoroutine(endingScene());

            dead = true;
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        animator.Play(newAnimation);
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(6f);
        
        while (true) {
            yield return new WaitForSeconds(2f);

            bossIsAttacking = true;
            audioSource.PlayOneShot(powerBuild, 0.25f);
            GameObject attack;
            attack = Instantiate(bossAttackSphere, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z - 1f), Quaternion.identity);

            audioSource.PlayOneShot(earthQuake, 0.15f);

            obstacle1 = Instantiate(bossObstacle, new Vector3((-25f + Random.Range(-3f, -12f)), 0f, Random.Range(5f, -5f)), Quaternion.identity);

            obstacle2 = Instantiate(bossObstacle, new Vector3((-25f + Random.Range(3f, 12f)), 0f, Random.Range(5f, -5f)), Quaternion.identity);

            yield return new WaitForSeconds(6f);

            bossIsAttacking = false;

            yield return new WaitForSeconds(4f);

            audioSource.PlayOneShot(earthBreak, 0.15f);

            GameObject obstacle1disappear;
            obstacle1disappear = Instantiate(disappearObstacle, obstacle1.transform.position, Quaternion.identity);
            Destroy(obstacle1);
            GameObject obstacle2disappear;
            obstacle2disappear = Instantiate(disappearObstacle, obstacle2.transform.position, Quaternion.identity);
            Destroy(obstacle2);

            Destroy(obstacle1disappear, 2f);
            Destroy(obstacle2disappear, 2f);
        }
    }

    private IEnumerator endingScene()
    {
        LevelManager.GetComponent<AudioSource>().Stop();

        audioSource.PlayOneShot(mageScream, 0.25f);

        Instantiate(deathParticles, this.transform.position, Quaternion.identity);

        Destroy(this.gameObject.transform.GetChild(0).gameObject);

        Destroy(GameObject.Find("BossAttack(Clone)"));

        if (obstacle1 || obstacle2)
        {
            AudioSource.PlayClipAtPoint(earthBreak, GameObject.Find("Main Camera").transform.position, 0.15f);
            obstacle1disappear = Instantiate(disappearObstacle, obstacle1.transform.position, Quaternion.identity);
            obstacle2disappear = Instantiate(disappearObstacle, obstacle2.transform.position, Quaternion.identity);
            Destroy(obstacle1);
            Destroy(obstacle2);

            Destroy(obstacle1disappear, 2f);
            Destroy(obstacle2disappear, 2f);
        }

        Time.timeScale = 0.5f;
        
        while(true) {
            
            yield return new WaitForSeconds(4f);

            lmScript.canMove = false;

            ending.SetActive(true);

            CanvasGroup UIGroup;
            UIGroup = ending.GetComponent<CanvasGroup>();

            while (UIGroup.alpha < 1)
            {
                UIGroup.alpha += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(8f);

            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
