using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _jumpForce;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private bool _isJumped = false;
    private bool _isFasingRight = true;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isJumped)
        {
            _animator.SetTrigger("Jump");
            Vector2 jump = new Vector2(0, _jumpForce);
            _rigidbody2D.AddForce(jump);
        }
    }

    private void FixedUpdate()
    {
        _isJumped = Physics2D.OverlapCircle(gameObject.transform.position, 0.2f, 3);
        _animator.SetFloat("JumpSpeed", _rigidbody2D.velocity.y);

        if (!_isJumped)
        {
            return;
        }

        float move = Input.GetAxis("Horizontal");
        _animator.SetFloat("Speed", Mathf.Abs(move));
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
            _animator.SetFloat("Speed", 0);
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
