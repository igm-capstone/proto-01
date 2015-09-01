using UnityEngine;
using System;

[Flags]
public enum Actions
{
    None = 0,

    MoveRight = (1 << 0),
    MoveLeft = (1 << 1),
    StopHorizontal = (1 << 2),

    MoveUp = (1 << 3),
    MoveDown = (1 << 4),
    StopVertical = (1 << 5),
}

public class ActorBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    float speed = 5;

    public new Rigidbody rigidbody { get; private set; }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void PerformActions(Actions action)
    {
        var velocity = rigidbody.velocity;

        if ((action & Actions.MoveRight) != 0)
        {
            velocity.x = speed;
        }

        if ((action & Actions.MoveLeft) != 0)
        {
            velocity.x = -speed;
        }

        if ((action & Actions.StopHorizontal) != 0)
        {
            velocity.x = 0;
        }

        if ((action & Actions.MoveUp) != 0)
        {
            velocity.z = speed;
        }

        if ((action & Actions.MoveDown) != 0)
        {
            velocity.z = -speed;
        }

        if ((action & Actions.StopVertical) != 0)
        {
            velocity.z = 0;
        }

        rigidbody.velocity = velocity;
    }
}
