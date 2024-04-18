using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    private int lastZPosition;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;

        if (Mathf.Abs(rb.velocity.z) > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.z));
        }
        else if (Mathf.Abs(rb.velocity.x) > 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }



        if (y != 0 && y < 0)
        {

            animator.SetBool("isMovingFaceFront", true);
            animator.SetBool("isMovingFaceBack", false);
        }
        else if (y != 0 && y > 0)
        {
            animator.SetBool("isMovingFaceBack", true);
            animator.SetBool("isMovingFaceFront", false);
        }
        else
        {
            animator.SetBool("isMovingFaceBack", false);
            animator.SetBool("isMovingFaceFront", false);
        }

        if (Input.GetKeyDown("space"))
            animator.SetBool("isJumping", true);
         else
            animator.SetBool("isJumping", false);

    }
}
