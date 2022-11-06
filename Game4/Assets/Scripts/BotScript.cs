using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    public float lifeTime;
    public float maxSpeed = 5f;
    public Vector2 movement;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
    }
}
