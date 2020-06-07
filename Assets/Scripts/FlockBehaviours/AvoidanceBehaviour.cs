
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    public class AvoidanceBehaviour : IFlockBehaviour
    {
        private const float s_AvoidanceDistance = 0.5f;

        public Vector2 CalculateMove(Transform birdTransform, List<Transform> neighbours, List<Transform> obstacles)
        {
            Vector2 avoidanceVector = Vector2.zero;

            var closeNeighbours = neighbours.Where(x => Vector2.Distance(birdTransform.position, x.position) < s_AvoidanceDistance);

            if (!closeNeighbours.Any())
            {
                return avoidanceVector;
            }

            foreach (var closeNeighbour in closeNeighbours)
            {
                if (birdTransform.position == closeNeighbour.position)
                {
                    // If two birds are at the same location 
                    return new Vector2(Random.Range(1, 5), Random.Range(1, 5)).normalized;
                }             

                avoidanceVector += (Vector2)(birdTransform.position - closeNeighbour.position);
            }

            avoidanceVector = avoidanceVector / closeNeighbours.Count();
            avoidanceVector = (s_AvoidanceDistance - avoidanceVector.magnitude) / s_AvoidanceDistance * avoidanceVector.normalized;

            return avoidanceVector;
        }
    }
}