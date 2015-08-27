using UnityEngine;
using System.Collections;

public class PlyrCtrl : MonoBehaviour {

    //Movement Variables
    float horDir, verDir;
    float horSpd = 10.0f;
    float verSpd = 3.0f;

    // Components
    Rigidbody plrRgdBody;

	
    // Use this for initialization
	void Awake ()
    {
        plrRgdBody = transform.GetComponent<Rigidbody>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        #region Input Manager
        horDir = Input.GetAxis("Horizontal");
        verDir = Input.GetAxis("Vertical");

        #endregion


        // Update player speed
        plrRgdBody.velocity = new Vector2(horSpd * horDir, plrRgdBody.velocity.y);

    }
}
