using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour 
{
    private const float SpeedUp = 5f;
    private const float SpeedDown = -5f;
    private const float SpeedZero = 0f;
    private const float ZeroGravityScale = 0f;
    private const float OneGravityScale = 1f;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _rigidbody2D.gravityScale = ZeroGravityScale;

        if (collision.GetComponent<Ladder>())
        {
            if (Input.GetKey(KeyCode.W))
            {
                _rigidbody2D.velocity = new Vector2(SpeedZero, SpeedUp);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _rigidbody2D.velocity = new Vector2(SpeedZero, SpeedDown);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(SpeedZero, SpeedZero);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _rigidbody2D.gravityScale = OneGravityScale;
    }
}
