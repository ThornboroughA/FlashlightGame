using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    /// <summary>
    /// Code for the empty that follows the mouse cursor. Also instantiates platforms (simulating the flashlight)
    /// </summary>

    Vector3 mousePos;
    [SerializeField] private Transform player;
    [SerializeField] private float flashLightRange;
    
    [SerializeField] private GameObject lightPlatform;


     // have layer order of sprite change in here
    // depending on bool contained in separate script

    private SpriteRenderer _spriteRenderer;
    private Color spriteColor;

    private float distNormalized;

    [SerializeField] private bool isBlocked;
    [SerializeField] private bool onCooldown;

    public int currentPlatforms;
    [SerializeField] private int maxPlatforms;
    [SerializeField] private float delayBetweenPlatforms;

    private void Start()
    {

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Player.flashlight == false)
        {
            print("false");
            _spriteRenderer.sortingOrder = -2;
            return;
        }
        else {

            print("flashlight arrived!");

        _spriteRenderer.sortingOrder = 2;
        UpdatePosition();
        CalculateRay();
        UpdateOpacity();

       
        if (!isBlocked && !onCooldown && (maxPlatforms >= currentPlatforms))
        {
            if (Input.GetMouseButtonDown(0))
            {
                InstantiatePlatform();
            }
        }
        }
    }
    
    private void CalculateRay()
    {

        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position);
        //Debug.DrawRay2D(transform.position, player.position); 

        

        if (hit.collider != null)
        {
            isBlocked = false;
        }
        else
        {
            isBlocked = true;
        }*/


        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1<<8);
        Debug.DrawLine(transform.position, player.position);
        print(hit.collider);


        if (hit == false)
        {
            isBlocked = false;
        } else
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
        if (!isBlocked && !onCooldown)
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
        currentPlatforms++;
        StartCoroutine(FlashlightCooldown());
    }
    private IEnumerator FlashlightCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(delayBetweenPlatforms);
        onCooldown = false;
    }


}
