
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timertext;
    public float remainingTime;
    public bool isStarted= false;
    public playerMovement pm;
    private float initialTime;
   
    void Start()
{
    initialTime = remainingTime;
    GameManager.Instance.onPlayerRespawn += ResetTimer;
    GameManager.Instance.onRestartLevel += ResetTimer;
}

private void OnDestroy()
{
    if (GameManager.Instance != null)
    {
        GameManager.Instance.onPlayerRespawn -= ResetTimer;
        GameManager.Instance.onRestartLevel -= ResetTimer;
    }
}

public void ResetTimer()
{
    StopAllCoroutines();
    remainingTime = initialTime;
    isStarted = false;
}

    
    void Update()
    {
        if (isStarted)
        {
        
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime ;
        }
        else
        {
            isStarted = false;
            GameManager.Instance.GoalReached();
            pm.anim.SetTrigger("Dead");
            pm.deathThisFrame = true;
            pm.IsDead = true;
            remainingTime = 0;
            StartCoroutine(CountdownRoutine());
        }
       }
       
       int minutes = Mathf.FloorToInt(remainingTime/60);
       int seconds = Mathf.FloorToInt(remainingTime % 60);
       timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.RestartLevel();
    }
}
