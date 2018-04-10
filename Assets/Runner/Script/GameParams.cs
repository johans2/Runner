using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameParams : ScriptableObject {

    public float RunSpeed = 10;

    public Vector2 jumpForce = new Vector2(0f, 1f);
}
