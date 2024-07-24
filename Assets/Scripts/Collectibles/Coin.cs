using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float velocity;

    public Material[] letters;
    private GameObject frontSide, backSide;
    private int materialIndex;
    private Material chosenMaterial;

    void Start()
    {
        velocity = Random.Range(25f, 75f);

        materialIndex = Random.Range(0, letters.Length);
        chosenMaterial = letters[materialIndex];
        frontSide = transform.GetChild(0).GetChild(0).gameObject;
        backSide = transform.GetChild(0).GetChild(1).gameObject;
        frontSide.GetComponent<MeshRenderer>().material = new Material(chosenMaterial);
        backSide.GetComponent<MeshRenderer>().material = new Material(chosenMaterial);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * velocity, 0);
    }
}
