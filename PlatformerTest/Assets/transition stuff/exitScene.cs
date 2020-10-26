using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)

        // greater than zero and less than five load next scene 
    {
        if (SceneManager.GetActiveScene().buildIndex < 5)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // greater than level 5, load first scene 

        if (SceneManager.GetActiveScene().buildIndex >= 5) { 

            SceneManager.LoadScene(0);

        }
    }
}

     

