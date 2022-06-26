using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private const float MinDistance = 1f;

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _enemyRigidbody2D;
    [SerializeField] private Transform[] _positionsPatrol;

    private bool _isFasingLeft = true;
    private int _targetPatrolPosition;
    private Enemy _enemy;

    private void Start()
    {
        _enemyRigidbody2D = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        if (_enemy != null)
        {
            if (_positionsPatrol.Length == 0)
            {
                transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            }
            else
            {
                _enemyRigidbody2D.position = Vector2.MoveTowards(transform.position, _positionsPatrol[_targetPatrolPosition].position, _speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, _positionsPatrol[_targetPatrolPosition].position) < MinDistance)
                {
                    if (_targetPatrolPosition != _positionsPatrol.Length - 1)
                    {
                        _targetPatrolPosition++;

                        if (!_isFasingLeft)
                            Flip();
                    }
                    else
                    {
                        _targetPatrolPosition = 0;

                        if (_isFasingLeft)
                            Flip();
                    }
                }
            }
        }
    }

    private void Flip()
    {
        _isFasingLeft = !_isFasingLeft;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }
}