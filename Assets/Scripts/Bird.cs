using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Data;

[RequireComponent(typeof(CircleCollider2D))]
public class Bird
{
    private readonly float s_NeighbourRadius = 1f;

    private List<Transform> m_Neighbours;

    internal Transform Transform;

    private Vector2 m_LastVector = Vector2.zero;

    internal Bird(Transform transform) 
    {
        m_Neighbours = new List<Transform>();
        Transform = transform;
    }

    internal void Move(Vector2 velocityVector, float velocity)
    {
        m_LastVector = velocityVector;

        if (velocityVector == Vector2.zero)
        {
            return;
        }

        Transform.up = Vector2.Lerp(Transform.up, velocityVector, Time.time/10);

        velocityVector = velocityVector.magnitude > 3f ? velocityVector.normalized * 2f : velocityVector;
        Transform.position += (Vector3)(velocity * velocityVector * Time.deltaTime);
    }

    internal List<Transform> GetNeighbours()
    {
        m_Neighbours.Clear();

        var closeNeighburs = Physics2D.OverlapCircleAll(Transform.position, s_NeighbourRadius);

        foreach (var neighbour in closeNeighburs) 
        {
            m_Neighbours.Add(neighbour.transform);
        }

        if (m_Neighbours.Contains(Transform))
        {
            m_Neighbours.Remove(Transform);
        }

        return m_Neighbours;
    }

}
