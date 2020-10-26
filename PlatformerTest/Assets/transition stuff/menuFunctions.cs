using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuFunctions : MonoBehaviour
{
    // Start is called before the first frame update


    public void startGame() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
               
    }
    public void quit() {

        Application.Quit(); 
    
    }
}
