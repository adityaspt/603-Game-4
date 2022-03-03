using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public float timer;
    public float radius;

    private float counter;
    private CircleCollider2D blaster;

    // Start is called before the first frame update
    void Start()
    {
        blaster = GetComponent<CircleCollider2D>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // counting down
        timer -= Time.deltaTime;

        // blast!
        if (timer < 0)
        {
            blaster.radius = radius;
        }
        if (timer < -0.1f)
        {
            Destroy(transform);
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
