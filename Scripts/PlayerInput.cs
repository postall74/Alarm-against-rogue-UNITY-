using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public float DirectionHorizontal { get; private set; }
    public float DirectionVertical { get; private set; }
    public bool Jump { get; private set; }

    private void Update()
    {
        DirectionHorizontal = Input.GetAxisRaw(Horizontal);
        DirectionVertical = Input.GetAxisRaw(Vertical);
        Jump = Input.GetKey(KeyCode.Space);
    }
}
