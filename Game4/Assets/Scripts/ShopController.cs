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

    public GameObject notAvailableButton1;
    public GameObject availableButton1;
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
            availableButton1.gameObject.SetActive(false);
            notAvailableButton1.gameObject.SetActive(true);
        }

        else
        {
            availableButton1.gameObject.SetActive(true);
            notAvailableButton1.gameObject.SetActive(false);
        }
    }

    public void Upgrade()
    {
        

        for(int i = 0; i < upgradeProgressSprites.Count; i++)
        {
            if(upgradeBar1.sprite == upgradeProgressSprites[i])
            {
                upgradeBar1.sprite = upgradeProgressSprites[i + 1];
                break;
            }
        }

        resourceManager.RemoveGold(upgrade1.cost);
    }
}
