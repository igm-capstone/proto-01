using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour 
{
    public GameObject TeleportTo, objBeingTeleported;
    public bool isTeleporting = false;

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, TeleportTo.transform.position,Color.magenta);
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "BluePlayer" || other.tag == "BlueGhost") && isTeleporting == false && other.transform.parent.GetComponent<ActorBehaviour>().justTeleported == false)
        {
            other.transform.parent.position = TeleportTo.transform.position;
            TeleportTo.GetComponent<Teleport>().isTeleporting = true;
            other.transform.parent.GetComponent<ActorBehaviour>().justTeleported = true;
            StartCoroutine("Delay", 0.3f);
        }
        else
        {
            StartCoroutine("Delay", 0.3f);
            other.transform.parent.GetComponent<ActorBehaviour>().justTeleported = false;
            isTeleporting = false;
        }
    }

    IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
