using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBalloon : MonoBehaviour
{
    private GameObject player;
    public string textSource;
    private SpriteRenderer backgroundSprite;
    private TextMeshPro textMeshPro;

    private void Awake() 
    {
        backgroundSprite = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    void Start()
    {
        // Setup(textSource);
        player = GameObject.Find("Lirael");

        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x + 3f, player.transform.position.y + 2f, player.transform.position.z);
    }

    private void Setup(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();

        backgroundSprite.size = new Vector2(textMeshPro.GetComponent<RectTransform>().rect.width + 2f, 8f);
        backgroundSprite.transform.localPosition = new Vector3((backgroundSprite.size.x / 30f) - 1f, 0f);
    }
}
