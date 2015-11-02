using UnityEngine;
using System.Collections;
using System;

public class RespawnableBehavior : MonoBehaviour {

    public Transform[] mRespawnPoint;
	public int maxRecordingSpawns = -1;
    //[NonSerialized]
    public int recordingSpawnCount;
    RecordBehavior recorder;
    Vector3 spawnPos, spawnScale, spawnRot;
    public GameObject playbackPrefab;
    private bool temp = true;

	void Start () 
    {
        recorder = GetComponent<RecordBehavior>();
		recordingSpawnCount = maxRecordingSpawns;

        //change the spawn point to where the player spawns in the start of the game
        spawnPos = mRespawnPoint[0].transform.position;
        spawnScale = mRespawnPoint[0].transform.localScale;
        spawnRot = mRespawnPoint[0].transform.rotation.eulerAngles;

        gameObject.transform.position = spawnPos;
        gameObject.transform.localScale = spawnScale;
        gameObject.transform.eulerAngles = spawnRot;
        recorder.StartRecording();
	}
	
	void Update () 
    {
        //Stop Recording if recording limit reaches 5
        if (recordingSpawnCount <= 0 && recordingSpawnCount != -1) 
        {
			gameObject.SetActive(false);
			return;
		}
	
        //TODO when the player dies
        if (transform.position.y < -10.0)
        {
            do
            {
                recorder.StopRecording();

                if (recordingSpawnCount != 0)
                {
                    recordingSpawnCount--;
                    SpawnRecording();
                }
                
                SpawnPlayer();
            } while (temp == false);
        }
	}

    void SpawnPlayer()
    {
        int rand = UnityEngine.Random.Range(0, mRespawnPoint.Length);
        transform.position = mRespawnPoint[rand].position;
        transform.rotation = mRespawnPoint[rand].rotation;
        var rBody = GetComponent<Rigidbody>();
        if (rBody)
        {
            rBody.velocity = Vector3.zero;
        }
        StoreSpawnTransform(mRespawnPoint[rand]);
        recorder.StartRecording();
    }
    public void StoreSpawnTransform(Transform t)
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