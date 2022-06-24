using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private UnityEvent _enterDoor;
    [SerializeField] private UnityEvent _wentOutDoor;


    public event UnityAction EterDoor
    {
        add => _enterDoor?.AddListener(value);
        remove => _enterDoor?.RemoveListener(value);
    }

    public event UnityAction WentOutDoor
    {
        add => _wentOutDoor.AddListener(value);
        remove => _wentOutDoor.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _enterDoor?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _wentOutDoor?.Invoke();
        }
    }
}