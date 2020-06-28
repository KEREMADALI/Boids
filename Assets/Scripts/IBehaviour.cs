using UnityEngine;
using System.Collections;

public interface IBehaviour
{
    void Start(Transform transform);

    void Tick(Transform transform);
}
