using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePP : MonoBehaviour
{
    private Recorder recorder;
    // Start is called before the first frame update
    void Awake()
    {
       recorder = GetComponent<Recorder>(); 
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        ReplayData data = new MovingObjectReplayData(this.transform.position, this.transform.localScale);
        recorder.RecordReplayFrame(data);
    }
}
