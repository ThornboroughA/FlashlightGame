using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{


    private float lifeSpan;
   
    private SpriteRenderer _spriteRenderer;

     private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        lifeSpan = 3;
      
    }

     void Update()
    {
        print("PLATFORM LIFE:" + lifeSpan);
        
        StartCoroutine(LifeSpanCountdown());
        if (lifeSpan <= 0)
        {
            LightSwitchTemp.lightOnOff = false;  
        }

        if (lifeSpan == 0 && LightSwitchTemp.lightOnOff == false) {

            lifeSpan = 3; 
        }
        // create separate temporary switch file 

       
    }

  
    private IEnumerator LifeSpanCountdown()
    {
        while (lifeSpan > 0)
        {
            yield return new WaitForSeconds(4);

            lifeSpan = 0;
          
        }
    }


}
