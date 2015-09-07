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
    public GameObject coordinateSpace;
    public Frame[] recordedFrames;
    Renderer r;
    PlaybackMode mode;
    bool isPlaying = false;
    ActorBehaviour actor;

    int frameCount = 0;

    //For UI
    Canvas screenCanvas;
    public Slider healthPrefab;
    public float healthPanelOffset = 0.35f;
    private Slider healthSlider;

	// Use this for initialization
	void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        r = GetComponent<Renderer>();

        screenCanvas = FindObjectOfType<Canvas>();

        healthSlider = Instantiate(healthPrefab) as Slider;
        healthSlider.transform.SetParent(screenCanvas.transform, false);
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
                Vector3 v = new Vector3(recordedFrames[frameCount].mHorizontal, 0.0f, recordedFrames[frameCount].mVertical);
                float spaceAngle = Mathf.Atan2(coordinateSpace.transform.forward.x, coordinateSpace.transform.forward.z);
                float recordAngle = recordedFrames[0].mOffset;
                float a = spaceAngle - recordAngle;
                Vector3 t = Quaternion.AngleAxis(a * Mathf.Rad2Deg, Vector3.up) * v;
                actor.PerformActions(t.x, t.z, recordedFrames[frameCount].mJump);

                frameCount++;
            }
            else
            {
                if (mode == PlaybackMode.RunOnce)
                    StopPlayback();
                else if (mode == PlaybackMode.Loop)
                    frameCount = 0;
            }

            Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            healthSlider.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            healthSlider.value = 1.0f - ((float)frameCount / (float)recordedFrames.Length);
            
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
        Destroy(healthSlider.gameObject);
        Destroy(coordinateSpace);
        Destroy(gameObject);
    }

    public void PausePlayback()
    {
        isPlaying = false;
    }


}
