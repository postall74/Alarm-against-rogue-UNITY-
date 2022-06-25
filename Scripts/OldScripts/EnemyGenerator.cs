using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGenerator : MonoBehaviour
{
    private const float Latency = 2f;

    [SerializeField] private Enemy _enemy;
    [SerializeField] private UnityEvent _created;

    private Coroutine _enemyCoroutine;

    public event UnityAction Created
    {
        add => _created?.AddListener(value);
        remove => _created?.RemoveListener(value);
    }

    private void Start()
    {
        StartCreateEnemy();
    }

    public void StartCreateEnemy()
    {
        _enemyCoroutine = StartCoroutine(CreateEnemy());
    }

    public IEnumerator CreateEnemy()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(Latency);

        while (!gameObject.GetComponentInChildren<Enemy>())
        {
            Enemy newEnemy = Instantiate(_enemy, transform.position, Quaternion.identity);
            _created.Invoke();
            yield return wait;
        }
    }
}