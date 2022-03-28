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
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        SceneManager.LoadScene(1);
    }
   

    public void ExitGame()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        Application.Quit();
    }

    public void ReturnToGame()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        upgradeCanvas.gameObject.SetActive(false);
        itemCanvas.gameObject.SetActive(false);
        goldCanvas.gameObject.SetActive(false);
        
        Time.timeScale = 1;
    }

    public void CloseInstructionCanvas()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        instructionCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenInstructionCanvas()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        menuCanvas.gameObject.SetActive(false);
        instructionCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
}
