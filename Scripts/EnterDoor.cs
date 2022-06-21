using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;
    private AudioSource _audioSource;
    private float _speedVolumeUp = 5f;

    public event UnityAction Reached
    {
        add => _reached?.AddListener(value);
        remove => _reached?.RemoveListener(value);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) == true)
        {
            _reached?.Invoke();
            _audioSource = GetComponent<AudioSource>();
        }
    }

    private void FixedUpdate()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 1f, Time.deltaTime / _speedVolumeUp);
    }
}
