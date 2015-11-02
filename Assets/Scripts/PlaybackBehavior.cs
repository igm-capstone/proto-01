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
    public FPSFrame[] recordedFrames;
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
    private GameObject startPosition;

	void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        r = GetComponentInChildren<Renderer>();

        screenCanvas = FindObjectOfType<Canvas>();

        healthSlider = Instantiate(healthPrefab) as Slider;
        healthSlider.transform.SetParent(screenCanvas.transform, false);
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
                //Vector3 t = Quaternion.AngleAxis(a * Mathf.Rad2Deg, Vector3.up) * v;
                //actor.PerformActions(v.x, v.z, recordedFrames[frameCount].mJump);

                frameCount++;
            }
            else
            {
                if (mode == PlaybackMode.RunOnce)
                    //StopPlayback();
                    Debug.Log("abc");
                else if (mode == PlaybackMode.Loop)
                    StartPlayback(recordedFrames, PlaybackMode.Loop, startPosition);
            }

            Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            healthSlider.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            healthSlider.value = 1.0f - ((float)frameCount / (float)recordedFrames.Length);

            //Color current = r.materials[0].color;
            //current.a = 1.0f - ((float)frameCount / (float)recordedFrames.Length);
            //r.materials[0].color = current;

        }
    }

    public void StartPlayback (FPSFrame[] recordedFrames, PlaybackMode mode, GameObject startPos) 
    {
        frameCount = 0;
        startPosition = startPos;
        transform.localScale = startPos.transform.localScale;
        transform.position = startPos.transform.position;
        transform.rotation = startPos.transform.rotation;
        transform.localRotation = Quaternion.identity;
        this.recordedFrames = recordedFrames;
        this.mode = mode;
        this.isPlaying = true;
    }

    //public void StopPlayback()
    //{
    //    isPlaying = false;
    //    Destroy(healthSlider.gameObject);
    //    Destroy(coordinateSpace);
    //    Destroy(gameObject);
    //}

    //public void PausePlayback()
    //{
    //    isPlaying = false;
    //}
}
