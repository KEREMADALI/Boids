
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    public class CohesionBehaviour : IFlockBehaviour
    {
        private float m_SmoothTime = 0.7f;
        private Vector2 m_Velocity;

        public Vector2 CalculateMove(Transform birdTransform, List<Transform> neighbours)
        {
            var cohesionVector = Vector2.zero;

            if (!neighbours.Any())
            {
                return cohesionVector;
            }

            foreach (var neighbour in neighbours)
            {
                cohesionVector += (Vector2)neighbour.position;
            }

            cohesionVector = cohesionVector / neighbours.Count;
            cohesionVector -= (Vector2)birdTransform.position;

            cohesionVector = Vector2.SmoothDamp(birdTransform.up, cohesionVector, ref m_Velocity, m_SmoothTime);

            Debug.Log($"Cohesion vector: {cohesionVector.magnitude}");

            return cohesionVector;
        }
    }
}