using UnityEngine;
using System.Collections;
using System.Linq;

public struct Frame
{
    public int mIndex;
    public bool mEmpty;
    public float mHorizontal;
    public float mVertical;
    public float mOffset;
    // TO DO: Add member variable for recording button presses.
}

[RequireComponent(typeof(PlayerHUD))]
[RequireComponent(typeof(PlayerController))]
public class RecordBehavior : MonoBehaviour 
{
    public Frame[] recordedFrames;
    public float duration = 0.0f;
    private bool isRecording = false;

    int maxRecordedFrames = 0;
    int frameCount = 0;

    private PlayerHUD hud;
    private PlayerController ctrl;

	// Use this for initialization
	void Awake()
    {
        hud = GetComponent<PlayerHUD>();
        ctrl = GetComponent<PlayerController>();
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
                recordedFrames[frameCount].mIndex = frameCount++;
                hud.setSlider((float)frameCount / (float)maxRecordedFrames);
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
            frameCount = 0;
            maxRecordedFrames = (int)(duration / Time.fixedDeltaTime);
            recordedFrames = new Frame[maxRecordedFrames];
            isRecording = true;
        }
        hud.startRec();
    }

    public void StopRecording () 
    {
        recordedFrames = recordedFrames.Take(frameCount - 1).ToArray();
        isRecording = false;
        hud.stopRec();
        ctrl.resetPlaybackCounter();
    }

    public void RecordFrameAction(float horizontal, float vertical, float offset)
    {
       recordedFrames[frameCount].mHorizontal = horizontal;
       recordedFrames[frameCount].mVertical = vertical;
       recordedFrames[frameCount].mOffset = offset; 
       recordedFrames[frameCount].mEmpty = false;
    }
    
    public bool IsRecording()
    {
        return isRecording;
    }
}
