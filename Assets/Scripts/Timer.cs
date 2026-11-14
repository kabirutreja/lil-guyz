using System.Xml.Schema;
using System;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        yield return new WaitForSeconds(10f);
        GameManager.Instance.RestartLevel();
    }
}
