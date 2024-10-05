using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeView : MonoBehaviour
{
    public TMPro.TextMeshProUGUI livesText;
    public TMPro.TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void UpdateLivesText(int lives)
    {
        livesText.text =  lives.ToString();
        Debug.Log("UpdateLivesText Lives: " + lives);
    }

    public void UpdateTimer(LifeModel lifeModel)
    {
        TimeSpan timeSinceLastLife = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastLifeTime", DateTime.Now.ToString()));
        // TESTING
        Debug.Log("UpdateTimer Lives: " + LifeModel.MaxLives);

        if (lifeModel.Lives < LifeModel.MaxLives)
        {
            double minutesLeft = Math.Max(0, 2 - timeSinceLastLife.TotalMinutes);
            timerText.text = minutesLeft.ToString("0.00");
        }
        else
        {
            timerText.text = "Full Lives!";
    
        }
    }
}
