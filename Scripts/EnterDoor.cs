using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;

    private AudioSource _audioSource;
    private float _speedVolumeUp = 3f;
    private float _maxVolume = 1f;

    public event UnityAction Reached
    {
        add => _reached?.AddListener(value);
        remove => _reached?.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _reached?.Invoke();
            _audioSource = GetComponent<AudioSource>();

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, Time.deltaTime / _speedVolumeUp);
        }
    }
}
