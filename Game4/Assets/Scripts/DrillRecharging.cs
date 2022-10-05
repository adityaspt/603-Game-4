using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillRecharging : MonoBehaviour
{
    public Slider drillSliders;
    public float drillChargingSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        drillSliders = GameObject.FindGameObjectWithTag("DrillChargeBar").GetComponent<Slider>();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Entered Charging Station");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Slider>().value += drillChargingSpeed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
