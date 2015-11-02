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
    public float mCamHorizontal;
    public float mCamVertical;
    public bool mShoot;

}


[RequireComponent(typeof(PlayerController))]
public class RecordBehavior : MonoBehaviour 
{
    public FPSFrame[] recordedFrames;
    public float duration = 0.0f;
    public bool isRecording { get; private set; }

    int maxRecordedFrames = 0;
    int frameCount = 0;

    private PlayerHUD hud;
    private PlayerController ctrl;

	void Awake()
    {
        hud = GameObject.Find("Canvas").GetComponent<PlayerHUD>();
        ctrl = GetComponent<PlayerController>();
	}

    void FixedUpdate()
    {
        if (isRecording)
        {
            if (frameCount < maxRecordedFrames)
            {
                recordedFrames[frameCount].mIndex = frameCount++;
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
    }

    public void StopRecording () 
    {
        recordedFrames = recordedFrames.Take(frameCount - 1).ToArray();
        isRecording = false;
        //Update the number of recordings in play
        hud.stopRec(1);
    }
    //whats the offset?
    public void RecordFrameAction(float horizontal, float vertical, float offset, bool jump, bool shoot, float camHorizontal, float camVertical)
    {
       recordedFrames[frameCount].mHorizontal = horizontal;
       recordedFrames[frameCount].mVertical = vertical;
       recordedFrames[frameCount].mOffset = offset;
       recordedFrames[frameCount].mJump = jump;
       recordedFrames[frameCount].mEmpty = false;
       recordedFrames[frameCount].mShoot = shoot;
       recordedFrames[frameCount].mCamHorizontal = camHorizontal;
       recordedFrames[frameCount].mCamVertical = camVertical;
    }
    
    public bool IsRecording()
    {
        return isRecording;
    }
}