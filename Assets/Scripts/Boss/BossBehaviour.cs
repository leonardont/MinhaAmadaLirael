using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float speed;
    public float health = 30;

    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.Find("Lirael").transform;
        rb = GetComponent<Rigidbody>();

        switch(PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                speed = 2f;
                break;

            case 1:
                speed = 3f;
                break;

            case 2:
                speed = 4f;
                break;

            default:
                speed = 2f;
                break;

        }
    }

    void Update()
    {
        Vector2 target = new Vector2(player.position.x, 3.5f);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);
    }
}
