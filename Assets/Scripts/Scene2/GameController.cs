using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public LifeController lifeController;   // Reference to the life controller
    // public InventoryController inventoryController;   // Reference to the inventory controller

    public int coinsAwardedOnWin = 50;      // Coins rewarded upon winning
    public int currentCoins = 0;


    void Awake()
    {
        lifeController= FindObjectOfType<LifeController>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when the object is destroyed to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

        // This method is called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (lifeController == null)
        {
            lifeController = FindObjectOfType<LifeController>(); // Only find once after scene load
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Load coins from PlayerPrefs or start with 0
        currentCoins = PlayerPrefs.GetInt("Coins", 0);
    }

    // Update is called once per frame
    void Update()
    {
if(lifeController==null){
    lifeController= FindObjectOfType<LifeController>();
}
    }

    // Call this method when a matching failure happens (collection bar fills)
    public void GameOver()
    {
        // Show Game Over UI
        LevelManager.Instance?.ShowGameOver();
        // Optionally: you can add more functionality here, such as animations or effects
    }

    // Call this method when all matches are successfully made
    public void GameWin()
    {
        currentCoins += coinsAwardedOnWin;    // Award coins
        PlayerPrefs.SetInt("Coins", currentCoins);   // Save coins to PlayerPrefs
        //MainMenuManager.Instance.UpdateCoinsUI();

        LevelManager.Instance?.ShowGameGameWin();
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
