using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public int goldAmount;
    public int premiumCurrencyAmount;

    // Start is called before the first frame update
    void Start()
    {
        goldAmount = 5;
        premiumCurrencyAmount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
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
