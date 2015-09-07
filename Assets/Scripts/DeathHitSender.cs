using UnityEngine;
using System.Collections;

public class DeathHitSender : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Player")) 
            || (other.gameObject.layer == LayerMask.NameToLayer("Ghost")))
        {
            other.transform.parent.SendMessage("Die");
        }
    }
}
