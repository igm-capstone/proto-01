using UnityEngine;
using System.Collections;

public class SwitchControl : MonoBehaviour 
{
    FloorManager floorManObj;

    void Start()
    {
        floorManObj = GameObject.Find("FloorBase").GetComponent<FloorManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit");

        if (other.gameObject.tag == "Ghost")
        {
            floorManObj.trapControl(gameObject.name);                        
        }
    }
}
