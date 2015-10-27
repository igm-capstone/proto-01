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
    public short playerId = 1;
    int availablePlaybacks = 0;

    ActorBehaviour actor;
    RecordBehavior recorder;
    PlayerHUD hud;

    Vector3 currentVelocity, spawnPos, spawnScale, spawnRot;

    public void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        recorder = GetComponent<RecordBehavior>();
        hud = GetComponent<PlayerHUD>();

        // Record Player Start Position when the Scene Starts
        PlyrStartPos = GetComponent<Transform>().position;

        actor.setSpeed(speed);
        actor.setJumpForce(jumpForce);
        //spawnPos = transform.position;
        //spawnScale = transform.localScale;
        //spawnRot = transform.rotation.eulerAngles;
    }

    void Start()
    {
        recorder.StartRecording();
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

   

	void loseLives()
	{
		hud.setLives (gameObject.GetComponent<RespawnableBehavior> ().currentSpawn);
	}

    private void ReadPlayerInput(out float horizontal, out float vertical, out bool jump)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);

        if (Input.GetKeyDown(KeyCode.G))
        {
            recorder.StartRecording();
        }

        jump = Input.GetButton("Jump_P" + playerId);
    }
    
    /*
    public void ResetTransform(Transform t)
    {
        spawnPos = t.position;
        spawnScale = t.localScale;
        spawnRot = t.rotation.eulerAngles;
    }
    public void InstantiatePlayback()
    {
        if (availablePlaybacks > 0)
        {
            GameObject coordinateSpace = new GameObject();
            coordinateSpace.transform.position = spawnPos;
            coordinateSpace.transform.eulerAngles = spawnRot;
            coordinateSpace.transform.localScale = spawnScale;


            //Set the transform to spawn position of the player
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
    */

    public void resetPlaybackCounter()
    {
        availablePlaybacks = maxPlaybacks;
        hud.setPlaybackCounter(availablePlaybacks);
    }
}
