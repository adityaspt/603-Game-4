using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    
    public int level = 1;
    public int index;

    public int requiredCoal;
    public int requiredMetal;
    public int requiredGem;
    public int requiredGold;
    public int requiredSpeedUp;

    public Drill drill;
    public playerController playerController;

    
    public Text firstUpgradeText;
    public Text secondUpgradeText;
    public Text thirdUpgradeText;
    public Text fourthUpgradeText;

    public Image firstImage;
    public Image secondImage;
    public Image thirdImage;
    public Image fourthImage;

    public Text speedUpText;
    public Button upgradeButton;
    public Text upgradeButtonText;
    public GameObject progressSlider;
    public Slider slider;
    public Text timeText;
    public GameObject upgradeCompleteButton;
    public Button speedUpButton;

    private Timer timer;

    private ResourceManager resourceManager;

    public Sprite oreSprite;
    public Material coalMat;
    public Material metalMat;
    public Material goldMat;
    public Material gemMat;

    public Color white;
    public Color green;
    public Color red;
    // Start is called before the first frame update
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        timer = FindObjectOfType<Timer>();
        requiredSpeedUp = 2;

        switch (index)
        {
            case 0:
                requiredCoal = 25;
                requiredGem = 15;
                requiredMetal = 10;
                break;
            case 1:
                requiredCoal = 100;
                requiredGem = 80;
                requiredMetal = 80;
                requiredGold = 3;
                break;
            case 2:
                requiredCoal = 25;
                requiredGem = 15;
                requiredMetal = 10;
                break;
            case 3:
                requiredCoal = 100;
                requiredGem = 80;
                requiredMetal = 80;
                requiredGold = 3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (resourceManager.coalAmountStorage < requiredCoal ||
            resourceManager.metalAmountStorage < requiredMetal ||
            resourceManager.gemAmountStorage < requiredGem ||
            resourceManager.goldAmountStorage < requiredGold)
        {
            upgradeButton.interactable = false;
            upgradeButtonText.text = "Not AvAilAble";
        }

        else
        {
            upgradeButton.interactable = true;
            upgradeButtonText.text = "StArt UpgrAde";
        }

        speedUpText.text = "Speed Up \n" + requiredSpeedUp;

        firstImage.sprite = oreSprite;
        firstImage.material = coalMat;
        secondImage.sprite = oreSprite;
        secondImage.material = gemMat;
        thirdImage.sprite = oreSprite;
        thirdImage.material = metalMat;

        firstUpgradeText.text = resourceManager.coalAmountStorage.ToString() + " / " + requiredCoal.ToString();
        secondUpgradeText.text = resourceManager.gemAmountStorage.ToString() + " / " + requiredGem.ToString();
        thirdUpgradeText.text = resourceManager.metalAmountStorage.ToString() + " / " + requiredMetal.ToString();

        if(resourceManager.coalAmountStorage > requiredCoal)
        {
            firstUpgradeText.color = green;
        }
        else
        {
            firstUpgradeText.color = red;
        }

        if (resourceManager.gemAmountStorage > requiredGem)
        {
            secondUpgradeText.color = green;
        }
        else
        {
            secondUpgradeText.color = red;
        }
        if (resourceManager.metalAmountStorage > requiredMetal)
        {
            thirdUpgradeText.color = green;
        }
        else
        {
            thirdUpgradeText.color = red;
        }
        if (resourceManager.goldAmountStorage > requiredGold)
        {
            fourthUpgradeText.color = green;
        }
        else
        {
            fourthUpgradeText.color = red;
        }

        switch (index)
        {
            case 0:

                if (level > 4)
                {
                    fourthImage.color = white;
                    fourthImage.sprite = oreSprite;
                    fourthImage.material = goldMat;
                    fourthUpgradeText.text = resourceManager.goldAmountStorage.ToString() + " / " + requiredGold.ToString();
                }
                break;
            case 1:
                fourthImage.sprite = oreSprite;
                fourthImage.material = goldMat;
                fourthUpgradeText.text = resourceManager.goldAmountStorage.ToString() + " / " + requiredGold.ToString();
                break;
            case 2:

                if (level > 4)
                {
                    fourthImage.color = white;
                    fourthImage.sprite = oreSprite;
                    fourthImage.material = goldMat;
                    fourthUpgradeText.text = resourceManager.goldAmountStorage.ToString() + " / " + requiredGold.ToString();
                }
                break;
            case 3:
                fourthImage.sprite = oreSprite;
                fourthImage.material = goldMat;
                fourthUpgradeText.text = resourceManager.goldAmountStorage.ToString() + " / " + requiredGold.ToString();
                break;



        }

    }

    public void UpgradeStart()
    {
        resourceManager.coalAmountStorage -= requiredCoal;
        resourceManager.metalAmountStorage -= requiredMetal;
        resourceManager.gemAmountStorage -= requiredGem;
        resourceManager.goldAmountStorage -= requiredGold;

        upgradeButton.gameObject.SetActive(false);

        progressSlider.gameObject.SetActive(true);

        speedUpButton.gameObject.SetActive(true);

        switch (index)
        {
            case 0:
                timer.DSPStartTimer(30f * level);
                break;
            case 1:
                timer.DSIStartTimer(30f * level);
                break;
            case 2:
                timer.BCStartTimer(30f * level);
                break;
            case 3:
                timer.BStartTimer(30f * level);
                break;

        }

       

    }

    public void UpgradeComplete()
    {
        resourceManager.doneInt++;
        upgradeCompleteButton.SetActive(true);
        progressSlider.gameObject.SetActive(false);
        speedUpButton.gameObject.SetActive(false);

        switch (index)
        {
            case 0:
                timer.DSPReset();
                break;
            case 1:
                timer.DSIReset();
                break;
            case 2:
                timer.BCReset();
                break;
            case 3:
                timer.BReset();
                break;

        }
    }

    public void PurchaseUpgrade()
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
        resourceManager.doneInt--;
        upgradeCompleteButton.SetActive(false);
        upgradeButton.gameObject.SetActive(true);
        level++;
        requiredSpeedUp = level * 2;
        requiredCoal *= 2;
        requiredGem *= 2;
        requiredMetal *= 2;
        requiredGold *= 2;

        switch (index)
        {
            case 0:
                drill.drillSpeed += 0.25f;
                break;
            case 1:
                drill.UpSize();
                break;
            case 2:
                resourceManager.goldBagCapacity *= 2;
                resourceManager.coalBagCapacity *= 2;
                resourceManager.gemBagCapacity *= 2;
                resourceManager.metalBagCapacity *= 2;
                break;
            case 3:
                playerController.bombRadius += 0.5f;
                break;

        }

    }

    public void SpeedUp()
    {
        if (resourceManager.goldAmountStorage >= requiredSpeedUp)
        {
            UpgradeComplete();
            resourceManager.goldAmountStorage -= requiredSpeedUp;
        }
        /*
        else
        {

            GoldPurchaseCanvas.gameObject.SetActive(true);
        }
        */
    }
}
