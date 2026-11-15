
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [Header("Prefab to instantiate")]
    [SerializeField] private GameObject replayObjectPrefab;
    public Timer timer;
    
    
 public Queue<ReplayData> recordingQueue { get; private set;}
 private List<Recording> recordings;
 private bool isDoingReplay = true;
 private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
        recordings = new List<Recording>();
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
        isDoingReplay = false;
        if (recordingQueue.Count > 0)
        {
            Recording completedTurn = new Recording(recordingQueue);
            recordings.Add(completedTurn);
            recordingQueue.Clear();
        }
        //TODO WHEN THE PLAYER STARTS
        //StartReplay();
    }
    private void OnRestartLevel()
    {
        recordingQueue.Clear();
        isDoingReplay = true;
        foreach(Recording pastTurn in recordings)
        {
            pastTurn.DestroyReplayObjectIfExists();
            pastTurn.RestartFromBeginning();
            pastTurn.InstantiateReplayObject(replayObjectPrefab);
        }
        //todo when the player ends run
        //Reset();
    }
    public void RecordReplayFrame(ReplayData data)
    {
        if (!isDoingReplay)
        {
            return;
        }
       recordingQueue.Enqueue(data);
    }
    private void StartReplay()
    {
        isDoingReplay = true;
        AddRecording();
        //Instantiate Object
        foreach(Recording recording in recordings)
        {
            recording.InstantiateReplayObject(replayObjectPrefab);
        }
        
        //TODO - INSTANTIATE AND CHANGE CAMERA TARGET 
    }
    private void RestartReplay()
    {
        isDoingReplay = true;
        foreach(Recording recording in recordings)
        {
            recording.RestartFromBeginning();
        }
        
        //TODO - restart queued data from beginning
    }
    public void Reset()
    {
        isDoingReplay = false;
        recordingQueue.Clear();
        foreach(Recording recording in recordings)
        {
           recording.DestroyReplayObjectIfExists(); 
        }
        //TODO - reset the recoredr
        
        recordings = new List<Recording>();
    }
    public void StartNewRecording()
    {
        AddRecording();
    }
    private void AddRecording()
    {
        recordings.Add(new Recording(recordingQueue));
        //reset current recording queue
        recordingQueue.Clear();
    }
    private void FixedUpdate()
    {
        if (!timer.isStarted)
        {
            return;
        }
        bool hasMoreFrames = false;
        foreach(Recording recording in recordings)
        {
           hasMoreFrames = recording.PlayNextFrame(); 
        }
        //check if we finished
        if (!hasMoreFrames)
        {
           // RestartReplay();
        }
    }
}
