using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public List<Wall> currentWalls = new List<Wall>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger!");
        if (collision.gameObject.tag == "Wall")
        {
            currentWalls.Add(collision.GetComponent<Wall>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            currentWalls.Remove(collision.GetComponent<Wall>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < currentWalls.Count; i++)
            {
                currentWalls[i].Drill(Time.deltaTime);
            }
        }
    }
}
