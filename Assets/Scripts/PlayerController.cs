﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ActorBehaviour))]
[RequireComponent(typeof(RecordBehavior))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    [Range(1, 2)]
    short playerId = 1;

    ActorBehaviour actor;
    RecordBehavior recorder;

    Vector3 currentVelocity;

    public void Awake()
    {
        actor = GetComponent<ActorBehaviour>();
        recorder = GetComponent<RecordBehavior>();
    }

    public void Update()
    {
        var newVelocity = ReadPlayerInput();
        var actions = Actions.None;

        if (newVelocity.x != currentVelocity.x)
        {
            if (newVelocity.x == 0)
            {
                actions |= Actions.StopHorizontal;
            }
            else if (newVelocity.x == 1)
            {
                actions |= Actions.MoveRight;
            }
            else
            {
                actions |= Actions.MoveLeft;
            }
        }
        
        if (newVelocity.z != currentVelocity.z)
        {
            if (newVelocity.z == 0)
            {
                actions |= Actions.StopVertical;
            }
            else if (newVelocity.z == 1)
            {
                actions |= Actions.MoveUp;
            }
            else
            {
                actions |= Actions.MoveDown;
            }
        }

        actor.PerformActions(actions);
        recorder.RecordFrameAction(actions);
    }

    private Vector3 ReadPlayerInput()
    {
        // horizontal controls X axis while vertical controls Z axis
        var horizontal = Input.GetAxis("Horizontal_P" + playerId);
        var vertical = Input.GetAxis("Vertical_P" + playerId);

        var hasHorizontal = Mathf.Abs(horizontal) > 0.1f;
        var hasVertical = Mathf.Abs(vertical) > 0.1f;

        var velocity = Vector3.zero;

        velocity.x = hasHorizontal ? horizontal > 0 ? 1 : -1 : 0;
        velocity.z = hasVertical ? vertical > 0 ? 1 : -1 : 0;

        return velocity;
    }
}
