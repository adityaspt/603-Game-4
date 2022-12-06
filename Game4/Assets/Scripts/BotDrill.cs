using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDrill : MonoBehaviour
{
    public List<Wall> currentWalls = new List<Wall>();
    public float drillSpeed = 1;
    public float drillChargeConsumption = 20.0f;
    public bool isDrilling;
    public bool nearWall = false;
    public GameObject drillVFX;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpSize()
    {
        Vector3 newScale = transform.localScale;
        newScale *= 1.2f;
        transform.localScale = newScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       


        if (collision.gameObject.tag == "Wall")
        {
            currentWalls.Add(collision.GetComponent<Wall>());
            nearWall = true;
        }



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       

        if (collision.gameObject.tag == "Wall")
        {
            currentWalls.Remove(collision.GetComponent<Wall>());
            nearWall=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Set the drill animation and vfx
        animator.SetBool("IsDrilling", isDrilling);
        drillVFX.SetActive(isDrilling);

        if (nearWall)
        {
            for (int i = 0; i < currentWalls.Count; i++)
            {
                currentWalls[i].Drill(Time.deltaTime * drillSpeed);
            }

            isDrilling = true; 
        }
        else
        {
            isDrilling = false;
        }
    }
}
