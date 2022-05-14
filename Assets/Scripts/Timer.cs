using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    readonly private int levelTime = 60;

    private Text timerText;

    private int currentTime;

    void Start()
    {
        timerText = GetComponent<Text>();

        currentTime = levelTime;
        timerText.text = currentTime.ToString();
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(DecreaseTimer());
    }

    private IEnumerator DecreaseTimer(int decrement = 1)
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);

            currentTime -= decrement;
            timerText.text = currentTime.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
