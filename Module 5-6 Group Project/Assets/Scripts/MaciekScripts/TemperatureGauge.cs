//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//WORK IN PROGRESS :^) It's really scuffed right now xD
public class TemperatureGauge : MonoBehaviour
{
    //Rect tansforms
    public RectTransform tempImage;

    //Script variables
    public float maxTemp;

    [HideInInspector]
    public float currentTemp;

    private float initialTempImageHeight;
    private float newTempImageHeight;

    //Text
    public Text temperatureText;

    //Scripts references*
    private SpawnerV2 spawnerScript;
    private WaveHandler waveHandlerScript;

    void Start()
    {
        //Script references
        spawnerScript = FindObjectOfType<SpawnerV2>();
        waveHandlerScript = FindObjectOfType<WaveHandler>();

        //Script variables
        currentTemp = 0;
        initialTempImageHeight = tempImage.sizeDelta.y;

        //Initialize functions
        ResizeTempImage();
        UpdateTempText();
    }

    void ResizeTempImage() //Resizes temperature image
    {
        newTempImageHeight = (currentTemp / maxTemp) * initialTempImageHeight; //Calculates the current percentage of the max temperature that will be used to resize the image 

        tempImage.sizeDelta = new Vector2(tempImage.sizeDelta.x, newTempImageHeight); //Resizes the image
    }

    void UpdateTempText() //Updates text to show current temperature of the generator
    {
        temperatureText.text = currentTemp.ToString() + "°";
    }

    public void ChangeGeneratorTemperature(float value) //Changes generators temperature by given value
    {
        currentTemp += value;

        if (currentTemp < maxTemp && currentTemp > 0) //Makes sure that the temperature stays within the limit
        {            

            if (currentTemp > maxTemp)
            { 
                currentTemp = maxTemp; 
            }

            if (currentTemp < 0)
            {
                currentTemp = 0; 
            }
        }
        else
        {
            CheckGeneratorTemperature(); //If the temperature is outside of the limits, a function is called to check which limit has been exceeded
        }
 
        ResizeTempImage();
        UpdateTempText();
    }

    void CheckGeneratorTemperature()
    {
        spawnerScript.SpawnerToggle(); //Turns off spawner 
        currentTemp = 0; //Resets the temperature

        if (currentTemp >= maxTemp)
        {
            //Something happens that makes the player lose
        }

        if(currentTemp <= 0)
        {
            waveHandlerScript.AdvanceToNextWave();
        }
    }
}
