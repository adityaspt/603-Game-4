using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public float timer;
    public float radius;
    public GameObject explosionVFX;

    private float counter;
    private CircleCollider2D blaster;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        blaster = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        counter = 0;

        radius = 4;

        explosionVFX.SetActive(false);

        animator.SetTrigger("Explode");
    }

    // Update is called once per frame
    void Update()
    {
        // counting down
        timer -= Time.deltaTime;
       
        // blast!
        if (timer < 0)
        {
            explosionVFX.SetActive(true);
            GetComponent<Renderer>().enabled = false;
            blaster.radius = radius;
        }
        if (timer < -2.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger! " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Wall")
        {
            collision.gameObject.GetComponent<Wall>().Break();
        }
    }
}
