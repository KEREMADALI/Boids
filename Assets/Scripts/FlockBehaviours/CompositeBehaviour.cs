
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    internal class CompositeBehaviour : IFlockBehaviour
    {
        private List<IFlockBehaviour> m_Behaviours;

        internal CompositeBehaviour(Flock flock)
        {
            m_Behaviours = new List<IFlockBehaviour>
            {
                new AlignmentBehaviour(),
                new AvoidanceBehaviour(),
                new CohesionBehaviour(),
                new LimitAvoidanceBehaviour(flock.FlightArea, flock.InputCenter),
                //new ObstacleAvoidanceBehaviour()
            };
        }

        public Vector3 CalculateMove(
            Transform transform,
            IEnumerable<Transform> neighbours,
            IEnumerable<Transform> obstacles)
        {
            var moveVector = Vector3.zero;

            if (!m_Behaviours.Any())
            {
                return moveVector;
            }

            foreach (var behaviour in m_Behaviours)
            {
                moveVector += behaviour.CalculateMove(transform, neighbours, obstacles);
            }

            return moveVector;
        }
    }
}