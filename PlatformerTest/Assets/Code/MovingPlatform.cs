using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float distance = 5;
    private float startPosition;
    private float startPositionY;
    [SerializeField] private bool moveVertically = false;

    [SerializeField] private bool canBeStoppedByFlashlight = true;
    private bool flashlightHit = false;

    private float localTimer = 0f;
    private bool activePermPlatform = false;

    private void Start()
    {
        startPosition = transform.position.x;
        startPositionY = transform.position.y;
        StartCoroutine(TimerMethod());

    }


    private void Update()
    {
        if (!flashlightHit)
        {
            MovePlatform();
        }  
    }

    private void MovePlatform()
    { 
        Vector2 newPosition = transform.position;

        if (!moveVertically)
        {
            newPosition.x = Mathf.SmoothStep(startPosition, startPosition + distance, Mathf.PingPong(localTimer * speed, 1));
        } else
        {
            newPosition.y = Mathf.SmoothStep(startPositionY, startPositionY + distance, Mathf.PingPong(localTimer * speed, 1));
        }
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        } 
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LightPlatform>())
        {
            LightPlatform lightPlatform = collision.gameObject.GetComponent<LightPlatform>();

            float platformTime = lightPlatform.lifeSpan;
            float timeRate = lightPlatform.fadeRate;
            activePermPlatform = lightPlatform.isPermanent;

            if (canBeStoppedByFlashlight)
            {
                flashlightHit = true;

                if (!activePermPlatform)
                {
                    StartCoroutine(RestartTimer(platformTime, timeRate));
                }
            }

        }
    }

    private IEnumerator RestartTimer(float platformTime, float timeRate)
    {
        while (platformTime > 0)
        {
            yield return new WaitForSeconds(0.1f);

            platformTime -= timeRate;
        }
        flashlightHit = false;
    }

    //for when lightswitch platforms are turned off
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("trigger exit hit");

        if (collision.gameObject.GetComponent<LightPlatform>() && activePermPlatform)
        {
            flashlightHit = false;
        }
    }


    //a local timer for this script -- if using time.time, when the platform restarts it jumps around based on the entire game's timer.
    private IEnumerator TimerMethod()
    {
        while (true)
        {
            while (!flashlightHit)
            {

                yield return new WaitForSeconds(0.015f);
                localTimer += (0.015f);
            }
            yield return null;
        }
    }



}
