using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplayData : ReplayData
{
    public Vector2 movement { get; private set;}
    public bool facingRight {get; private set;}
    public bool deathThisFrame{get; private set;}
    public PlayerReplayData(Vector3 position, Vector2 movement, bool facingRight, bool deathThisFrame)
    {
        this.position = position;
        this.movement = movement;
        this.facingRight = facingRight;
        this.deathThisFrame = deathThisFrame;
    }
}
