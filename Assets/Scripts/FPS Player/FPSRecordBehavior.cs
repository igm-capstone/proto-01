using UnityEngine;
using System.Collections;
using System.Linq;

public struct FPSFrame
{
    public int mIndex;
    public bool mEmpty;
    public float mHorizontal;
    public float mVertical;
    public float mOffset;
    public bool mJump;
}

[RequireComponent(typeof(PlayerHUD))]
[RequireComponent(typeof(PlayerController))]
public class FPSRecordBehavior : MonoBehaviour 
{
    public FPSFrame[] recordedFrames;
    public float duration = 0.0f;
    public bool isRecording { get; private set; }

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
            recordedFrames = new FPSFrame[maxRecordedFrames];
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
    //whats the offset?
    public void RecordFrameAction(float horizontal, float vertical, float offset, bool jump)
    {
       recordedFrames[frameCount].mHorizontal = horizontal;
       recordedFrames[frameCount].mVertical = vertical;
       recordedFrames[frameCount].mOffset = offset;
       recordedFrames[frameCount].mJump = jump;
       recordedFrames[frameCount].mEmpty = false;
    }
    
    public bool IsRecording()
    {
        return isRecording;
    }
}