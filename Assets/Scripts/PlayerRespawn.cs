using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

    void OnTriggerEnter(Collider Other)
    {
        if (Other.tag == "Player")
        {
            Other.transform.position = Other.GetComponent<PlayerController>().PlyrStartPos;
        }
    }
}
