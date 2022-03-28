using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPurchaseController : MonoBehaviour
{
    public Button confirmButton;
    public GameObject confirmCanvas;
    public Text confirmText;
    private ResourceManager resourceManager;
    // Start is called before the first frame update
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTenGold()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        confirmText.text = "Are you sure you would like to purchAse 10 gold for $0.99";
        confirmButton.onClick.AddListener(delegate { AddGold(10); });
        confirmCanvas.gameObject.SetActive(true);
    }

    public void BuyFiftyGold()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        confirmText.text = "Are you sure you would like to purchAse 50 gold for $3.99";
        confirmButton.onClick.AddListener(delegate { AddGold(50); });
        confirmCanvas.gameObject.SetActive(true);
    }

    public void BuyHundredGold()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        confirmText.text = "Are you sure you would like to purchAse 100 gold for $6.99";
        confirmButton.onClick.AddListener(delegate { AddGold(100); });
        confirmCanvas.gameObject.SetActive(true);
    }

    public void BuyFiveHundredGold()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        confirmText.text = "Are you sure you would like to purchAse 500 gold for $29.99";
        confirmButton.onClick.AddListener(delegate { AddGold(500); });
        confirmCanvas.gameObject.SetActive(true);
    }



    void AddGold(int gold)
    {
        SoundManager.PlaySound(SoundManager.Sounds.premiumCoinBuySFX);
        resourceManager.goldAmountStorage +=(gold);
        confirmCanvas.gameObject.SetActive(false);
    }

    public void CancelPurchase()
    {
        SoundManager.PlaySound(SoundManager.Sounds.clickSFX);
        confirmCanvas.gameObject.SetActive(false);
    }

}
