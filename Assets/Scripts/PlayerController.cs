using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    public GameObject playbackPrefab;

    [SerializeField]
    [Range(1, 2)]
    short playerId = 1;

    [SerializeField]
    [Range(1f, 20f)]
    float speed = 5;

    Rigidbody _rigidbody;
    new Rigidbody rigidbody
    {
        get { return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody>()); }
    }

    Vector3 lastVelocity;

    public void Update()
    {
        // horizontal controls X axis while vertical controls Z axis
        var horizontal = Input.GetAxis("Horizontal_P" + playerId);
        var vertical = Input.GetAxis("Vertical_P" + playerId);

        var hasHorizontal = Mathf.Abs(horizontal) > 0.1f;
        var hasVertical = Mathf.Abs(vertical) > 0.1f;

        var newVelocity = Vector3.zero;

        if (!hasHorizontal)
        {
            newVelocity.x = 0;
        }
        else if (horizontal > 0)
        {
            newVelocity.x = speed;
        }
        else
        {
            newVelocity.x = -speed;
        }

        if (!hasVertical)
        {
            newVelocity.z = 0;
        }
        else if (vertical > 0)
        {
            newVelocity.z = speed;
        }
        else
        {
            newVelocity.z = -speed;
        }

        rigidbody.velocity = newVelocity;
    }

    void InstantiatePlayback()
    {
        GameObject playbackGhost = Instantiate(playbackPrefab, transform.position + transform.forward, Quaternion.identity) as GameObject;
        playbackGhost.GetComponent<PlaybackBehavior>().StartPlayback(GetComponent<RecordBehavior>().recordedFrames, PlaybackMode.RunOnce);
    }
}
