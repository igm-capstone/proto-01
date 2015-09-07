using UnityEngine;
using System.Collections;
using System;

public class RespawnableBehavior : MonoBehaviour {

    [NonSerialized]
    public Vector3 mRespawnPoint;

	// Use this for initialization
	void Start () {
        mRespawnPoint = transform.position;
        Debug.Log(this.name + ": " + mRespawnPoint.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -10.0)
        {
            transform.position = mRespawnPoint;
        }
	}
}
