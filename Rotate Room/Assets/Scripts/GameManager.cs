using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public enum LevelState { PAUSE, RUN, WIN, LOSE }
    [SerializeField] public int currentLevel = 0;
    [SerializeField] private LevelState levelState = LevelState.RUN;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject levelCanvas;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject continueMenu;
    [SerializeField] private GameObject pauseIcon;
    [SerializeField] private GameObject resumeIcon;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] public int maxLevel = 3;
    [SerializeField] private LevelHandler levelHandler;
    private void Start()
    {
        if (currentLevel == 0) return;
        levelText.text = currentLevel.ToString();
    }
    public LevelState GetLevelState()
    {
        return levelState;
    }
    public void SetPauseState()
    {
        levelState = LevelState.PAUSE;
        Time.timeScale = 0;
    }
    public void SetRunState()
    {
        levelState = LevelState.RUN;
        Time.timeScale = 1f;
    }
    public void SetWinState()
    {
        levelState = LevelState.WIN;
        levelHandler.UnlockLevel((currentLevel + 1) % (maxLevel + 1));
    }
    public void SetLoseState()
    {
        levelState = LevelState.LOSE;
    }
    public void ReloadCurrentLevel()
    {
        LoadLevel(currentLevel);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        currentLevel++;
        currentLevel = currentLevel % (maxLevel + 1);
        LoadLevel(currentLevel);
    }
    public void LoadLevel(int i)
    {
        Debug.Log("Access level " + i);
        Time.timeScale = 1f;
        currentLevel = i;
        if (currentLevel == 0)
        {
            BackToMainMenu();
            return;
        }
        mainCanvas.SetActive(false);
        levelCanvas.SetActive(true);
        pauseIcon.SetActive(true);
        resumeIcon.SetActive(false);
        levelText.text = currentLevel.ToString();
        restartMenu.SetActive(false);
        continueMenu.SetActive(false);
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Level_" + i);
    }
    public void ShowRestartMenu()
    {
        restartMenu.SetActive(true);
    }
    public void ShowContinueMenu()
    {
        continueMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SetRunState();
        currentLevel = 0;
        levelCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
