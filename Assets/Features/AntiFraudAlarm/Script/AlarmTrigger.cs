using System;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action ActionFraudEnter;
    public event Action ActionFraudExit;

    private void Awake()
    {
        if (GetComponent<Collider>().isTrigger == false)
        {
            Debug.LogError("Trigger not set!");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Fraud>(out _))
        {
            ActionFraudEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Fraud>(out _))
        {
            ActionFraudExit?.Invoke();
        }
    }
}
