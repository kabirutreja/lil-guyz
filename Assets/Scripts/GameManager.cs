
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get;private set;}
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
        Debug.LogError("Isuue with gm.. more than one");    
        }
         Instance = this;
    }
    //completed the stage
    public event Action onGoalReached;
    public void GoalReached()
    {
        if (onGoalReached != null)
        {
            onGoalReached();
        }
    }
    //another turn
    public event Action onRestartLevel;
    public void RestartLevel()
    {
        if (onRestartLevel != null)
        {
            onRestartLevel();
        }
    }
    public event Action<GameObject> onChangeCameraTarget;
    public void ChangeCameraTarget(GameObject newTarget)
    {
        if (onChangeCameraTarget != null)
        {
           onChangeCameraTarget(newTarget); 
        }
    }
    public event Action onPlayerRespawn;
    public void PlayerRespawn()
    {
        if (onPlayerRespawn != null)
        {
            onPlayerRespawn();
        }
    }
    
}
