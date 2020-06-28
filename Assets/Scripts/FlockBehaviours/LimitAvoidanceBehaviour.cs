
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

        public Vector2 CalculateMove(Transform birdTransform, List<Transform> neighbours, List<Transform> obstacles)
        {
            var moveVector = Vector2.zero;
            var limitRadius = m_FlightArea.localScale.x;

            if (Vector3.SqrMagnitude(birdTransform.position) < (limitRadius * limitRadius))
            {
                return moveVector;
            }

            var randomCenter = new Vector2(Random.Range(0,1), Random.Range(0,1));
            moveVector = (Vector2)m_InputCenter.position +  randomCenter -(Vector2)birdTransform.position;
            moveVector = (moveVector.magnitude - limitRadius) * moveVector.normalized;

            if (moveVector.magnitude > 1)
            {
                moveVector = moveVector.normalized;
            }

            return moveVector;
        }   
    }
}