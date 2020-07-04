
using System.Collections.Generic;
using UnityEngine;

namespace FlockBehaviours
{
    public class LimitAvoidanceBehaviour : IFlockBehaviour
    {
        private Transform m_FlightArea;
        private Transform m_InputCenter;

        public LimitAvoidanceBehaviour(Transform flightArea, Transform inputCenter)
        {
            m_FlightArea = flightArea;
            m_InputCenter = inputCenter;
        }

        public Vector3 CalculateMove(
            Transform birdTransform,
            IEnumerable<Transform> neighbours,
            IEnumerable<Transform> obstacles)
        {
            var moveVector = Vector3.zero;
            var limitRadius = m_FlightArea.localScale.x;

            if (Vector3.SqrMagnitude(birdTransform.position) < (limitRadius * limitRadius))
            {
                return moveVector;
            }

            var randomCenter = new Vector3(Random.Range(0,1), Random.Range(0,1));
            moveVector = m_InputCenter.position +  randomCenter -birdTransform.position;
            moveVector = (moveVector.magnitude - limitRadius) * moveVector.normalized;

            if (moveVector.magnitude > 1)
            {
                moveVector = moveVector.normalized;
            }

            return moveVector;
        }   
    }
}