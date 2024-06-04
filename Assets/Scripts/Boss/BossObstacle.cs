using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObstacle : MonoBehaviour
{
    public float initialYPosition;
    public float targetYPosition;
    public float riseSpeed;
    public float shakeIntensity;
    private float currentYPosition;
    private float randomX, randomY, randomZ;

    void Start()
    {
        currentYPosition = initialYPosition;
        transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }

    void Update()
    {
        if (currentYPosition < targetYPosition)
        {
            currentYPosition += riseSpeed * Time.deltaTime;

            randomX = Random.value * shakeIntensity * 2f - shakeIntensity;
            randomY = Random.value * shakeIntensity * 2f - shakeIntensity;
            randomZ = Random.value * shakeIntensity * 2f - shakeIntensity;

            transform.position = new Vector3(transform.position.x + randomX, currentYPosition + randomY, transform.position.z + randomZ);
        }
    }
}
