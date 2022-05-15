using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public delegate void TimerDelegate();
    public event TimerDelegate OnTimeIsUp;

    [SerializeField] private int levelTime = 60;

    private Text timerText;

    private int currentTime;

    private Coroutine coroutine;

    public void Restart()
    {
        currentTime = levelTime;

        StartTimer();
    }

    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        timerText = GetComponent<Text>();

        currentTime = levelTime;
        timerText.text = currentTime.ToString();
        coroutine = StartCoroutine(DecreaseTimer());
    }

    private IEnumerator DecreaseTimer(int decrement = 1)
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);

            currentTime -= decrement;
            timerText.text = currentTime.ToString();
        }

        OnTimeIsUp?.Invoke();

        StopCoroutine(coroutine);
    }

    private void OnDestroy()
    {
        OnTimeIsUp = null;
    }
}
