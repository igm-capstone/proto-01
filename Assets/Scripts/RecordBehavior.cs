using UnityEngine;
using System.Collections;
using System.Linq;

public class Frame
{
    public int index;
    public int[] actionIDs;
}

public class RecordBehavior : MonoBehaviour 
{
    public Frame[] recordedFrames;
    public float duration = 0.0f;
    bool isRecording = false;

    int maxRecordedFrames = 0;
    int frameCount = 0;

	// Use this for initialization
	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update() 
    {
	    
	}

    void FixedUpdate()
    {
        
        if (isRecording)
        {
            if (frameCount < maxRecordedFrames)
            {
                recordedFrames[frameCount].index = frameCount++;
            }
            else
            {
                StopRecording();
            }
        }
    }

    public void StartRecording () 
    {
        if (duration > 0.0f) 
        {
            maxRecordedFrames = (int)(duration / Time.fixedDeltaTime);
            recordedFrames = new Frame[maxRecordedFrames];
            isRecording = true;
        }
    }

    public void StopRecording () 
    {
        isRecording = false;
    }

    public void SetRecordedFrameParameters(int[] actionIDs)
    {
        if (isRecording)
        {
            recordedFrames[frameCount].actionIDs = actionIDs;
        }
    }
}
