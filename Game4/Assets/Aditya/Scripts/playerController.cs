using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{


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

    [Header("Script References")]
    [SerializeField]
    ResourceManager resourceManager;

    public float bombRadius;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            Time.timeScale = 0;
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
            isTouchingDropbox = false;
        }

        if (setPopUpText)
        {
            popUpTimeCounter +=Time.deltaTime;
            if (popUpTimeCounter >= popUpTextTime)
            {
                popUpTimeCounter = 0;
                setPopUpText = false;
                playerPopUpText.gameObject.SetActive(false);
                print("Pop up text false");
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    bool setPopUpText = false;
    void popUpText()
    {
        playerPopUpText.gameObject.SetActive(true);
        setPopUpText = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gold"))
        {
            if (resourceManager.goldAmount < resourceManager.goldBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gold);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Coal"))
        {
            if (resourceManager.coalAmount < resourceManager.coalBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.coal);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Metal"))
        {
            if (resourceManager.metalAmount < resourceManager.metalBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.metal);
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Gems"))
        {
            if (resourceManager.gemAmount < resourceManager.gemBagCapacity)
            {
                ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gem);
                Destroy(collision.gameObject);
            }
            else
            {
                popUpText();
            }
        }

        if (collision.gameObject.CompareTag("DropBox"))
        {
            // dropBoxReference = collision.gameObject.GetComponent<Dropbox>();
            isTouchingDropbox = true;
            dropBoxReference.ShowDropBoxCanvas();
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DropBox"))
        {
            isTouchingDropbox = false;
            dropBoxReference.HideDropBoxCanvas();
        }
    }
}
