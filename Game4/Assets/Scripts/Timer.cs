using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public Upgrade dsp;
    public float dspTimeRemaining;
    public float dspTotalTime;
    private bool dspActive = false;
    public Slider dspSlider;
    public Text dspTimeText;
    public TimeSpan dspTS;

    public Upgrade dsi;
    public float dsiTimeRemaining;
    public float dsiTotalTime;
    private bool dsiActive = false;
    public Slider dsiSlider;
    public Text dsiTimeText;
    public TimeSpan dsiTS;

    public Upgrade bc;
    public float bcTimeRemaining;
    public float bcTotalTime;
    private bool bcActive = false;
    public Slider bcSlider;
    public Text bcTimeText;
    public TimeSpan bcTS;

    public Upgrade b;
    public float bTimeRemaining;
    public float bTotalTime;
    private bool bActive = false;
    public Slider bSlider;
    public Text bTimeText;
    public TimeSpan bTS;

    private ShopController shopController;
    
    

    // Start is called before the first frame update
    void Start()
    {
        shopController = FindObjectOfType<ShopController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dspActive)
        {
            dspTimeRemaining -= Time.unscaledDeltaTime;
            if(dspTimeRemaining < 0)
            {
                dspActive = false;
                dsp.UpgradeComplete();
            }
        }

        dsp.slider.value = (dspTotalTime - dspTimeRemaining) / dspTotalTime;

        dspTS = TimeSpan.FromSeconds(dspTimeRemaining);

        dsp.timeText.text = dspTS.ToString("hh':'mm':'ss");

        if (dsiActive)
        {
            dsiTimeRemaining -= Time.unscaledDeltaTime;
            if (dsiTimeRemaining < 0)
            {
                dsiActive = false;
                dsi.UpgradeComplete();
            }
        }

        dsi.slider.value = (dsiTotalTime - dsiTimeRemaining) / dsiTotalTime;

        dsiTS = TimeSpan.FromSeconds(dsiTimeRemaining);

        dsi.timeText.text = dsiTS.ToString("hh':'mm':'ss");

        if (bcActive)
        {
            bcTimeRemaining -= Time.unscaledDeltaTime;
            if (bcTimeRemaining < 0)
            {
                bcActive = false;
                bc.UpgradeComplete();
            }
        }

        bc.slider.value = (bcTotalTime - bcTimeRemaining) / bcTotalTime;

        bcTS = TimeSpan.FromSeconds(bcTimeRemaining);

        bc.timeText.text = bcTS.ToString("hh':'mm':'ss");

        if (bActive)
        {
            bTimeRemaining -= Time.unscaledDeltaTime;
            if (bTimeRemaining < 0)
            {
                bActive = false;
                b.UpgradeComplete();
            }
        }

        b.slider.value = (bTotalTime - bTimeRemaining) / bTotalTime;

        bTS = TimeSpan.FromSeconds(bTimeRemaining);

        b.timeText.text = bTS.ToString("hh':'mm':'ss");
    }

    public void DSPStartTimer(float seconds)
    {
        dspTotalTime = seconds;
        dspTimeRemaining = seconds;
        dspActive = true;
    }

    public void DSPReset()
    {
        dspTotalTime = 0f;
        dspTimeRemaining = 0f;
        dspActive = false;
    }

    public void DSIStartTimer(float seconds)
    {
        dsiTotalTime = seconds;
        dsiTimeRemaining = seconds;
        dsiActive = true;
    }

    public void DSIReset()
    {
        dsiTotalTime = 0f;
        dsiTimeRemaining = 0f;
        dsiActive = false;
    }

    public void BCStartTimer(float seconds)
    {
        bcTotalTime = seconds;
        bcTimeRemaining = seconds;
        bcActive = true;
    }

    public void BCReset()
    {
        bcTotalTime = 0f;
        bcTimeRemaining = 0f;
        bcActive = false;
    }

    public void BStartTimer(float seconds)
    {
        bTotalTime = seconds;
        bTimeRemaining = seconds;
        bActive = true;
    }

    public void BReset()
    {
        bTotalTime = 0f;
        bTimeRemaining = 0f;
        bActive = false;
    }








}
