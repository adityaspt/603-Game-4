using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    
    public GameObject upgradeCanvas;
    public GameObject itemCanvas;
    public GameObject goldCanvas;
    public GameObject instructionCanvas;
    public GameObject menuCanvas;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
   

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToGame()
    {
        upgradeCanvas.gameObject.SetActive(false);
        itemCanvas.gameObject.SetActive(false);
        goldCanvas.gameObject.SetActive(false);
        
        Time.timeScale = 1;
    }

    public void CloseInstructionCanvas()
    {
        instructionCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenInstructionCanvas()
    {
        menuCanvas.gameObject.SetActive(false);
        instructionCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
}
