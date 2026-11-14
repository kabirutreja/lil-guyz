
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [Header("Prefab to instantiate")]
    [SerializeField] private GameObject replayObjectPrefab;
 public Queue<ReplayData> recordingQueue { get; private set;}
 private Recording recording;
 private bool isDoingReplay = false;
 private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
    }
    private void Start()
    {
        GameManager.Instance.onGoalReached += OnGoalReached;
        GameManager.Instance.onRestartLevel += OnRestartLevel;
    }
    private void OnDestroy()
    {
        GameManager.Instance.onGoalReached -= OnGoalReached;
        GameManager.Instance.onRestartLevel -= OnRestartLevel;
    }
    
    private void OnGoalReached()
    {
        //TODO WHEN THE PLAYER STARTS
        StartReplay();
    }
    private void OnRestartLevel()
    {
        //todo when the player ends run
        Reset();
    }
    public void RecordReplayFrame(ReplayData data)
    {
       recordingQueue.Enqueue(data);
    }
    private void StartReplay()
    {
        isDoingReplay = true;
        //Initialize
        recording = new Recording(recordingQueue);
        //reset current recording queue
        recordingQueue.Clear();
        //Instantiate Object
        recording.InstantiateReplayObject(replayObjectPrefab);
        //TODO - INSTANTIATE AND CHANGE CAMERA TARGET 
    }
    private void RestartReplay()
    {
        isDoingReplay = true;
        recording.RestartFromBeginning();
        //TODO - restart queued data from beginning
    }
    private void Reset()
    {
        isDoingReplay = false;
        recordingQueue.Clear();
        //TODO - reset the recoredr
        recording.DestroyReplayObjectIfExists();
        recording = null;
    }
    private void FixedUpdate()
    {
        if (!isDoingReplay)
        {
            return;
        }
        bool hasMoreFrames = recording.PlayNextFrame();
        //check if we finished
        if (!hasMoreFrames)
        {
            RestartReplay();
        }
    }
}
