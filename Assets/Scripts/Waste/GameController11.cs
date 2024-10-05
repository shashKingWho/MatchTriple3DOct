using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController11 : MonoBehaviour
{
    public static GameController11 Instance;
    public int coins = 0;
    public int currentLives = 5;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CompleteGame()
    {
        coins += 50;
        Debug.Log("Game completed! You earned 50 coins. Total coins: " + coins);
        // Trigger game complete screen logic here
    }

    // Call this when the player loses a life
    public void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            Debug.Log("Game Over!");
            // Trigger game over screen logic here
        }
        else
        {
            Debug.Log("Lives remaining: " + currentLives);
        }
    }

}
