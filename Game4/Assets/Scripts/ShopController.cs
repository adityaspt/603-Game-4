using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public List<Sprite> upgradeProgressSprites;

    private ResourceManager resourceManager;

    public Upgrade drillSpeedUpgrade;
    public Upgrade drillSizeUpgrade;
    public Upgrade bagCapacityUpgrade;
    public Upgrade bombUpgrade;

    public Button oneBomb;
    public Button fiveBomb;
    public Button oneTorch;
    public Button fiveTorch;

    public GameObject GoldPurchaseCanvas;
    public GameObject ItemPurchaseCanvas;
    public GameObject UpgradeCanvas;

    
    // Start is called before the first frame update
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (resourceManager.coalAmountStorage < 10)
        {
            oneBomb.interactable = false;
            oneTorch.interactable = false;
        }
        else
        {
            oneBomb.interactable = true;
            oneTorch.interactable = true;
        }

        if (resourceManager.coalAmountStorage < 50)
        {
            fiveBomb.interactable = false;
            fiveTorch.interactable = false;
        }
        else
        {
            fiveBomb.interactable = true;
            fiveTorch.interactable = true;
        }
    }

    public void UpgradeTab()
    {
        GoldPurchaseCanvas.gameObject.SetActive(false);
        ItemPurchaseCanvas.gameObject.SetActive(false);
        UpgradeCanvas.gameObject.SetActive(true);
    }

    public void ItemTab()
    {
        GoldPurchaseCanvas.gameObject.SetActive(false);
        ItemPurchaseCanvas.gameObject.SetActive(true);
        UpgradeCanvas.gameObject.SetActive(false);
    }

    public void GoldTab()
    {
        GoldPurchaseCanvas.gameObject.SetActive(true);
        ItemPurchaseCanvas.gameObject.SetActive(false);
        UpgradeCanvas.gameObject.SetActive(false);
    }

    public void BuyOneBomb()
    {
        resourceManager.coalAmountStorage -= 10;
        resourceManager.AddBomb(1);
    }

    public void BuyOneTorch()
    {
        resourceManager.coalAmountStorage -= 10;
        resourceManager.AddTorch(1);
    }

    public void BuyFiveBomb()
    {
        resourceManager.coalAmountStorage -= 50;
        resourceManager.AddBomb(5);
    }

    public void BuyFiveTorch()
    {
        resourceManager.coalAmountStorage -= 50;
        resourceManager.AddTorch(5);
    }




}
