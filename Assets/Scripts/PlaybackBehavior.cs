using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public enum PlaybackMode {
    RunOnce,
    Loop
}

[RequireComponent(typeof(ActorBehaviour))]
public class PlaybackBehavior : MonoBehaviour 
{
    public Frame[] recordedFrames;
    public Slider sliderHP;
    Renderer r;
    PlaybackMode mode;
    bool isPlaying = false;
    ActorBehaviour actor;
    
    int frameCount = 0;

	// Use this for initialization
	void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        r = GetComponent<Renderer>();
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
                actor.PerformActions(recordedFrames[frameCount].action);
                frameCount++;
            }
            else
            {
                if (mode == PlaybackMode.RunOnce)
                    StopPlayback();
                else if (mode == PlaybackMode.Loop)
                    frameCount = 0;
            }

            sliderHP.value = 1.0f - ((float)frameCount / (float)recordedFrames.Length);
            
            Color current = r.materials[0].color;
            current.a = 1.0f - ((float)frameCount / (float)recordedFrames.Length);
            r.materials[0].color = current;

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
