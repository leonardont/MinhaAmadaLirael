using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public BossBehaviour bossScript;
    public PlayerController playerScript;

    void Start()
    {
        bossScript = GameObject.Find("Boss").GetComponent<BossBehaviour>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        this.transform.rotation = Random.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            if (playerScript.doubleDamage == true)
            {
                bossScript.health -= 4;
            } else {
                bossScript.health -= 2;
            }
        }

        Destroy(this.gameObject);
    }
}
