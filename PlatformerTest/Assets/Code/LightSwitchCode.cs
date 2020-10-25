using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchCode : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject lightPlatform;
    public bool lightOnOff;
    void Start()
    {
         lightOnOff = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOnOff == true)
        {
            Debug.Log("active");
            lightPlatform.SetActive(true);
        }
        else {
            Debug.Log("inactive");
            lightPlatform.SetActive(false);
        }


    }

   void OnTriggerEnter2D(Collider2D collision)
    {           
            lightOnOff = !lightOnOff;
    }


}
