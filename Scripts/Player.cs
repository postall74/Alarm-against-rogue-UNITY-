using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string IsDead = "IsDead";

    [SerializeField] private TMP_Text _coinsCountText;

    private Animator _animator;
    private int _coins = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.6f)
                {
                    enemy.Die();
                }
                else
                {
                    Hurt();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Coin>())
        {
            _coins++;
            _coinsCountText.text = _coins.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void Hurt()
    {
        _animator.SetBool(IsDead, true);
        Debug.Log($"You are die");
        transform.position = new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z);
    }
}

