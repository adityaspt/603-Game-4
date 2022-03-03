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


}
