using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public int upgradeNumber;
    public float timeRemaining;
    public float totalTime;
    private bool active = false;
    public Slider thisSlider;
    public Text timeText;

    public TimeSpan ts;

    private ShopController shopController;
    
    

    // Start is called before the first frame update
    void Start()
    {
        shopController = FindObjectOfType<ShopController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining < 0)
            {
                active = false;
                shopController.UpgradeComplete();
            }
        }

        thisSlider.value = (totalTime - timeRemaining) / totalTime;

        ts = TimeSpan.FromSeconds(timeRemaining);

        timeText.text = ts.ToString("hh':'mm':'ss");
    }

    public void StartTimer(float seconds)
    {
        totalTime = seconds;
        timeRemaining = seconds;
        active = true;
    }

    public void Reset()
    {
        totalTime = 0f;
        timeRemaining = 0f;
        active = false;
    }





}
