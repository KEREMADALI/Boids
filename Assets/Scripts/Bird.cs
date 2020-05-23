using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(CircleCollider2D))]
public class Bird
{
    private readonly float m_NeighbourRadius = 1f;

    private List<Transform> m_Neighbours;
    private SpriteRenderer m_SpriteRenderer;

    internal Transform Transform;

    internal Bird(Transform transform, SpriteRenderer spriteRenderer)
    {
        m_Neighbours = new List<Transform>();
        m_SpriteRenderer = spriteRenderer;
        Transform = transform;
    }

    internal void Move(Vector2 velocityVector, float velocity)
    {
        if (velocityVector == Vector2.zero)
        {
            return;
        }

        Transform.up = Vector2.Lerp(Transform.up, velocityVector, Time.time/10);

        var velocityMagnitude = velocityVector.magnitude > 3f ? 2f : velocityVector.magnitude;

        velocityVector = velocityVector.magnitude > 3f ? velocityVector.normalized * 2f : velocityVector;
        Transform.position += (Vector3)(velocity * velocityVector * Time.deltaTime);
    }

    internal List<Transform> GetNeighbours()
    {
        m_Neighbours.Clear();

        var neighbourColliders = Physics2D.OverlapCircleAll(Transform.position, m_NeighbourRadius);

        if (neighbourColliders == null)
        {
            return m_Neighbours;
        }

        foreach (var neighbourCollider in neighbourColliders)
        {
            m_Neighbours.Add(neighbourCollider.transform);
        }

        if (m_Neighbours.Contains(Transform))
        {
            m_Neighbours.Remove(Transform);
        }

        var neighboursBehind = GetNeighboursBehind(Transform, m_Neighbours);
        m_Neighbours = m_Neighbours.Except(neighboursBehind).ToList();

        return m_Neighbours;
    }

    private IEnumerable<Transform> GetNeighboursBehind(Transform localTransform, List<Transform> neighbours)
    {
        var neighboursBehind = new List<Transform>();

        if (!neighbours.Any())
        {
            return neighboursBehind;
        }

        foreach (var neighbour in neighbours)
        {
            Vector3 direction = neighbour.transform.position - localTransform.position;
            direction = neighbour.transform.InverseTransformDirection(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = (angle + 360) % 360; 

            if (angle > 210 && angle < 330)
            {
                neighboursBehind.Add(neighbour);
            }
        }

        return neighboursBehind;
    }
}
