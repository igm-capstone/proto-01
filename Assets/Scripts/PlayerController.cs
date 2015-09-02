using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActorBehaviour))]
[RequireComponent(typeof(RecordBehavior))]
public class PlayerController : MonoBehaviour {

    public GameObject playbackPrefab;

    [SerializeField]
    [Range(1, 2)]
    short playerId = 1;

    ActorBehaviour actor;
    RecordBehavior recorder;

    Vector3 currentVelocity;

    public void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        recorder = GetComponent<RecordBehavior>();
    }

    public void Update()
    {
        var newVelocity = ReadPlayerInput();
        var actions = Actions.None;

        var newKeyFrame = WriteVelocityActions(newVelocity, out actions);
    
        if (newKeyFrame)
        {
            currentVelocity = newVelocity;

            actor.PerformActions(actions);
            recorder.RecordFrameAction(actions);
        }
    }

    private bool WriteVelocityActions(Vector3 velocity, out Actions actions, bool force = false)
    {
        var newKeyFrame = false;

        actions = Actions.None;
        if (force || (velocity.x != currentVelocity.x))
        {
            if (velocity.x == 0)
            {
                actions |= Actions.StopHorizontal;
            }
            else if (velocity.x == 1)
            {
                actions |= Actions.MoveRight;
            }
            else
            {
                actions |= Actions.MoveLeft;
            }

            newKeyFrame = true;
        }

        if (force || (velocity.z != currentVelocity.z))
        {
            if (velocity.z == 0)
            {
                actions |= Actions.StopVertical;
            }
            else if (velocity.z == 1)
            {
                actions |= Actions.MoveUp;
            }
            else
            {
                actions |= Actions.MoveDown;
            }

            newKeyFrame = true;
        }

        return newKeyFrame;
    }

    private Vector3 ReadPlayerInput()
    {

        // horizontal controls X axis while vertical controls Z axis
        var horizontal = Input.GetAxis("Horizontal_P" + playerId);
        var vertical = Input.GetAxis("Vertical_P" + playerId);

        var hasHorizontal = Mathf.Abs(horizontal) > 0.1f;
        var hasVertical = Mathf.Abs(vertical) > 0.1f;

        var velocity = Vector3.zero;

        velocity.x = hasHorizontal ? horizontal > 0 ? 1 : -1 : 0;
        velocity.z = hasVertical ? vertical > 0 ? 1 : -1 : 0;

        Debug.Log("process input");
        if (Input.GetButtonDown("A" + playerId))
        {
            Debug.Log("start recording" + playerId);
            recorder.StartRecording();
            var actions = Actions.None;
            WriteVelocityActions(currentVelocity, out actions, true);
            recorder.RecordFrameAction(actions);
        }

        if (Input.GetButtonUp("A" + playerId))
        {
            Debug.Log("stop recording " + playerId);
            recorder.StopRecording();
        }

        if (Input.GetButtonDown("X" + playerId))
        {
            Debug.Log("start playback" + playerId);
            InstantiatePlayback();
        }

        return velocity;
    }

    void InstantiatePlayback()
    {
        GameObject playbackGhost = Instantiate(playbackPrefab, transform.position + (currentVelocity == Vector3.zero ? Vector3.forward : currentVelocity), Quaternion.identity) as GameObject;
        playbackGhost.GetComponent<PlaybackBehavior>().StartPlayback(recorder.recordedFrames, PlaybackMode.RunOnce);
    }
}
