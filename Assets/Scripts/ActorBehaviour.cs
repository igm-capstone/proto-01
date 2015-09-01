using UnityEngine;
using System;

[Flags]
public enum Actions
{
    MoveUp = (1 << 0),
    MoveDown = (1 << 1),
    StopVertical = (1 << 2),

    MoveLeft = (1 << 3),
    MoveRight = (1 << 4),
    StopHorizontal = (1 << 5),
}

public class ActorBehaviour : MonoBehaviour
{
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

    public void PerformAction(Actions action)
    {
        var velocity = rigidbody.velocity;


        if ((action & Actions.MoveUp) != 0)
        {
                velocity.x = -speed;
        }

        
        rigidbody.velocity = velocity;
    }

    public void MoveUp()
    {
        var velocity = rigidbody.velocity;

        velocity.y = speed;

        rigidbody.velocity = velocity;
    }

    public void MoveDown()
    {
        var velocity = rigidbody.velocity;

        velocity.y = -speed;

        rigidbody.velocity = velocity;
    }


    public void StopVertical()
    {
        var velocity = rigidbody.velocity;

        velocity.y = 0;

        rigidbody.velocity = velocity;
    }

    public void StopHorizontal()
    {
        var velocity = rigidbody.velocity;

        velocity.x = 0;

        rigidbody.velocity = velocity;
    }

}
