using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager resourceManagerInstance;
    [Header("Player Resources amounts")]
    public int goldAmount;

    public int premiumCurrencyAmount;

    //Resources
    public int coalAmount = 0, metalAmount = 0, gemAmount = 0;

    [Header("Storage Resources amounts")]
    public int coalAmountStorage = 0, metalAmountStorage = 0, gemAmountStorage = 0, goldAmountStorage = 0;

    public enum ResourceType
    {
        coal, metal, gem, gold
    };
    public static ResourceType resourceType;

    public event EventHandler<eventTriggerSet.resourceEventTrigger> onResourceAmountChanged;

    [Header("Player Items amounts")]
    //Items
    public int bombs = 0, torches = 0;

    public enum ItemType
    {
        bomb, torches
    };
    public static ResourceType itemType;

    public event EventHandler<eventTriggerSet.itemEventTrigger> onItemAmountChanged;


    //Increase these variables on bag capacity upgrades
    [Header("Bag capacity stuff")]
    public int goldBagCapacity, coalBagCapacity, metalBagCapacity, gemBagCapacity;

    [Header("Done Object")]
    public GameObject doneObject;
    public int doneInt;
    private void Awake()
    {
        resourceManagerInstance = this;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        onResourceAmountChanged -= uiController.uiControllerInstance.updateResourceAmountTextUI;
        onItemAmountChanged -= uiController.uiControllerInstance.updateItemAmountTextUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        goldAmount = 5;
        premiumCurrencyAmount = 3;
        onResourceAmountChanged += uiController.uiControllerInstance.updateResourceAmountTextUI;
        onItemAmountChanged += uiController.uiControllerInstance.updateItemAmountTextUI;
    }

    // Update is called once per frame
    void Update()
    {
        //Testing events
        //if (Input.GetMouseButtonDown(1))
        //{
        //    AddResourceAmount(ResourceType.coal);
        //}

        if(doneInt > 0)
        {
            doneObject.gameObject.SetActive(true);
        }
        else
        {
            doneObject.gameObject.SetActive(false);
        }
    }

    //Add Resources
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
        onResourceAmountChanged?.Invoke(this, new eventTriggerSet.resourceEventTrigger { resourceType = rt });
    }
    //


    //Add Items
    public void updateItemAmount(ItemType iT, bool isAdding, int number)
    {
        int itemTypeInt = (int)iT;
        switch (itemTypeInt)
        {
            case 0:
                if (isAdding)
                    bombs+=number;
                else
                    bombs-=number;
                break;
            case 1:
                if (isAdding)
                    torches+=number;
                else
                    torches-=number;
                break;
            default:
                Debug.LogWarning("Items enum not set properly");
                break;
        }
        onItemAmountChanged?.Invoke(this, new eventTriggerSet.itemEventTrigger { itemType = iT });
    }
    //

    //For Zach to use in Shop for item purchase
    public int GetBombAmount()
    {
        return bombs;
    }
    public int GetTorchesAmount()
    {
        return torches;
    }


    public void AddBomb(int amount)
    {
        updateItemAmount(ItemType.bomb, true,amount);
    }

    public void AddTorch(int amount)
    {
        updateItemAmount(ItemType.torches, true,amount);
    }

    public void UseABomb() //Call in playercontroller
    {
        updateItemAmount(ItemType.bomb, false,1);
    }

    public void UseATorch() //Call in playercontroller
    {
        updateItemAmount(ItemType.torches, false,1);
    }

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
