using UnityEngine;
using System.Collections;

public class RespawnableBehavior : MonoBehaviour {

    private Vector3 mRespawnPoint;

	// Use this for initialization
	void Start () {
        mRespawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -10.0)
        {
            transform.position = mRespawnPoint;
        }
	}
}
