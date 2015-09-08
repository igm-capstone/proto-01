using UnityEngine;
using System.Collections;
using System;

public class RespawnableBehavior : MonoBehaviour {

    [NonSerialized]
    public Vector3 mRespawnPoint;

	public int maxSpawns = -1;
	public int currentSpawn;

	// Use this for initialization
	void Start () {
		currentSpawn = maxSpawns;
        mRespawnPoint = transform.position;
		gameObject.SendMessage("loseLives");
//        Debug.Log(this.name + ": " + mRespawnPoint.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        if (currentSpawn <= 0 && currentSpawn != -1) {
			gameObject.SetActive(false);
			return;
		}
		if (transform.position.y < -10.0)
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
            transform.position = mRespawnPoint;
			var rBody = GetComponent<Rigidbody>();
			if (rBody) 
			{
				rBody.velocity = Vector3.zero;
			}
        }
	}
}