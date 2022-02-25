using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public List<Sprite> upgradeProgressSprites;

    private ResourceManager resourceManager;

    public Upgrade upgrade1;

    public Image upgradeBar1;

    
    public Button button1;
    public Text buttonText1;
    public GameObject progressSlider1;
    public Timer progressTimer1;
    public GameObject upgradeCompleteButton;
    public GameObject speedUpButton;
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
        if(resourceManager.GetGoldAmount() < upgrade1.cost)
        {
            button1.interactable = false;
            buttonText1.text = "Not AvAilAble";
        }

        else
        {
            button1.interactable = true;
            buttonText1.text = "Buy UpgrAde";
            
        }
    }

    public void UpgradeStart()
    {
        resourceManager.RemoveGold(upgrade1.cost);
        button1.gameObject.SetActive(false);
        
        progressSlider1.gameObject.SetActive(true);
        speedUpButton.gameObject.SetActive(true);


        progressTimer1.StartTimer(30f);
        
        
    }

    public void UpgradeComplete()
    {
        upgradeCompleteButton.SetActive(true);
        progressSlider1.gameObject.SetActive(false);
        speedUpButton.gameObject.SetActive(false);
        progressTimer1.Reset();
    }

    public void PurchaseUpgrade()
    {
        
        for (int i = 0; i < upgradeProgressSprites.Count; i++)
        {
            if(upgradeBar1.sprite == upgradeProgressSprites[i])
            {
                upgradeBar1.sprite = upgradeProgressSprites[i + 1];
                break;
            }
        }

        upgradeCompleteButton.SetActive(false);
        button1.gameObject.SetActive(true);

    }

    public void SpeedUp()
    {
        if(resourceManager.GetPremiumCurrencyAmount() >= 2)
        {
            UpgradeComplete();
            resourceManager.RemovePremiumCurrency(2);
        }

        else
        {

            GoldPurchaseCanvas.gameObject.SetActive(true);
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


}
