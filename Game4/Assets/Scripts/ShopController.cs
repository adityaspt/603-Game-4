using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public List<Sprite> upgradeProgressSprites;

    private ResourceManager resourceManager;

    public Upgrade drillSpeedUpgrade;
    public TieredUpgrade drillSizeUpgrade;
    public Upgrade bagCapacityUpgrade;
    public TieredUpgrade bombUpgrade;

    /*
    public Image drillSizeUpgradeBar;
    public Image bombUpgradeBar;
    */
    
    public Button dspUpgradeButton;
    public Button dsiUpgradeButton;
    public Button bcUpgradeButton;
    public Button bUpgradeButton;


    public Text dspUpgradeButtonText;
    public Text dsiUpgradeButtonText;
    public Text bcUpgradeButtonText;
    public Text bUpgradeButtonText;


    public GameObject dspProgressSlider;
    public GameObject dsiProgressSlider;
    public GameObject bcProgressSlider;
    public GameObject bProgressSlider;


    public Timer timer;
    

    public GameObject dspUpgradeCompleteButton;
    public GameObject dsiUpgradeCompleteButton;
    public GameObject bcUpgradeCompleteButton;
    public GameObject bUpgradeCompleteButton;

    public GameObject dspSpeedUpButton;
    public GameObject dsiSpeedUpButton;
    public GameObject bcSpeedUpButton;
    public GameObject bSpeedUpButton;

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
        if(resourceManager.coalAmount < drillSpeedUpgrade.requiredCoal ||
            resourceManager.metalAmount < drillSpeedUpgrade.requiredMetal ||
            resourceManager.gemAmount < drillSpeedUpgrade.requiredGem ||
            resourceManager.goldAmount < drillSpeedUpgrade.requiredGold)
        {
            dspUpgradeButton.interactable = false;
            dspUpgradeButtonText.text = "Not AvAilAble";
        }

        else
        {
            dspUpgradeButton.interactable = true;
            dspUpgradeButtonText.text = "Buy UpgrAde";           
        }

        if (resourceManager.coalAmount < drillSizeUpgrade.requiredCoal ||
            resourceManager.metalAmount < drillSizeUpgrade.requiredMetal ||
            resourceManager.gemAmount < drillSizeUpgrade.requiredGem ||
            resourceManager.goldAmount < drillSizeUpgrade.requiredGold)
        {
            dsiUpgradeButton.interactable = false;
            dsiUpgradeButtonText.text = "Not AvAilAble";
        }

        else
        {
            dsiUpgradeButton.interactable = true;
            dsiUpgradeButtonText.text = "Buy UpgrAde";
        }

        if (resourceManager.coalAmount < bagCapacityUpgrade.requiredCoal ||
            resourceManager.metalAmount < bagCapacityUpgrade.requiredMetal ||
            resourceManager.gemAmount < bagCapacityUpgrade.requiredGem ||
            resourceManager.goldAmount < bagCapacityUpgrade.requiredGold)
        {
            bcUpgradeButton.interactable = false;
            bcUpgradeButtonText.text = "Not AvAilAble";
        }

        else
        {
            bcUpgradeButton.interactable = true;
            bcUpgradeButtonText.text = "Buy UpgrAde";
        }

        if (resourceManager.coalAmount < bombUpgrade.requiredCoal ||
            resourceManager.metalAmount < bombUpgrade.requiredMetal ||
            resourceManager.gemAmount < bombUpgrade.requiredGem ||
            resourceManager.goldAmount < bombUpgrade.requiredGold)
        {
            bUpgradeButton.interactable = false;
            bUpgradeButtonText.text = "Not AvAilAble";
        }

        else
        {
            bUpgradeButton.interactable = true;
            bUpgradeButtonText.text = "Buy UpgrAde";
        }


    }

    public void DSPUpgradeStart()
    {
        resourceManager.coalAmount -= drillSpeedUpgrade.requiredCoal;
        resourceManager.metalAmount -= drillSpeedUpgrade.requiredMetal;
        resourceManager.gemAmount -= drillSpeedUpgrade.requiredGem;
        resourceManager.goldAmount -= drillSpeedUpgrade.requiredGold;

        dspUpgradeButton.gameObject.SetActive(false);
        
        dspProgressSlider.gameObject.SetActive(true);
        dspSpeedUpButton.gameObject.SetActive(true);


        timer.DSPStartTimer(30f);
               
    }

    public void DSPUpgradeComplete()
    {
        dspUpgradeCompleteButton.SetActive(true);
        dspProgressSlider.gameObject.SetActive(false);
        dspSpeedUpButton.gameObject.SetActive(false);
        timer.DSPReset();
    }

    public void DSPPurchaseUpgrade()
    {
        /*
        for (int i = 0; i < upgradeProgressSprites.Count; i++)
        {
            if(upgradeBar1.sprite == upgradeProgressSprites[i])
            {
                upgradeBar1.sprite = upgradeProgressSprites[i + 1];
                break;
            }
        }
        */
        dspUpgradeCompleteButton.SetActive(false);
        dspUpgradeButton.gameObject.SetActive(true);

    }

    public void DSPSpeedUp()
    {
        if(resourceManager.goldAmount >= drillSpeedUpgrade.requiredSpeedUp)
        {
            DSPUpgradeComplete();
            resourceManager.goldAmount -= drillSpeedUpgrade.requiredSpeedUp;
        }

        else
        {

            GoldPurchaseCanvas.gameObject.SetActive(true);
        }
    }

    public void DSIUpgradeStart()
    {
        resourceManager.coalAmount -= drillSizeUpgrade.requiredCoal;
        resourceManager.metalAmount -= drillSizeUpgrade.requiredMetal;
        resourceManager.gemAmount -= drillSizeUpgrade.requiredGem;
        resourceManager.goldAmount -= drillSizeUpgrade.requiredGold;

        dsiUpgradeButton.gameObject.SetActive(false);

        dsiProgressSlider.gameObject.SetActive(true);
        dsiSpeedUpButton.gameObject.SetActive(true);


        timer.DSIStartTimer(30f);

    }

    public void DSIUpgradeComplete()
    {
        drillSizeUpgrade.IncreaseLevel();
        dsiUpgradeCompleteButton.SetActive(true);
        dsiProgressSlider.gameObject.SetActive(false);
        dsiSpeedUpButton.gameObject.SetActive(false);
        timer.DSIReset();
    }

    public void DSIPurchaseUpgrade()
    {
        /*
        for (int i = 0; i < upgradeProgressSprites.Count; i++)
        {
            if(upgradeBar1.sprite == upgradeProgressSprites[i])
            {
                upgradeBar1.sprite = upgradeProgressSprites[i + 1];
                break;
            }
        }
        */
        dsiUpgradeCompleteButton.SetActive(false);
        dsiUpgradeButton.gameObject.SetActive(true);

    }

    public void DSISpeedUp()
    {
        if (resourceManager.goldAmount >= drillSizeUpgrade.requiredSpeedUp)
        {
            DSIUpgradeComplete();
            resourceManager.goldAmount -= drillSizeUpgrade.requiredSpeedUp;
        }

        else
        {

            GoldPurchaseCanvas.gameObject.SetActive(true);
        }
    }

    public void BCUpgradeStart()
    {
        resourceManager.coalAmount -= bagCapacityUpgrade.requiredCoal;
        resourceManager.metalAmount -= bagCapacityUpgrade.requiredMetal;
        resourceManager.gemAmount -= bagCapacityUpgrade.requiredGem;
        resourceManager.goldAmount -= bagCapacityUpgrade.requiredGold;

        bcUpgradeButton.gameObject.SetActive(false);

        bcProgressSlider.gameObject.SetActive(true);
        bcSpeedUpButton.gameObject.SetActive(true);


        timer.BCStartTimer(30f);

    }

    public void BCUpgradeComplete()
    {
        bcUpgradeCompleteButton.SetActive(true);
        bcProgressSlider.gameObject.SetActive(false);
        bcSpeedUpButton.gameObject.SetActive(false);
        timer.BCReset();
    }

    public void BCPurchaseUpgrade()
    {
        /*
        for (int i = 0; i < upgradeProgressSprites.Count; i++)
        {
            if(upgradeBar1.sprite == upgradeProgressSprites[i])
            {
                upgradeBar1.sprite = upgradeProgressSprites[i + 1];
                break;
            }
        }
        */
        bcUpgradeCompleteButton.SetActive(false);
        bcUpgradeButton.gameObject.SetActive(true);

    }

    public void BCSpeedUp()
    {
        if (resourceManager.goldAmount >= bagCapacityUpgrade.requiredSpeedUp)
        {
            BCUpgradeComplete();
            resourceManager.goldAmount -= bagCapacityUpgrade.requiredSpeedUp;
        }

        else
        {

            GoldPurchaseCanvas.gameObject.SetActive(true);
        }
    }

    public void BUpgradeStart()
    {
        resourceManager.coalAmount -= bombUpgrade.requiredCoal;
        resourceManager.metalAmount -= bombUpgrade.requiredMetal;
        resourceManager.gemAmount -= bombUpgrade.requiredGem;
        resourceManager.goldAmount -= bombUpgrade.requiredGold;

        bUpgradeButton.gameObject.SetActive(false);

        bProgressSlider.gameObject.SetActive(true);
        bSpeedUpButton.gameObject.SetActive(true);


        timer.BStartTimer(30f);

    }

    public void BUpgradeComplete()
    {
        bombUpgrade.IncreaseLevel();
        bUpgradeCompleteButton.SetActive(true);
        bProgressSlider.gameObject.SetActive(false);
        bSpeedUpButton.gameObject.SetActive(false);
        timer.BReset();
    }

    public void BPurchaseUpgrade()
    {
        /*
        for (int i = 0; i < upgradeProgressSprites.Count; i++)
        {
            if(upgradeBar1.sprite == upgradeProgressSprites[i])
            {
                upgradeBar1.sprite = upgradeProgressSprites[i + 1];
                break;
            }
        }
        */
        bUpgradeCompleteButton.SetActive(false);
        bUpgradeButton.gameObject.SetActive(true);

    }

    public void BSpeedUp()
    {
        if (resourceManager.goldAmount >= bombUpgrade.requiredSpeedUp)
        {
            BUpgradeComplete();
            resourceManager.goldAmount -= bombUpgrade.requiredSpeedUp;
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
