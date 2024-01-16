using System;
using Pathfinding;
using UnityEngine;

[Serializable]
public class EnemyAI
{
    [SerializeField] private Seeker seeker;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private float nextWaypointDistance = .5f;

    private Transform target;
    private Path path;
    private int currentWaypoint;

    public void Initialize(Transform target)
    {
        this.target = target;
    }
    
    public void UpdatePath()
    {
        if (seeker.IsDone() && target)
        {
            seeker.StartPath(rigidbody2d.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
    
    public Vector2 CalculatePath()
    {
        if (path == null)
        {
            return Vector2.zero;
        }

        if (target == null)
        {
            return Vector2.zero;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return Vector2.zero;
        }
        
        float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance && currentWaypoint < path.vectorPath.Count - 1)
        {
            currentWaypoint++;
        }
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;

        return direction;
    }
}
