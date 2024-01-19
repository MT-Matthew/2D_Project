using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float tempTimeScale = 1;
    public static GameManager instance;

    public enum GameState
    {
        GamePlay,
        Paused,
        GameOver,
        LevelUp
    }

    // PlayerController player;

    public GameState currentState;
    public GameState previousState;

    [Header("Screen")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject settingsScreen;
    public GameObject statsScreen;
    public GameObject background;

    [Header("UI")]
    public GameObject UpgradeUIPrefab;
    public GameObject LootUIPrefab;

    public bool isGameOver = false;
    private bool isSetting = false;
    private bool isPaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        DisableScreens();
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.GamePlay:
                CheckForPauseAndResume();
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.LevelUp:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    DisplayResults();
                }
                break;

            default:
                Debug.Log("State Does Not Exist");
                break;
        }
        if (isPaused)
        {
            background.SetActive(true);
            statsScreen.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                if (isSetting)
                {
                    HideSettings();
                }
                else
                {
                    ResumeGame();
                }

            }
            GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>().volume = 0.17f;
        }
        else
        {
            if (!isGameOver)
            {
                background.SetActive(false);
                statsScreen.SetActive(false);
                GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>().volume = 0.5f;
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            pauseScreen.SetActive(true);
            previousState = currentState;
            ChangeState(GameState.Paused);
            tempTimeScale = Time.timeScale;
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (currentState != GameState.GamePlay)
        {
            pauseScreen.SetActive(false);
            ChangeState(GameState.GamePlay);
            if (currentState != GameState.LevelUp)
            {
                Time.timeScale = tempTimeScale;
                isPaused = false;
            }
        }
    }

    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                if (isSetting)
                {
                    HideSettings();
                }
                else
                {
                    ResumeGame();
                }
            }
            else if (currentState == GameState.GamePlay)
            {
                PauseGame();
            }
        }
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            ChangeState(GameState.GameOver);
        }
    }

    public void StartLevelUp(bool isLevelUp)
    {
        if (isLevelUp)
        {
            UpgradeUIPrefab.SetActive(true);
        }
        else
        {
            LootUIPrefab.SetActive(true);
        }

        ChangeState(GameState.LevelUp);
        isPaused = true;
        tempTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void EndLevelUp()
    {
        UpgradeUIPrefab.SetActive(false);

        ChangeState(GameState.GamePlay);
        isPaused = false;
        Time.timeScale = tempTimeScale;
    }

    public void EndLoot()
    {
        LootUIPrefab.SetActive(false);

        ChangeState(GameState.GamePlay);
        isPaused = false;
        Time.timeScale = tempTimeScale;
    }

    void DisplayResults()
    {
        background.SetActive(true);
        resultsScreen.SetActive(true);
        statsScreen.SetActive(true);
        GameObject.FindGameObjectWithTag("Background").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("Game Over").GetComponent<AudioSource>().Play();

        GetComponent<Counting>().CalculateScore();

        tempTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void DisplaySettings()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
        isSetting = true;
    }

    public void HideSettings()
    {
        pauseScreen.SetActive(true);
        settingsScreen.SetActive(false);
        isSetting = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
