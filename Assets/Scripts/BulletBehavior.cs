using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{

    // Time to self destruct
    public float projDestroyTime = 3.0f;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(KillSelf());
    }

    // Kills itself after some time.
    IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(projDestroyTime);
        //GameObject.Destroy(this);
        Destroy(this.gameObject);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.tag == "Ghost" || collision.gameObject.tag == "BluePlayer" || collision.gameObject.tag == "RedPlayer" || collision.gameObject.tag == "Platform")
    //    Destroy(this.gameObject);
    //}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost" || other.tag == "BluePlayer" || other.tag == "RedPlayer" || other.tag == "Platform")
            Destroy(this.gameObject);
    }
}
