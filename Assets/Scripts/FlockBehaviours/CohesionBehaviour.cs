
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    public class CohesionBehaviour : IFlockBehaviour
    {
        private float m_SmoothTime = 0.7f;
        private Vector3 m_Velocity;

        public Vector3 CalculateMove(
            Transform birdTransform,
            IEnumerable<Transform> neighbours,
            IEnumerable<Transform> obstacles)
        {
            var cohesionVector = Vector3.zero;

            if (!neighbours.Any())
            {
                return cohesionVector;
            }

            foreach (var neighbour in neighbours)
            {
                cohesionVector += (Vector3)neighbour.position;
            }

            cohesionVector = cohesionVector / neighbours.Count();
            cohesionVector -= birdTransform.position;

            cohesionVector = Vector3.SmoothDamp(birdTransform.up, cohesionVector, ref m_Velocity, m_SmoothTime);

            return cohesionVector;
        }
    }
}