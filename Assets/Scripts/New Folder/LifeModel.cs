using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LifeModel : MonoBehaviour
{
    public int Lives { get; private set; }
    public const int MaxLives = 5;
    [Space]
    private DateTime lastLifeRegenerationTime;
    

    public void LifeModelStart()
    {
        // Load saved data
        Lives = PlayerPrefs.GetInt("Lives", MaxLives);
        lastLifeRegenerationTime = DateTime.Parse(PlayerPrefs.GetString("LastLifeTime", DateTime.Now.ToString()));

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void UpdateLifeRegeneration()
    {
        TimeSpan timeSinceLastLife = DateTime.Now - lastLifeRegenerationTime;

        if (Lives < MaxLives && timeSinceLastLife.TotalMinutes >= .10f) // >= 2)
        {
            Lives++;
            lastLifeRegenerationTime = DateTime.Now;
            PlayerPrefs.SetInt("Lives", Lives);
            PlayerPrefs.SetString("LastLifeTime", lastLifeRegenerationTime.ToString());
        }

     
    }


    public void UseLife()
    {
        if (Lives > 0)
        {
            Lives--;
            PlayerPrefs.SetInt("Lives", Lives);

            // If lives become 0, start tracking life regeneration immediately
            if (Lives < MaxLives)
            {
                lastLifeRegenerationTime = DateTime.Now;
                PlayerPrefs.SetString("LastLifeTime", lastLifeRegenerationTime.ToString());
            }
        }
    }


}
