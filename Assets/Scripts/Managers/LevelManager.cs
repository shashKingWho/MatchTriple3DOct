using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public AudioClip LevelMusicClip;

    public UnityEngine.UI.Button backButton, continueButton;


    private void Awake()
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
        // GameController.Instance.lifeController.OnPlayLevel();  // Deduct one life when the game starts
        HideAllUI();
        // initialization things
        // Play menu music initially
        if (LevelMusicClip != null)
        {
            AudioManager.Instance.PlayMusic(LevelMusicClip);
        }

        backButton.onClick.AddListener(ReturnToMainMenu);
        continueButton.onClick.AddListener(ReturnToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideAllUI()
    {
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    public void ShowGameOver()
    {
        HideAllUI();
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void ShowGameGameWin()
    {
        HideAllUI();
        gameWinUI?.SetActive(true);

    }

    // Call when returning to the main menu
    public void ReturnToMainMenu()
    {
        // Assuming you load the main menu scene by name
        GameController.Instance.LoadLevel("MainMenu");
    }
}
