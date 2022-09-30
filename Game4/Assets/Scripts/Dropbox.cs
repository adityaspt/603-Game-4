using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dropbox : MonoBehaviour
{
    [SerializeField]
    GameObject goldAmountText, coalAmountText, metalAmountText, gemsAmountText;

    [SerializeField]
    Canvas dropBoxCanvas;

    public Animator animator;
    private void Awake()
    {
       
    }


    // Start is called before the first frame update
    void Start()
    {
        updateStorageAmountUI();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDropBoxCanvas()
    {
        dropBoxCanvas.gameObject.SetActive(true);
    }

    public void HideDropBoxCanvas()
    {
        dropBoxCanvas.gameObject.SetActive(false);
    }

    public void TransferResourcesFromPlayerToStorage()
    {
        if (ResourceManager.resourceManagerInstance.gemAmount > 0)
        {
            ResourceManager.resourceManagerInstance.gemAmountStorage += ResourceManager.resourceManagerInstance.gemAmount;
            ResourceManager.resourceManagerInstance.gemAmount = 0;
        }
        if (ResourceManager.resourceManagerInstance.goldAmount > 0)
        {
            ResourceManager.resourceManagerInstance.goldAmountStorage += ResourceManager.resourceManagerInstance.goldAmount;
            ResourceManager.resourceManagerInstance.goldAmount = 0;
        }
        if (ResourceManager.resourceManagerInstance.metalAmount > 0)
        {
            ResourceManager.resourceManagerInstance.metalAmountStorage += ResourceManager.resourceManagerInstance.metalAmount;
            ResourceManager.resourceManagerInstance.metalAmount = 0;
        }
        if (ResourceManager.resourceManagerInstance.coalAmount > 0)
        {
            ResourceManager.resourceManagerInstance.coalAmountStorage += ResourceManager.resourceManagerInstance.coalAmount;
            ResourceManager.resourceManagerInstance.coalAmount = 0;
        }

        ResourceManager.resourceManagerInstance.currentBagCapacity = 0;

        uiController.uiControllerInstance.UpdateCurrentBagCapacity();

        updateStorageAmountUI();
        uiController.uiControllerInstance.UpdateAllPlayerResourcesUI();
        print("Transfer function");
    }

    public void updateStorageAmountUI()
    {
        print("updating storage resource amount UI");

        coalAmountText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.coalAmountStorage.ToString();

        metalAmountText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.metalAmountStorage.ToString();

        gemsAmountText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.gemAmountStorage.ToString();

        goldAmountText.GetComponent<TextMeshProUGUI>().text = ResourceManager.resourceManagerInstance.goldAmountStorage.ToString();
    }

}
