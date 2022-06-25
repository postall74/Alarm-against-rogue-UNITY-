using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    private Animator _animator;
    private Collider2D _collider2D;
    private Coroutine _cherryTaken;
    private WaitForSecondsRealtime _wait = new WaitForSecondsRealtime(1f);

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>())
        {
            StartCherryTake();
        }
    }

    private void StartCherryTake()
    {
        _cherryTaken = StartCoroutine(CherryTake());
    }

    private IEnumerator CherryTake()
    {
        _animator.Play("Cherry Feedback");
        yield return _wait;
        Destroy(gameObject);
    }
}
