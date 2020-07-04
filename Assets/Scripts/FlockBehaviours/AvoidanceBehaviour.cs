
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    public class AvoidanceBehaviour : IFlockBehaviour
    {
        protected readonly float s_AvoidanceDistance = .25f;

        public Vector3 CalculateMove(
            Transform birdTransform, 
            IEnumerable<Transform> neighbours, 
            IEnumerable<Transform> obstacles)
        {
            Vector3 avoidanceVector = Vector3.zero;

            var closeNeighbours = neighbours
                .Where(x => Vector3.SqrMagnitude(birdTransform.position - x.position) < s_AvoidanceDistance);

            if (!closeNeighbours.Any())
            {
                return avoidanceVector;
            }

            foreach (var closeNeighbour in closeNeighbours)
            {
                if (birdTransform.position == closeNeighbour.position)
                {
                    // If two birds are at the same location 
                    return new Vector3(Random.Range(1, 5), Random.Range(1, 5)).normalized;
                }             

                avoidanceVector += birdTransform.position - closeNeighbour.position;
            }

            avoidanceVector = avoidanceVector / closeNeighbours.Count();
            avoidanceVector = (s_AvoidanceDistance - avoidanceVector.magnitude) 
                / s_AvoidanceDistance * avoidanceVector.normalized;

            return avoidanceVector;
        }
    }
}