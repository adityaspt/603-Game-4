using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public static playerController localController;

    [Header("Player Variables")]
    public Sprite[] playerStyles;

    [Header("Player Variables")]
    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb2d;

    private Vector2 movement;

    [SerializeField]
    GameObject playerPopUpText;

    [SerializeField]
    float popUpTextTime;

    
    float popUpTimeCounter;

    [Header("Pause UI")]
    public GameObject pauseCanvas;

    //public int bombs;
    //public int lights;

    [Header("Prefab references")]
    public GameObject bombPrefab;
    public GameObject lightPrefab;

    //dropbox
    [Header("Dropbox/Storage variables")]
    [SerializeField]
    private bool isTouchingDropbox = false;
    [SerializeField]
    private Dropbox dropBoxReference;

    [Header("Shop")]
    [SerializeField]
    public bool isTouchingShop;
    public GameObject shopCanvas;
    public GameObject itemCanvas;
    public GameObject goldCanvas;
    public GameObject confirmCanvas;

    [Header("Compass")]
    public GameObject compass;
    public bool compassActive;

    [Header("Script References")]
    [SerializeField]
    ResourceManager resourceManager;

    [SerializeField] public GameObject bot;
    [SerializeField] private float botNumber = 5;

    public Slider drillSlider;
    public float drillChargingSpeed = 20.0f;

    public float bombRadius;

    private PhotonView PV;

    public void changeStylesMultiplayer(int i)
    {
        PV.RPC("changeStyle", RpcTarget.AllBuffered, i);
    }

    [PunRPC]
    void changeStyle(int i)
    {
        GetComponent<SpriteRenderer>().sprite = playerStyles[i];
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
            localController = this;
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<Drill>().enabled = false;
            transform.GetChild(0).GetChild(0).GetComponent<drillRotation>().enabled = false;
            rb2d.simulated = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = PlayerInfo.instance.pauseCanvas;
        dropBoxReference = PlayerInfo.instance.dropBoxReference;
        shopCanvas = PlayerInfo.instance.shopCanvas;
        itemCanvas = PlayerInfo.instance.itemCanvas;
        goldCanvas = PlayerInfo.instance.goldCanvas;
        confirmCanvas = PlayerInfo.instance.confirmCanvas;
        compass = PlayerInfo.instance.compass;
        resourceManager = PlayerInfo.instance.resourceManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;

        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;


        //Check for Menu button
        if (Input.GetKeyDown(KeyCode.M))
        {
            pauseCanvas.gameObject.SetActive(true);
            //Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (compassActive)
            {
                compassActive = false;
                compass.gameObject.SetActive(false);
            }
            else
            {
                compassActive = true;
                compass.gameObject.SetActive(true);
            }

        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            if (botNumber > 0)
            {
                Instantiate(bot, transform.position, Quaternion.identity);
                botNumber--;
            }
        }

        // Bomb key
        if (Input.GetKeyDown(KeyCode.Q) && ResourceManager.resourceManagerInstance.bombs > 0)
        {
            ResourceManager.resourceManagerInstance.UseABomb();
            bomb b = Instantiate(bombPrefab, transform.position, Quaternion.identity).GetComponent<bomb>();
            b.radius = bombRadius;
        }

        // Light key
        if (Input.GetKeyDown(KeyCode.E) && ResourceManager.resourceManagerInstance.torches > 0)
        {
            ResourceManager.resourceManagerInstance.UseATorch();
            Instantiate(lightPrefab, transform.position, Quaternion.identity);
        }

        if (isTouchingDropbox && Input.GetKeyDown(KeyCode.F))
        {
            dropBoxReference.TransferResourcesFromPlayerToStorage();
            SoundManager.PlaySound(SoundManager.Sounds.resourceDropSFX);
            isTouchingDropbox = false;
        }

        if (isTouchingShop && Input.GetKeyDown(KeyCode.F))
        {
            //Time.timeScale = 0;
            shopCanvas.gameObject.SetActive(true);
        }

        if (setPopUpText)
        {
            popUpTimeCounter +=Time.deltaTime;
            if (popUpTimeCounter >= popUpTextTime)
            {
                ClosePopUpText();
            }
        }

        
       
    }


    void ClosePopUpText()
    {
        popUpTimeCounter = 0;
        setPopUpText = false;
        playerPopUpText.gameObject.SetActive(false);
        print("Pop up text false");
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        //Movement
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    bool setPopUpText = false;
    void popUpText()
    {
        popUpTimeCounter = 0;
        playerPopUpText.gameObject.SetActive(true);
        setPopUpText = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PV.IsMine)
            return;

        if (collision.gameObject.CompareTag("DropBox"))
        {
            // dropBoxReference = collision.gameObject.GetComponent<Dropbox>();
            isTouchingDropbox = true;
            dropBoxReference.ShowDropBoxCanvas();
            dropBoxReference.animator.SetBool("IsOpen", isTouchingDropbox);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!PV.IsMine)
            return;

        if (collision.gameObject.CompareTag("Gold"))
        {
            if (resourceManager.currentBagCapacity < resourceManager.totalBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gold);
                ResourceManager.resourceManagerInstance.updateCurrentBagCapacity();
                SoundManager.PlaySound(SoundManager.Sounds.resourceCollectSFX);
                Destroy(collision.gameObject);
            }
            else
            {
                popUpText();
            }
        }
        if (collision.gameObject.CompareTag("Coal"))
        {
            if (resourceManager.currentBagCapacity < resourceManager.totalBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.coal);
                ResourceManager.resourceManagerInstance.updateCurrentBagCapacity();
                SoundManager.PlaySound(SoundManager.Sounds.resourceCollectSFX);
                Destroy(collision.gameObject);
            }
            else
            {
                popUpText();
            }
        }
        if (collision.gameObject.CompareTag("Metal"))
        {
            if (resourceManager.currentBagCapacity < resourceManager.totalBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.metal);
                ResourceManager.resourceManagerInstance.updateCurrentBagCapacity();
                SoundManager.PlaySound(SoundManager.Sounds.resourceCollectSFX);
                Destroy(collision.gameObject);
            }
            else
            {
                popUpText();
            }
        }
        if (collision.gameObject.CompareTag("Gems"))
        {
            if (resourceManager.currentBagCapacity < resourceManager.totalBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gem);
                ResourceManager.resourceManagerInstance.updateCurrentBagCapacity();
                SoundManager.PlaySound(SoundManager.Sounds.resourceCollectSFX);
                Destroy(collision.gameObject);
            }
            else
            {
                popUpText();
            }
        }
        
        if (collision.gameObject.CompareTag("RechargingStation"))
        {
            drillSlider.value += drillChargingSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!PV.IsMine)
            return;

        if (collision.gameObject.CompareTag("DropBox"))
        {
            isTouchingDropbox = false;
            dropBoxReference.HideDropBoxCanvas();
            dropBoxReference.animator.SetBool("IsOpen", isTouchingDropbox);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PV.IsMine)
            return;

        if (collision.gameObject.CompareTag("Shop"))
        {
            isTouchingShop = true;
        }
        
 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!PV.IsMine)
            return;

        if (collision.gameObject.CompareTag("Shop"))
        {
            isTouchingShop = false;
        }

    }
}
