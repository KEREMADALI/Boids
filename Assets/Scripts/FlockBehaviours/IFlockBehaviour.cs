
using System.Collections.Generic;
using UnityEngine;

namespace FlockBehaviours
{
    public interface IFlockBehaviour
    {
        Vector2 CalculateMove(Transform transform, List<Transform> neighbours, List<Transform> obstacles);
    }
}