using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public List<Wall> currentWalls = new List<Wall>();
    public float drillSpeed = 1;
    public bool isDrilling;
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
        // Set the drill animation and vfx
        animator.SetBool("IsDrilling", isDrilling);
        drillVFX.SetActive(isDrilling);

        if (Input.GetMouseButton(0))
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

       // isDrilling = false;
    }
}
