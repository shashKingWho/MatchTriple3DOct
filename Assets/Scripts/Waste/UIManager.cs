using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject gameOverUI;
    public GameObject gameCompleteUI;
    public GameObject waitForLivesUI;
    public TMPro.TextMeshProUGUI coinsText;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        // // Load relevant UI elements based on scene
        // LoadUIForCurrentScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //     private void LoadUIForCurrentScene()
    // {
    //     // Load Game Over, Game Complete, etc., only if it's a level scene
    //     if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Level"))
    //     {
    //         gameOverUI = GameObject.Find("GameOverUI");
    //         gameCompleteUI = GameObject.Find("GameCompleteUI");
    //         waitForLivesUI = GameObject.Find("WaitForLivesUI");
    //         coinsText = GameObject.Find("CoinsText").GetComponent<TMPro.TextMeshProUGUI>();
    //     }
    // }

     public void HideAllUI()
    {
        gameOverUI.SetActive(false);
        gameCompleteUI.SetActive(false);
        waitForLivesUI.SetActive(false);
    }

    public void ShowGameOver()
    {
        HideAllUI();
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        
    }

    public void ShowGameComplete()
    {
        HideAllUI();
        if (gameCompleteUI != null)
            gameCompleteUI.SetActive(true);
    }

    public void ShowWaitForLives()
    {
        HideAllUI();
        if (waitForLivesUI != null)
            waitForLivesUI.SetActive(true);
    }

    public void UpdateCoins(int coins)
    {
        if (coinsText != null)
            coinsText.text = coins.ToString();
    }
}
