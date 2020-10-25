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

    [SerializeField] private AudioClip[] lightClickSound;
    private AudioSource _audioSource;


     // have layer order of sprite change in here
    // depending on bool contained in separate script

    private SpriteRenderer _spriteRenderer;
    private Color baseSpriteColor;
    [SerializeField] private Color blockedSpriteColor;

    private float distNormalized;

    [SerializeField] private bool isBlocked;
    [SerializeField] private bool onCooldown;

    public int currentPlatforms;
    [SerializeField] private int maxPlatforms;
    [SerializeField] private float delayBetweenPlatforms;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        baseSpriteColor = _spriteRenderer.color;
    }
    void Update()
    {
        if (Player.flashlight == false)
        {
            _spriteRenderer.sortingOrder = -10;
            return;
        }
        else {

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
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1<<8);
        Debug.DrawLine(transform.position, player.position);

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
        //calculate opacity based on distance
        float dist = Vector2.Distance(transform.position, player.position);
        distNormalized = Mathf.Clamp((dist / flashLightRange), 0.2f, 1f);
        colorApply = (1 - distNormalized);

        if (!isBlocked && !onCooldown && (maxPlatforms >= currentPlatforms))
        {
        _spriteRenderer.color = new Color(baseSpriteColor.r, baseSpriteColor.g, baseSpriteColor.b, colorApply);
        } 
        else
        {
        _spriteRenderer.color = new Color(blockedSpriteColor.r, blockedSpriteColor.g, blockedSpriteColor.b, (colorApply / 2));
        }
        
        
    }
    private void InstantiatePlatform()
    {
        GameObject platform = Instantiate(lightPlatform, transform.position, transform.rotation);
        platform.GetComponent<LightPlatform>().StartCountdown(distNormalized);
        _audioSource.PlayOneShot(lightClickSound[Random.Range(0, 2)]);

        currentPlatforms++;

        //moved this to after platform disappears (called from platform script) so you can't chain infinite platforms, but if we want
        //multiple again, I think we'll need to change it back to this one.
        //StartCoroutine(FlashlightCooldown());
    }

    public void StartCooldown()
    {
        StartCoroutine(FlashlightCooldown());
    }
    private IEnumerator FlashlightCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(delayBetweenPlatforms);
        onCooldown = false;
    }


}
