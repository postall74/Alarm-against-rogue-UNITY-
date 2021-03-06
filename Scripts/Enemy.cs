using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string IsDead = "IsDead";

    private Animator _animator;
    private bool _isDead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.collider.transform.position.y > point.otherCollider.transform.position.y)
                {
                    Die();
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (_isDead)
            _animator.SetBool(IsDead, true);
    }

    public void Die()
    {
        _isDead = true;
        Destroy(gameObject, 1f);
    }
}