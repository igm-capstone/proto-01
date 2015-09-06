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

    public void PerformActions(float horizontal, float vertical)
    {
        if (Mathf.Abs(horizontal) > Mathf.Epsilon || Mathf.Abs(vertical) > Mathf.Epsilon)
        {
            transform.localRotation = Quaternion.Euler(0.0f, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0.0f);
            velocity = transform.forward * speed;
        }
        else
        {
            velocity = Vector3.zero;
        }   
    }
}
