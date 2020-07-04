
using System.Collections.Generic;
using UnityEngine;

namespace FlockBehaviours
{
    public interface IFlockBehaviour
    {
        Vector3 CalculateMove(
            Transform transform, 
            IEnumerable<Transform> neighbours, 
            IEnumerable<Transform> obstacles);
    }
}