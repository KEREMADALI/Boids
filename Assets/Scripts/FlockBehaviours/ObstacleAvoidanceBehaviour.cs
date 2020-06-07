using System.Collections.Generic;
using UnityEngine;

namespace FlockBehaviours
{
    internal class ObstacleAvoidanceBehaviour : IFlockBehaviour
    {

        public Vector2 CalculateMove(Transform transform, List<Transform> neighbours, List<Transform> obstacles)
        {
            return Vector2.zero;
        }
    }
}