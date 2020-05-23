
using System.Collections.Generic;
using UnityEngine;

namespace FlockBehaviours
{
    public class LimitAvoidanceBehaviour : IFlockBehaviour
    {
        private readonly float s_LimitRadius;
        private Transform s_InputCenter;

        public LimitAvoidanceBehaviour(int limitRadius, Transform inputCenter)
        {
            s_LimitRadius = limitRadius;
            s_InputCenter = inputCenter;
        }

        public Vector2 CalculateMove(Transform birdTransform, List<Transform> neighbours)
        {
            var moveVector = Vector2.zero;

            if (Vector2.Distance(Vector2.zero, birdTransform.position) < s_LimitRadius)
            {
                return moveVector;
            }

            var randomCenter = new Vector2(Random.Range(0,1), Random.Range(0,1));
            moveVector = (Vector2)s_InputCenter.position +  randomCenter -(Vector2)birdTransform.position;
            moveVector = (moveVector.magnitude - s_LimitRadius) * moveVector.normalized;

            if (moveVector.magnitude > 1)
            {
                moveVector = moveVector.normalized;
            }

            Debug.Log($"Limit avoidancevector {moveVector.magnitude}");

            return moveVector;
        }   
    }
}