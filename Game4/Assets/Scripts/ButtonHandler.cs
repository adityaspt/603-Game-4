using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    
    public GameObject upgradeCanvas;
    public GameObject itemCanvas;
    public GameObject goldCanvas;


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
   

    

    public void ReturnToGame()
    {
        upgradeCanvas.gameObject.SetActive(false);
        itemCanvas.gameObject.SetActive(false);
        goldCanvas.gameObject.SetActive(false);
        
        Time.timeScale = 1;
    }
    
}
