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

    Vector3 velocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    public void PerformActions(Actions actions)
    {
        velocity = rigidbody.velocity;

        if ((actions & Actions.MoveRight) != 0)
        {
            velocity.x = speed;
        }

        if ((actions & Actions.MoveLeft) != 0)
        {
            velocity.x = -speed;
        }

        if ((actions & Actions.StopHorizontal) != 0)
        {
            velocity.x = 0;
        }

        if ((actions & Actions.MoveUp) != 0)
        {
            velocity.z = speed;
        }

        if ((actions & Actions.MoveDown) != 0)
        {
            velocity.z = -speed;
        }

        if ((actions & Actions.StopVertical) != 0)
        {
            velocity.z = 0;
        }
    }
}
