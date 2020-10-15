﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlatform : MonoBehaviour
{


    private float lifeSpan = 1f;
    [SerializeField] private float fadeRate = 0.02f;
    private float lifeMultiplier = 2f;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartCountdown(float distValue)
    {
        lifeSpan = ((1 - distValue) * lifeMultiplier);
        StartCoroutine(LifeSpanCountdown());
    }

    private void Update()
    {
        UpdateOpacity();
        

        if (lifeSpan < 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateOpacity()
    {
        Color spriteColor = _spriteRenderer.color;

        float colorApply = ((lifeSpan / lifeMultiplier));

        _spriteRenderer.color = new Color(spriteColor.r, spriteColor.b, spriteColor.g, colorApply);

    }

    private IEnumerator LifeSpanCountdown()
    {
        while (lifeSpan > 0)
        {
            yield return new WaitForSeconds(0.1f);

            lifeSpan -= fadeRate;
            print(lifeSpan);
        }
    }


}
