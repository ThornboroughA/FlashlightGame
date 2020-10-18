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

    [SerializeField] private bool isBlocked;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        UpdatePosition();
        CalculateRay();
        UpdateOpacity();


        if (!isBlocked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InstantiatePlatform();
            }
        }        
     }
    
    private void CalculateRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position);
        print(hit.collider);

        if (hit.collider != null)
        {
            isBlocked = false;
        }
        else
        {
            isBlocked = true;
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
        float colorApply = 0f;
        if (!isBlocked)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            distNormalized = Mathf.Clamp((dist / flashLightRange), 0.2f, 1f);
            colorApply = (1 - distNormalized);
        }
        spriteColor = _spriteRenderer.color;
        _spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, colorApply);
    }
    private void InstantiatePlatform()
    {
        GameObject platform = Instantiate(lightPlatform, transform.position, transform.rotation);
        platform.GetComponent<LightPlatform>().StartCountdown(distNormalized);

    }


}
