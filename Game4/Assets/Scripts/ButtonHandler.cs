using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject upgradeCanvas;
    public GameObject itemCanvas;
    public GameObject goldCanvas;


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ClosePauseCanvas()
    {
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenShop()
    {
        pauseCanvas.gameObject.SetActive(false);
        upgradeCanvas.gameObject.SetActive(true);
    }

    public void ReturnToGame()
    {
        upgradeCanvas.gameObject.SetActive(false);
        itemCanvas.gameObject.SetActive(false);
        goldCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
}
