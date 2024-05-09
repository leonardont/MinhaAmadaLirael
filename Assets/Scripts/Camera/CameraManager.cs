using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public GameObject LevelManager;
    [NonSerialized] public LevelManager lmScript;

    public GameObject player;
    public GameObject main_camera;
 
    void Start()
    {
        lmScript = LevelManager.GetComponent<LevelManager>();
    }

    void Update()
    {
        if (player != null && lmScript.canMove == true)
        {
            float player_x = player.transform.position.x;
            float player_y = player.transform.position.y;
            float player_z = player.transform.position.z;
    
            float rounded_x = RoundToNearestPixel(player_x);
            float rounded_y = RoundToNearestPixel(player_y + 4.5f);
            float rounded_z = RoundToNearestPixel(player_z - 10f);
    
            Vector3 new_pos = new Vector3(rounded_x, rounded_y, rounded_z);
            main_camera.transform.position = new_pos;
        } 
    }

    public float pixelToUnits = 10f;
    
    public float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = unityUnits * pixelToUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
        return roundedUnityUnits;
    }
}
