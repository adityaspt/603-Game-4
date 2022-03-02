using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb2d;

    private Vector2 movement;

    public GameObject pauseCanvas;

    //public int bombs;
    //public int lights;

    public GameObject bombPrefab;
    public GameObject lightPrefab;

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
        movement.x=   Input.GetAxisRaw("Horizontal");
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
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }

        // Light key
        if (Input.GetKeyDown(KeyCode.E) && ResourceManager.resourceManagerInstance.torches > 0)
        {
            ResourceManager.resourceManagerInstance.UseATorch();
            Instantiate(lightPrefab, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        //Movement
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gold"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gold);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Coal"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.coal);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Metal"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.metal);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Gems"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gem);
            Destroy(collision.gameObject);
        }
        
       
    }
}
