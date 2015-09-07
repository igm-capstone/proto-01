using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActorBehaviour))]
[RequireComponent(typeof(RecordBehavior))]
[RequireComponent(typeof(PlayerHUD))]
public class PlayerController : MonoBehaviour {

    public GameObject playbackPrefab;
    public int maxPlaybacks = 2;

    [SerializeField]
    [Range(1f, 20f)]
    public float speed = 5;

    [SerializeField]
    public float jumpForce = 20;

    public Vector3 PlyrStartPos;

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

        // Record Player Start Position when the Scene Starts
        PlyrStartPos = GetComponent<Transform>().position;

        actor.setSpeed(speed);
        actor.setJumpForce(jumpForce);
    }

    public void Update()
    {
        float horizontal, vertical;
        bool jump;
        ReadPlayerInput(out horizontal, out vertical, out jump);
        actor.PerformActions(horizontal, vertical, jump);

        if (recorder.IsRecording())
        {
            recorder.RecordFrameAction(horizontal, vertical, Mathf.Atan2(transform.forward.x, transform.forward.z), jump);
        }
    }

    void Die()
    {
        transform.position = GetComponent<RespawnableBehavior>().mRespawnPoint;
    }

	void loseLives()
	{
		hud.setLives (gameObject.GetComponent<RespawnableBehavior> ().currentSpawn);
	}

    private void ReadPlayerInput(out float horizontal, out float vertical, out bool jump)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);

        if (Input.GetButtonDown("Record_P" + playerId))
        {
            //Debug.Log("start recording" + playerId);
            recorder.StartRecording();
        }

        if (Input.GetButtonUp("Record_P" + playerId))
        {
            //Debug.Log("stop recording " + playerId);
            recorder.StopRecording();
        }

        if (Input.GetButtonDown("Playback_P" + playerId) && !recorder.isRecording)
        {
            //Debug.Log("start playback" + playerId);
            InstantiatePlayback();
        }

        jump = Input.GetButton("Jump_P" + playerId);
    }

    void InstantiatePlayback()
    {
        if (availablePlaybacks > 0)
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
            playbackGhost.GetComponent<ActorBehaviour>().setSpeed(speed);
            playbackGhost.GetComponent<ActorBehaviour>().setJumpForce(jumpForce);
            playbackGhost.GetComponent<PlaybackBehavior>().StartPlayback(recorder.recordedFrames, PlaybackMode.RunOnce);

            availablePlaybacks--;
            hud.setPlaybackCounter(availablePlaybacks);
        }
        
    }

    public void resetPlaybackCounter()
    {
        availablePlaybacks = maxPlaybacks;
        hud.setPlaybackCounter(availablePlaybacks);
    }

}
