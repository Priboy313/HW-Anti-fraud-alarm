using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceHandler : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(float speed, float targetVolume)
    {
        TryStopCurrentCoroutine();

        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        StartCoroutine(ChangeVolume(speed, targetVolume));
    }

    public void Stop(float speed, float targetVolume = 0f)
    {
        TryStopCurrentCoroutine();

        StartCoroutine(ChangeVolume(speed, targetVolume, loop: false));
    }

    private bool TryStopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        return true;
    }

    private IEnumerator ChangeVolume(float speed, float targetVolume, bool loop = true)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, speed * Time.deltaTime);

            yield return null;
        }

        _audioSource.volume = targetVolume;
        
        if (loop == false)
        {
            _audioSource.Stop();
        }
    }
}
