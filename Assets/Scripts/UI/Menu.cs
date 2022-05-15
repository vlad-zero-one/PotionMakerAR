using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private RestartManager restartManager;

    [SerializeField] private GameObject clickBlocker;

    [Header("Buttons")]
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject exitButton;

    [SerializeField] private GameObject recipesScrollView;

    public void ShowOrHideMenu()
    {
        PauseOrPlay();

        clickBlocker.SetActive(!clickBlocker.activeSelf);
        continueButton.SetActive(!continueButton.activeSelf);
        restartButton.SetActive(!restartButton.activeSelf);
        exitButton.SetActive(!exitButton.activeSelf);
        recipesScrollView.SetActive(false);
    }

    public void ShowMenu(bool gameOver = false)
    {
        clickBlocker.SetActive(true);
        continueButton.SetActive(!gameOver);
        restartButton.SetActive(true);
        exitButton.SetActive(true);
        recipesScrollView.SetActive(false);
    }

    public void HideMenu()
    {
        clickBlocker.SetActive(false);
        continueButton.SetActive(false);
        restartButton.SetActive(false);
        exitButton.SetActive(false);
        recipesScrollView.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        restartManager.Restart();

        HideMenu();

        PauseOrPlay();
    }

    private void Awake()
    {
        timer.OnTimeIsUp += GameOver;
    }

    private void GameOver()
    {
        ShowMenu(gameOver: true);
        PauseOrPlay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOrHideMenu();
        }
    }

    private void PauseOrPlay()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    private void OnDestroy()
    {
        timer.OnTimeIsUp -= GameOver;
    }
}
