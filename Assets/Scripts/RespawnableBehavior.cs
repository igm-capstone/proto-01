using UnityEngine;
using System.Collections;
using System;

public class RespawnableBehavior : MonoBehaviour {

    public Transform[] mRespawnPoint;
    public GameObject playerPrefab;

	public int maxSpawns = -1;
	public int currentSpawn;
    RecordBehavior recorder;
    Vector3 spawnPos, spawnScale, spawnRot;
    public GameObject playbackPrefab;


    public bool temp = false;
	// Use this for initialization
	void Start () 
    {
        recorder = GetComponent<RecordBehavior>();
		currentSpawn = maxSpawns;
		gameObject.SendMessage("loseLives");


        //change the spawn point to where the player spawns in the start of the game
        spawnPos = transform.position;
        spawnScale = transform.localScale;
        spawnRot = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentSpawn <= 0 && currentSpawn != -1) 
        {
			gameObject.SetActive(false);
			return;
		}
	
        if (transform.position.y < -10.0)
        {
            temp = true;
            do
            {
                GameObject floors = GameObject.Find ("Floors");
			    if(floors)
			    {
				    FloorManager flrMan = floors.GetComponent<FloorManager>();
				    flrMan.spawnFloors();
			    }
			    if(currentSpawn != -1)
				    currentSpawn--;
			    gameObject.SendMessage("loseLives");

                recorder.StopRecording();
                SpawnRecording();
            
                spawnPlayer();
            } while (temp == false);
			
            //recorder.StartRecording();
        }
	}

    void spawnPlayer()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        transform.position = mRespawnPoint[rand].position;
        transform.rotation = mRespawnPoint[rand].rotation;
        var rBody = GetComponent<Rigidbody>();
        if (rBody)
        {
            rBody.velocity = Vector3.zero;
        }
        ResetTransform(mRespawnPoint[rand]);
    }
    public void ResetTransform(Transform t)
    {
        spawnPos = t.position;
        spawnScale = t.localScale;
        spawnRot = t.rotation.eulerAngles;
    }
    void SpawnRecording()
    {
        GameObject coordinateSpace = new GameObject();
        coordinateSpace.transform.position = spawnPos;
        coordinateSpace.transform.eulerAngles = spawnRot;
        coordinateSpace.transform.localScale = spawnScale;


        //Set the transform to spawn position of the player
        GameObject playbackGhost = Instantiate(playbackPrefab) as GameObject;
        playbackGhost.GetComponent<PlaybackBehavior>().coordinateSpace = coordinateSpace;

        playbackGhost.GetComponent<ActorBehaviour>().setSpeed(GetComponent<PlayerController>().speed);
        playbackGhost.GetComponent<ActorBehaviour>().setJumpForce(GetComponent<PlayerController>().jumpForce);

        //Start Playback
        playbackGhost.GetComponent<PlaybackBehavior>().StartPlayback(recorder.recordedFrames, PlaybackMode.Loop, coordinateSpace);
    }
}