
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    public class AlignmentBehaviour : IFlockBehaviour
    {
        public Vector2 CalculateMove(Transform transform, List<Transform> neighbours, List<Transform> obstacles)
        {
            Vector2 alignmentVector = transform.up;

            if (!neighbours.Any())
            {
                return alignmentVector;
            }

            alignmentVector = Vector2.zero;

            neighbours.ForEach(x => alignmentVector += (Vector2)x.up / neighbours.Count);

            return alignmentVector;
        }
    }
}