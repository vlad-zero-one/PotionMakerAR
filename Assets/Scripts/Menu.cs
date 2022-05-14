using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Menu : MonoBehaviour
{
    private List<GameObject> buttons = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform tr in transform)
        {
            buttons.Add(tr.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowOrHideMenu();
        }
    }

    public void ShowOrHideMenu()
    {
        PauseOrPlay();

        foreach (var button in buttons)
        {
            button.SetActive(!button.activeSelf);
        }
    }

    private void PauseOrPlay()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
