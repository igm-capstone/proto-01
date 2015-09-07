﻿using UnityEngine;
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
    float speed = 5;

    public new Rigidbody rigidbody { get; private set; }

    Vector3 velocity;
    float jumpImpulse;

    bool jump;

    bool isGrounded;

    public float force = 20;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            rigidbody.AddForce(Vector3.up * (jumpImpulse * .2f), ForceMode.VelocityChange);
            jumpImpulse *= .8f;
        }

        if (isGrounded)
        {
            jumpImpulse = force;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Platform") {
            isGrounded = true;
        }

    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {
            isGrounded = false;
        }
    }

    public void PerformActions(float horizontal, float vertical, bool jump = false)
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

        this.jump = jump;
    }

    public void setSpeed (float value) {
        speed = value;
    }
}
