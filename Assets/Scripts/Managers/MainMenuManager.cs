using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

        public GameObject waitForLivesUI;
        public UnityEngine.UI.Button StartButton;
    public TMPro.TextMeshProUGUI coinsText;
    [Space]
    [Space]
      public AudioClip menuMusicClip;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize coins from PlayerPrefs or start with 0        
        UpdateCoinsUI();
        
        // Play menu music initially
        if (menuMusicClip != null)
        {
            AudioManager.Instance.PlayMusic(menuMusicClip);
        }

        StartButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


//My (Play button) 
        public void StartGame()
    {
        // Check if the player has enough lives to start the level
        if (GameController.Instance.lifeController.lifeModel.Lives > 0)
        {
            HideAllUI();  
            GameController.Instance.lifeController.OnPlayLevel();  // Deduct one life when the game starts
            GameController.Instance.LoadLevel("Level01");
        }
        else
        {
            ShowWaitForLives();  // Show wait UI if no lives left
        }
    }
 
         public void HideAllUI()
    {        
        waitForLivesUI.SetActive(false);
    }


    public void ShowWaitForLives()
    {
        HideAllUI();
        if (waitForLivesUI != null)
            waitForLivesUI.SetActive(true);
    }
  
    public void UpdateCoinsUI()
    {        
        GameObject.Find("CoinsText").GetComponent<TMPro.TextMeshProUGUI>().text = GameController.Instance.currentCoins.ToString();
    }
}

