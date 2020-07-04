using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class Bird
{
    private const string s_NeighbourTag = "Bird";
    private const float s_NeighbourRadius = 1f;

    private List<Transform> m_Neighbours;
    private Vector3 m_LastVector = Vector3.zero;

    internal Transform Transform;

    internal Bird(Transform transform) 
    {
        m_Neighbours = new List<Transform>();
        Transform = transform;
    }

    internal void Move(Vector3 velocityVector, float velocity)
    {
        m_LastVector = velocityVector;

        if (velocityVector == Vector3.zero)
        {
            return;
        }

        Transform.up = Vector3.Lerp(Transform.up, velocityVector, Time.time/10);

        velocityVector = velocityVector.magnitude > 3f ? velocityVector.normalized * 2f : velocityVector;
        Transform.position += (velocity * velocityVector * Time.deltaTime);
    }

    internal List<Transform> GetNeighbours()
    {
        m_Neighbours.Clear();

        var closeObjects = Physics2D.OverlapCircleAll(Transform.position, s_NeighbourRadius);
        var closeNeighbours = closeObjects.Where(x => x.gameObject.tag == s_NeighbourTag);

        foreach (var neighbour in closeObjects) 
        {
            m_Neighbours.Add(neighbour.transform);
        }

        if (m_Neighbours.Contains(Transform))
        {
            m_Neighbours.Remove(Transform);
        }

        return m_Neighbours;
    }

    internal List<Transform> GetObstacles()
    {
        var obstacles = new List<Transform>();
        RaycastHit2D hit = Physics2D.Raycast(Transform.position, -Vector2.up);

        if (hit.collider == null || hit.transform.gameObject.CompareTag(s_NeighbourTag))
        {
            return obstacles;
        }

        float distance = Vector3.SqrMagnitude((Vector3)hit.point- Transform.position);

        if (distance < 3)
        {
            return obstacles;
        }

        obstacles.Add(hit.transform);

        return obstacles;
    }
}
