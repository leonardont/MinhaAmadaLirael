using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Animator Animator;

    const string ENEMY_IDLE = "EnemyIdle";
    const string ENEMY_RUN = "EnemyRun";
    const string ENEMY_RUN_FRONT = "EnemyRunFront";
    private string currentAnimation;

    private SpriteRenderer spriteRenderer;
    private Rigidbody enemyRb;

    private bool rememberFlip = false;

    private IEnumerator coroutine;
    public float horizontalSpeed;
    public float depthSpeed;
    public float moveSpeed;
    private int waitingTime;
    public float walkingTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody>();

        switch(PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                waitingTime = 4;
                break;

            case 1:
                waitingTime = 2;
                break;

            case 2:
                waitingTime = 0;
                break;

            default:
                waitingTime = 2;
                break;
        }

        Vector3 positiveMove = new Vector3(horizontalSpeed, 0, depthSpeed).normalized * moveSpeed;
        Vector3 negativeMove = new Vector3(-horizontalSpeed, 0, -depthSpeed).normalized * moveSpeed;

        coroutine = MoveEnemy(positiveMove, negativeMove, waitingTime, walkingTime);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        if (enemyRb.velocity.x < 0f)
        {
            rememberFlip = true;
        } else if (enemyRb.velocity.x > 0f) {
            rememberFlip = false;
        }

        if (rememberFlip == true)
        {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (enemyRb.velocity.x != 0 || enemyRb.velocity.z != 0)
        {
            if (enemyRb.velocity.z < -0.2f) 
            {
                ChangeAnimationState(ENEMY_RUN_FRONT);
            }
            else 
            {
                ChangeAnimationState(ENEMY_RUN);
            }
        } 
        else 
        {
            ChangeAnimationState(ENEMY_IDLE);
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        currentAnimation = newAnimation;

        Animator.Play(newAnimation);
    }

    private IEnumerator MoveEnemy(Vector3 positiveMove, Vector3 negativeMove, int waitingTime, float walkingTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitingTime);
            enemyRb.velocity = positiveMove;
            yield return new WaitForSeconds(walkingTime);
            enemyRb.velocity = Vector3.zero;
            yield return new WaitForSeconds(waitingTime);
            enemyRb.velocity = negativeMove;
            yield return new WaitForSeconds(walkingTime);
            enemyRb.velocity = Vector3.zero;
        }
    }
}