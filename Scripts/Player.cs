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
                if (point.collider.transform.position.y > point.otherCollider.transform.position.y)
                {
                    Hurt();
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Coin>(out Coin coin))
        {
            _coins++;
            _coinsCountText.text = _coins.ToString();
        }
    }

    public void Hurt()
    {
        _animator.SetBool(IsDead, true);
        Debug.Log($"You are die");
    }
}

