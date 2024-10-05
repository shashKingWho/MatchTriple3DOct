using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public LifeController lifeController;

    public int coinsAwardedOnWin = 50;
    public int currentCoins = 0;


    void Awake()
    {
        lifeController = FindObjectOfType<LifeController>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


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
        
        currentCoins = PlayerPrefs.GetInt("Coins", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeController == null)
        {
            lifeController = FindObjectOfType<LifeController>();
        }
    }

    // Call this method when a matching failure happens (collection bar fills)
    public void GameOver()
    {
        // Show Game Over UI
        LevelManager.Instance?.ShowGameOver();        
    }

    // Call this method when all matches are successfully made
    public void GameWin()
    {
        currentCoins += coinsAwardedOnWin;    // Award coins
        PlayerPrefs.SetInt("Coins", currentCoins);   
        //MainMenuManager.Instance.UpdateCoinsUI();

        LevelManager.Instance?.ShowGameGameWin();
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void CameraShakeNow()
    {
        Camera.main.transform.DOShakePosition(0.5f, 0.2f, 10, 90, false, true);
    }
}
