using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiController : MonoBehaviour
{
    public static uiController uiControllerInstance;


    [SerializeField]
    GameObject goldText, coalText, metalText, gemText;

    private void Awake()
    {
        uiControllerInstance = this;
    }

    public void updateAmountTextUI(object sender, eventTriggerSet.eventTrigger e)
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


    // Start is called before the first frame update
    void Start()
    {
        //Update all the text at start
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
