using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{

    Vector3 mousePos;
    [SerializeField] private Transform player;
    [SerializeField] private float flashLightRange;

    [SerializeField] private GameObject lightPlatform;

    private SpriteRenderer _spriteRenderer;
    private Color spriteColor;

    private float distNormalized;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        UpdatePosition();

        UpdateOpacity();

        if (Input.GetMouseButtonDown(0))
        {
            InstantiatePlatform();
        }

    }

    private void UpdatePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 2f;
        transform.position = mousePos;
    }
    private void UpdateOpacity()
    {
        float dist = Vector2.Distance(transform.position, player.position);


        distNormalized = Mathf.Clamp((dist / flashLightRange), 0.2f, 1f);
        float colorApply = (1 - distNormalized);
        spriteColor = _spriteRenderer.color;

        _spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, colorApply);
    }
    private void InstantiatePlatform()
    {
        GameObject platform = Instantiate(lightPlatform, transform.position, transform.rotation);
        platform.GetComponent<LightPlatform>().StartCountdown(distNormalized);

    }


}
