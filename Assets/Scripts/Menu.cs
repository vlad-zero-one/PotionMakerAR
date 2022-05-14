using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private ScoreContainer scoreContainer;

    private List<GameObject> buttons = new List<GameObject>();

    public void ShowOrHideMenu()
    {
        PauseOrPlay();

        foreach (var button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        timer.Restart();
        scoreContainer.Reset();
        scoreContainer.SetActive(false);

        ShowOrHideMenu();
    }

    private void Awake()
    {
        foreach (Transform tr in transform)
        {
            buttons.Add(tr.gameObject);
        }

        timer.OnTimeIsUp += ShowScore;
    }

    private void ShowScore()
    {
        ShowOrHideMenu();

        scoreContainer.SetActive(true);
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
        timer.OnTimeIsUp -= ShowScore;
    }
}
