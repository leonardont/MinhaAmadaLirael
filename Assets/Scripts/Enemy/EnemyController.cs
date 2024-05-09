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
    public int waitingTime;
    public int walkingTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody>();

        coroutine = MoveEnemy(horizontalSpeed, depthSpeed, waitingTime, walkingTime);
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

    private IEnumerator MoveEnemy(float horizontalSpeed, float depthSpeed, int waitingTime, int walkingTime)
    {
        Vector3 positiveMove = new Vector3(horizontalSpeed, 0, depthSpeed).normalized * Time.deltaTime * moveSpeed;
        Vector3 negativeMove = new Vector3(-horizontalSpeed, 0, -depthSpeed).normalized * Time.deltaTime * moveSpeed;

        while(true)
        {
            yield return new WaitForSeconds(waitingTime);
            enemyRb.AddForce(positiveMove);
            yield return new WaitForSeconds(walkingTime);
            enemyRb.AddForce(negativeMove);
            yield return new WaitForSeconds(waitingTime);
            enemyRb.AddForce(negativeMove);
            yield return new WaitForSeconds(walkingTime);
            enemyRb.AddForce(positiveMove);
        }
    }
}
