using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager resourceManagerInstance;
    


    public int goldAmount;
    public int premiumCurrencyAmount;

    //Resources
    public int coalAmount = 0, metalAmount = 0, gemAmount = 0;
    //

    public event EventHandler<eventTriggerSet.eventTrigger> onAmountChanged;

    

    public  enum ResourceType
    {
        coal, metal, gem, gold
    };
    public static ResourceType resourceType;

    private void Awake()
    {
        resourceManagerInstance = this;
    }
    private void OnEnable()
    {
      
    }
    private void OnDisable()
    {
        onAmountChanged -= uiController.uiControllerInstance.updateAmountTextUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        goldAmount = 5;
        premiumCurrencyAmount = 3;
        onAmountChanged += uiController.uiControllerInstance.updateAmountTextUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AddResourceAmount(ResourceType.coal);
        }
    }

    //Metal
    public void AddResourceAmount(ResourceType rt)
    {
        int resourceTypeInt = (int)rt;
        switch (resourceTypeInt)
        {
            case 0:
                coalAmount++;
                break;
            case 1:
                metalAmount++;
                break;
            case 2:
                gemAmount++;
                break;
            case 4:
                goldAmount++;
                break;
        }
        onAmountChanged?.Invoke(this, new eventTriggerSet.eventTrigger { resourceType = rt });
    }
    //


    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public int GetPremiumCurrencyAmount()
    {
        return premiumCurrencyAmount;
    }

    public void AddGold(int amount)
    {
        goldAmount += amount;
    }

    public void AddPremiumCurrency(int amount)
    {
        premiumCurrencyAmount += amount;
    }

    public void RemoveGold(int amount)
    {
        goldAmount -= amount;
    }

    public void RemovePremiumCurrency(int amount)
    {
        premiumCurrencyAmount -= amount;
    }
}
