using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class Movement : MonoBehaviour
{
    private const float SpeedZero = 0f;
    private const float ZeroGravityScale = 0f;
    private const float OneGravityScale = 1f;
    private const float GroungRadius = 0.2f;
    private const float Up = 1f;
    private const float Down = -1f;
    private const float Right = 1f;
    private const float Left = -1f;
    private const string Speed = "Speed";
    private const string Crouch = "Crouch";
    private const string Rise = "Rise";
    private const string JumpSpeed = "JumpSpeed";
    private const string Jump = "Jump";
    private const string Climb = "Climb";
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private int _jumpForce;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private PlayerInput _playerInput;

    private float move;
    private bool _isJumped;
    private bool _isRise;
    private bool _isFasingRight = true;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if (_playerInput.Jump && _isJumped)
        {
            _animator.SetTrigger(Jump);
            Vector2 force = new Vector2(0, _jumpForce);
            _rigidbody2D.AddForce(force);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ladder>(out Ladder ladder))
        {
            _animator.SetBool(Climb, true);
            _rigidbody2D.gravityScale = ZeroGravityScale;

            if (_playerInput.Direction == Up)
            {
                move = _playerInput.Direction;
                _rigidbody2D.velocity = new Vector2(SpeedZero, _speed * move);

            }
            else if (_playerInput.Direction == Down)
            {
                move = _playerInput.Direction;
                _rigidbody2D.velocity = new Vector2(SpeedZero, _speed * move);
            }
            else
            {
                move = _playerInput.Direction;
                _animator.SetBool(Climb, false);
                _rigidbody2D.velocity = new Vector2(SpeedZero, _speed * move);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _rigidbody2D.gravityScale = OneGravityScale;
        _animator.SetBool(Climb, false);
    }

    private void FixedUpdate()
    {
        _isJumped = Physics2D.OverlapCircle(gameObject.transform.position, GroungRadius);
        _animator.SetFloat(JumpSpeed, _rigidbody2D.velocity.y);

        if (!_isJumped)
        {
            return;
        }

        move = _playerInput.Direction;
        _animator.SetFloat(Speed, Mathf.Abs(move));
        _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);

        if (move > 0 && !_isFasingRight)
        {
            Flip();
        }
        else if (move < 0 && _isFasingRight)
        {
            Flip();
        }
        else if (move == 0)
        {
            _animator.SetFloat(Speed, 0);
        }
    }

    private void Flip()
    {
        _isFasingRight = !_isFasingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }
}
