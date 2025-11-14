using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSourceHandler _alarmSource;
    [SerializeField] private AlarmTrigger _alarmTrigger;

    [SerializeField] private float _volumeMax = 1f;
    [SerializeField] private float _volumeMin = 0f;
    [SerializeField] private float _volumeChangingSpeed = 0.2f;

    private void Awake()
    {
        bool hasErrors = false;

        if (_alarmTrigger == null)
        {
            Debug.LogError("Alarm Trigger not set!");
            hasErrors = true;
        }

        if (_alarmSource == null)
        {
            Debug.LogError("Alarm Source not set!");
            hasErrors = true;
        }

        enabled = !hasErrors;
    }

    private void OnEnable()
    {
        _alarmTrigger.ActionFraudEnter += OnFraudEnter;
        _alarmTrigger.ActionFraudExit += OnFraudExit;
    }

    private void OnDisable()
    {
        _alarmTrigger.ActionFraudEnter -= OnFraudEnter;
        _alarmTrigger.ActionFraudExit -= OnFraudExit;
    }

    private void OnFraudEnter()
    {
        _alarmSource.Play(_volumeChangingSpeed, _volumeMax);
    }

    private void OnFraudExit()
    {
        _alarmSource.Stop(_volumeChangingSpeed, _volumeMin);
    }
}
