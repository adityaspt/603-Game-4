using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class uiController : MonoBehaviour
{
    public static uiController uiControllerInstance;

    //Pause menu
    [SerializeField]
    GameObject PauseCanvas, resourceUIParent;

    //resource objects
    [SerializeField]
    GameObject goldText, coalText, metalText, gemText, bagCapacityText;

    //Items objects
    [SerializeField]
    GameObject bombsText, torchesText;

    private void Awake()
    {
        uiControllerInstance = this;
    }

    public void updateResourceAmountTextUI(object sender, eventTriggerSet.resourceEventTrigger e)
    {
        int rtInt = (int)e.resourceType;
        print("updating UI");
        switch (rtInt)
        {
            case 0: //coal
                coalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.coalAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.coalBagCapacity.ToString();
                break;
            case 1: //metal
                metalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.metalAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.metalBagCapacity.ToString();
                break;
            case 2: //gem
                gemText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.gemAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.gemBagCapacity.ToString();
                break;
            case 3: //gold
                goldText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.goldAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.goldBagCapacity.ToString();
                break;

        }
    }

    //new function
    public void updateBagCapacityAmountTextUI(object sender, EventArgs e)
    {
        //bag capacity
        bagCapacityText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.currentBagResourceValue.ToString() + "/" + ResourceManager.resourceManagerInstance.totalBagCapacity.ToString();
    }

    public void updateItemAmountTextUI(object sender, eventTriggerSet.itemEventTrigger e)
    {
        int iTInt = (int)e.itemType;
        print("updating UI item");
        switch (iTInt)
        {
            case 0: //bombs
                bombsText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.bombs.ToString();
                break;
            case 1: //torches
                torchesText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.torches.ToString();
                break;
            default:
                Debug.LogWarning("Items enum not set properly");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.PlayBackgroundMusic();
        //Update all the text at start
        UpdateAllPlayerResourcesUI();
        
        //Update all the Item amount text
        bombsText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.bombs.ToString();
        torchesText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.torches.ToString();

        //Update Bag Capacity amount text in the start
        UpdateCurrentBagCapacity();
       
    }

    public void UpdateCurrentBagCapacity()
    {
        ResourceManager.resourceManagerInstance.currentBagResourceValue = ResourceManager.resourceManagerInstance.coalAmount + ResourceManager.resourceManagerInstance.metalAmount + ResourceManager.resourceManagerInstance.gemAmount + ResourceManager.resourceManagerInstance.goldAmount;
        bagCapacityText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.currentBagResourceValue.ToString() + "/" + ResourceManager.resourceManagerInstance.totalBagCapacity.ToString();
    }

    public void UpdateAllPlayerResourcesUI()
    {
        //Update all the player Resources amount UI text
        coalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.coalAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.coalBagCapacity.ToString();
        metalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.metalAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.metalBagCapacity.ToString();
        gemText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.gemAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.gemBagCapacity.ToString();
        goldText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.goldAmount.ToString(); //+ "/" + ResourceManager.resourceManagerInstance.goldBagCapacity.ToString();
    }


    public void ResumeGame()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        resourceUIParent.SetActive(true);
        PauseCanvas.SetActive(false);

        Time.timeScale = 1;
    }

    public void ExitGameScene()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseCanvas.activeSelf)
        {
            SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
            resourceUIParent.SetActive(false);
            PauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PlayClickSFX()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
    }

}
