using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(ActorBehaviour))]

public enum PlaybackMode {
    RunOnce,
    Loop
}

public class PlaybackBehavior : MonoBehaviour 
{
    Frame[] recordedFrames;
    PlaybackMode mode;
    bool isPlaying = false;
    ActorBehaviour actor;

    int frameCount = 0;

	// Use this for initialization
	void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
	}
	
	// Update is called once per frame
	void Update() 
    {
	    
	}

    void FixedUpdate()
    {
        if (isPlaying)
        {
            if (frameCount < recordedFrames.Length)
            {
                actor.PerformAction(recordedFrames[frameCount].action);
                frameCount++;
            }
            else
            {
                if (mode == PlaybackMode.RunOnce)
                    StopPlayback();
                else if (mode == PlaybackMode.Loop)
                    frameCount = 0;
            }
        }
    }

    public void StartPlayback (Frame[] recordedFrames, PlaybackMode mode) 
    {
        this.recordedFrames = recordedFrames;
        this.mode = mode;
        this.isPlaying = true;
    }

    public void StopPlayback()
    {
        isPlaying = false;
        Destroy(gameObject);
    }

    public void PausePlayback()
    {
        isPlaying = false;
    }

}
