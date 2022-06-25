using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _speedVolumeChange = 0.001f;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _volume;

    public IEnumerator VolumeChange(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedVolumeChange);
            yield return null;
        }
    }
    public void StartVolumeUp()
    {
        if (_volume != null)
        {
            StopCoroutine(_volume);
        }
        _volume = StartCoroutine(VolumeChange(_maxVolume));
    }

    public void StartVolumeDown()
    {
        if (_volume != null)
        {
            StopCoroutine(_volume);
        }
        _volume = StartCoroutine(VolumeChange(_minVolume));
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartVolumeUp();
        StartVolumeDown();
    }
}