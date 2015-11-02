using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActorBehaviour))]
[RequireComponent(typeof(RecordBehavior))]
public class PlayerController : MonoBehaviour
{

    public GameObject playbackPrefab;
    public int maxPlaybacks = 2;
    public bool isFPSEnabled;

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

    Vector3 currentVelocity, spawnPos, spawnScale, spawnRot;

    public void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        recorder = GetComponent<RecordBehavior>();

        // Record Player Start Position when the Scene Starts
        PlyrStartPos = GetComponent<Transform>().position;

        actor.setSpeed(speed);
        actor.setJumpForce(jumpForce);
        actor.SetFpsControls(isFPSEnabled);

    }

    void Start()
    {
        recorder.StartRecording();
    }
    public void Update()
    {
        float horizontal, vertical;
        bool jump;

        //FPS Controls
        bool fireWeapon;
        float camHorizontal, camVertical; 

        ReadPlayerInput(out horizontal, out vertical, out jump, out camHorizontal, out camVertical, out fireWeapon);
        actor.PerformActions(horizontal, vertical, camHorizontal, camVertical, jump, fireWeapon);

        if (recorder.IsRecording())
        {
            recorder.RecordFrameAction(horizontal, vertical, Mathf.Atan2(transform.forward.x, transform.forward.z), jump, fireWeapon);
        }
    }    

    private void ReadPlayerInput(out float horizontal, out float vertical, out bool jump, out float camHorizontal, out float camVertical, out bool fireWeapon)
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerId);
        vertical = Input.GetAxis("Vertical_P" + playerId);
        jump = Input.GetButton("Jump_P" + playerId);

        // FPS controls Input
        camHorizontal = Input.GetAxis("CamHorizontal_P" + playerId);
        camVertical = Input.GetAxis("CamVertical_P" + playerId);

        if (Mathf.Abs(Input.GetAxis("Fire_P" + playerId)) > float.Epsilon)
            fireWeapon = true;
        else
            fireWeapon = false;
    }
}
