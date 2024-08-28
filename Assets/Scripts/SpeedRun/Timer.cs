using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time = 0f;
    private bool isRunning = true;
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

            text.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);


        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        time = 0f;
    }

    public float GetTime()
    {
        return time;
    }


}
