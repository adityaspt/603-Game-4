using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb2d;

    private Vector2 movement;

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
    }

    private void FixedUpdate()
    {
        //Movement
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gold"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gold);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Coal"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.coal);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Metal"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.metal);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Gems"))
        {
            ResourceManager.resourceManagerInstance.AddResourceAmount(ResourceManager.ResourceType.gem);
           // Destroy(collision.gameObject);
        }
        
       
    }
}
