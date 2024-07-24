using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float growthRate;
    public float maxRadius;
    private float currentRadius;
    private SphereCollider sphereCollider;
    public float speed;

    public GameObject attackHit;

    private GameObject player;

    public AudioClip attackSound;

    void Start()
    {
        currentRadius = transform.localScale.x;
        sphereCollider = GetComponent<SphereCollider>();

        switch(PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                speed = 15f;
                break;

            case 1:
                speed = 20f;
                break;

            case 2:
                speed = 25f;
                break;

            default:
                speed = 15f;
                break;
        }

        player = GameObject.Find("Lirael");
        StartCoroutine(FollowPlayer());
    }

    void Update()
    {
        if (currentRadius < maxRadius)
        {
            currentRadius += growthRate * Time.deltaTime;
            transform.localScale = Vector3.one * currentRadius * 1f;

            if (sphereCollider != null)
            {
                sphereCollider.radius = currentRadius / 2f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(attackSound, GameObject.Find("Lirael").transform.position);
        GameObject attackParticle;
        attackParticle = Instantiate(attackHit, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(attackParticle, 1f);
    }

    private IEnumerator FollowPlayer()
    {
        yield return new WaitForSeconds(6f);

        while (true)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            float movement = speed * Time.deltaTime;
            transform.position += direction * movement;

            yield return null;
        }
    }
}