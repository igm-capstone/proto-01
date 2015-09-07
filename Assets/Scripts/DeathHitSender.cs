using UnityEngine;
using System.Collections;

public class DeathHitSender : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("Player")) 
            || (other.gameObject.layer == LayerMask.NameToLayer("Ghost")))
        {
            if (other.transform.parent == null)
            {
                return;
            }

            other.transform.parent.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
        }
    }
}
