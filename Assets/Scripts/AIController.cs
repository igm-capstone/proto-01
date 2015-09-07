using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour 
{

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    void FixedUpdate()
    {
        //
    }

    void Die()
    {
        GetComponent<PlaybackBehavior>().StopPlayback();
    }
}
