using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiController : MonoBehaviour
{
    public static uiController uiControllerInstance;

    //resource objects
    [SerializeField]
    GameObject goldText, coalText, metalText, gemText;

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
        switch(rtInt)
            {
            case 0: //coal
                coalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.coalAmount.ToString();
                    break;
            case 1: //metal
                metalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.metalAmount.ToString();
                break;
            case 2: //gem
                gemText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.gemAmount.ToString();
                break;
            case 3: //gold
                goldText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.goldAmount.ToString();
                break;
        }
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
        //Update all the text at start
        UpdateAllPlayerResourcesUI();
        //Update all the Item amount text
        bombsText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.bombs.ToString();
        torchesText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.torches.ToString();
    }


    public void UpdateAllPlayerResourcesUI()
    {
        //Update all the player Resources amount UI text
        coalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.coalAmount.ToString();
        metalText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.metalAmount.ToString();
        gemText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.gemAmount.ToString();
        goldText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.goldAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
