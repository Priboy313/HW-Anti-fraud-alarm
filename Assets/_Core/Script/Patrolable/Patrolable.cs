using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Patrolable : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints = new();
    [SerializeField] private float _speed = 2;

    private int _currentWaypointIndex = 0;
    private float _reachDistance = 0.3f;
    private float _reachDistanceSqr;

    public Rigidbody Rigidbody { get; private set; }
    public Vector3 Position => transform.position;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (_waypoints.Count <= 0)
        {
            Debug.LogError("Waypoints is not set!");
            enabled = false;
        }

        _reachDistanceSqr = _reachDistance * _reachDistance;
    }

    private void FixedUpdate()
    {
        Vector3 currentWaypointPosition = _waypoints[_currentWaypointIndex].Position;
        Vector3 direction = (currentWaypointPosition - Position).normalized;
        Vector3 velocity = direction * _speed;

        Rigidbody.velocity = new Vector3(velocity.x, Rigidbody.velocity.y, velocity.z);

        float sqrDistance = (currentWaypointPosition - Position).sqrMagnitude;

        if (sqrDistance < _reachDistanceSqr)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
        }
    }
}
