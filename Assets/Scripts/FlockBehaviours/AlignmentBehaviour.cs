
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlockBehaviours
{
    public class AlignmentBehaviour : IFlockBehaviour
    {
        public Vector3 CalculateMove(Transform transform, IEnumerable<Transform> neighbours, IEnumerable<Transform> obstacles)
        {
            Vector3 alignmentVector = transform.up;

            if (!neighbours.Any())
            {
                return alignmentVector;
            }

            alignmentVector = Vector3.zero;

            neighbours.ToList().ForEach(x => alignmentVector += x.up);
            alignmentVector /= neighbours.Count();

            return alignmentVector;
        }
    }
}