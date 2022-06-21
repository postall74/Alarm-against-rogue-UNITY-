using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WentOutDoor : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;
    private AudioSource _audioSource;
    private float _speedVolumeDown = 5f;
    private float _minVolume = 0f;

    public event UnityAction Reached
    {
        add => _reached?.AddListener(value);
        remove => _reached?.RemoveListener(value);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _reached?.Invoke();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, Time.deltaTime / _speedVolumeDown);
        }
    }
}
