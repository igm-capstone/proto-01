﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActorBehaviour))]
[RequireComponent(typeof(RecordBehavior))]
[RequireComponent(typeof(PlayerHUD))]
public class PlayerController : MonoBehaviour {

    public GameObject playbackPrefab;
    public int maxPlaybacks = 2;

    [SerializeField]
    [Range(1, 2)]
    short playerId = 1;
    int availablePlaybacks = 0;

    ActorBehaviour actor;
    RecordBehavior recorder;
    PlayerHUD hud;

    Vector3 currentVelocity;

    public void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        recorder = GetComponent<RecordBehavior>();
        hud = GetComponent<PlayerHUD>();
    }

    public void Update()
    {
        float horizontal, vertical;
        ReadPlayerInput(out horizontal, out vertical);
        actor.PerformActions(horizontal, vertical);

        if (recorder.IsRecording())
        {
            recorder.RecordFrameAction(horizontal, vertical, Mathf.Atan2(transform.forward.x, transform.forward.z));
        }
    }

    private void ReadPlayerInput(out float horizontal, out float vertical)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);

        if (Input.GetButtonDown("A" + playerId))
        {
            //Debug.Log("start recording" + playerId);
            recorder.StartRecording();
        }

        if (Input.GetButtonUp("A" + playerId))
        {
            //Debug.Log("stop recording " + playerId);
            recorder.StopRecording();
        }

        if (Input.GetButtonDown("X" + playerId))
        {
            //Debug.Log("start playback" + playerId);
            InstantiatePlayback();
        }
    }

    void InstantiatePlayback()
    {
        GameObject coordinateSpace = new GameObject();
        coordinateSpace.transform.position = transform.position + transform.forward;
        coordinateSpace.transform.rotation = transform.rotation;
        coordinateSpace.transform.localScale = transform.localScale;

        GameObject playbackGhost = Instantiate(playbackPrefab) as GameObject;
        playbackGhost.GetComponent<PlaybackBehavior>().coordinateSpace = coordinateSpace;
        playbackGhost.transform.localScale = coordinateSpace.transform.localScale;
        playbackGhost.transform.position = coordinateSpace.transform.position;
        playbackGhost.transform.rotation = coordinateSpace.transform.rotation;
        playbackGhost.transform.localRotation = Quaternion.identity;
        playbackGhost.GetComponent<PlaybackBehavior>().StartPlayback(recorder.recordedFrames, PlaybackMode.RunOnce);
    }

    public void resetPlaybackCounter()
    {
        availablePlaybacks = maxPlaybacks;
        hud.setPlaybackCounter(availablePlaybacks);
    }

}
