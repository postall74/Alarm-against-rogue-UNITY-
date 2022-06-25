using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    private Animator _animator;
    private Coroutine _cherryTaken;
    private WaitForSecondsRealtime _wait = new WaitForSecondsRealtime(1f);

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>())
        {
            StartCherryTake();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void StartCherryTake()
    {
        _cherryTaken = StartCoroutine(CherryTake());
    }

    private IEnumerator CherryTake()
    {
        while (GetComponent<Animator>().enabled)
        {
            GetComponent<Animator>().Play("Cherry Feedback");
            yield return _wait;
        }
    }
}
