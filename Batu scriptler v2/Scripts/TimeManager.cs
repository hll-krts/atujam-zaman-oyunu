using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowdownFactor = 0.05f;
    public float slowdownLenght = 1.5f;

    private void Update()
    {
        Time.timeScale += (1f / slowdownLenght) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowmotion() 
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale*.02f;
    }
}
