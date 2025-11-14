using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints = new List<Waypoint>();

    public int WaypointCount => _waypoints.Count;

    public Waypoint GetWaypoint(int index)
    {
        if (WaypointCount == 0)
        {
            return null;
        }

        return _waypoints[index % WaypointCount];
    }
}
