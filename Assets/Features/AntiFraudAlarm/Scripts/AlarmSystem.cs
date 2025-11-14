using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSource;
    [SerializeField] private float _volumeMax = 1f;
    [SerializeField] private float _volumeMin = 0f;
    [SerializeField] private float _volumeChangingSpeed = 0.2f;

    private Coroutine _currentDecreasingCoroutine;

    private void Awake()
    {
        if (GetComponent<Collider>().isTrigger != true)
        {
            Debug.LogError("Alarm Trigger not set!");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fraud>(out _))
        {
            if (_currentDecreasingCoroutine != null)
            {
                StopCoroutine(_currentDecreasingCoroutine);
            }

            _alarmSource.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Fraud>(out _))
        {
            if (_alarmSource.volume < _volumeMax)
            {
                _alarmSource.volume += _volumeChangingSpeed * Time.fixedDeltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Fraud>(out _))
        {
            _currentDecreasingCoroutine = StartCoroutine(DecreaseVolumeAndStopAlarm());
        }
    }

    private IEnumerator DecreaseVolumeAndStopAlarm()
    {
        var wait = new WaitForFixedUpdate();

        while (_alarmSource.volume > _volumeMin)
        {
            _alarmSource.volume -= _volumeChangingSpeed * Time.fixedDeltaTime;

            yield return wait;
        }

        _alarmSource.volume = _volumeMin;
        _alarmSource.Stop();
    }
}
