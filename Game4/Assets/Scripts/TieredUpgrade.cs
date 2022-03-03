using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TieredUpgrade : MonoBehaviour
{
    public int level = 1;
    public int requiredCoal;
    public int requiredMetal;
    public int requiredGem;
    public int requiredGold;
    public int requiredSpeedUp;


    public Text firstUpgradeText;
    public Text secondUpgradeText;
    public Text thirdUpgradeText;
    public Text fourthUpgradeText;

    public Image firstImage;
    public Image secondImage;
    public Image thirdImage;
    public Image fourthImage;

    public Sprite oreSprite;

    public Material metalMat;
    public Material goldMat;
    public Material coalMat;
    public Material gemMat;

    private ResourceManager resourceManager;

    public Color green;
    public Color red;

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();

        requiredCoal = 20;
        requiredGem = 10;
        requiredGold = 1;
        requiredMetal = 5;

        firstImage.sprite = oreSprite;
        secondImage.sprite = oreSprite;
        thirdImage.sprite = oreSprite;
        fourthImage.sprite = oreSprite;

        firstImage.material = coalMat;
        secondImage.material = metalMat;
        thirdImage.material = gemMat;
        fourthImage.material = goldMat;
    }

    // Update is called once per frame
    void Update()
    {
        firstUpgradeText.text = resourceManager.coalAmount.ToString() + " / " + requiredCoal.ToString();
        secondUpgradeText.text = resourceManager.metalAmount.ToString() + " / " + requiredMetal.ToString();
        thirdUpgradeText.text = resourceManager.gemAmount.ToString() + " / " + requiredGem.ToString();
        fourthUpgradeText.text = resourceManager.goldAmount.ToString() + " / " + requiredGold.ToString();

        if(resourceManager.coalAmount > requiredCoal)
        {
            firstUpgradeText.color = green;
        }
        else
        {
            firstUpgradeText.color = red;
        }

        if (resourceManager.metalAmount > requiredMetal)
        {
            secondUpgradeText.color = green;
        }
        else
        {
            secondUpgradeText.color = red;
        }

        if (resourceManager.gemAmount > requiredGem)
        {
            thirdUpgradeText.color = green;
        }
        else
        {
            thirdUpgradeText.color = red;
        }

        if (resourceManager.goldAmount > requiredGold)
        {
            fourthUpgradeText.color = green;
        }
        else
        {
            fourthUpgradeText.color = red;
        }

    }

    public void IncreaseLevel()
    {
        level++;
        requiredCoal *= level;
        requiredGem *= level;
        requiredGold *= level;
        requiredMetal *= level;
    }
}
