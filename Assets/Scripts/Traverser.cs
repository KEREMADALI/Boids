
using UnityEngine;

public class Traverser : MonoBehaviour
{
    private const float s_RotationSpeed = 2;
    private const float s_Radius = 2f;
    private Vector2 m_CenterPosition;
    private float m_Angle;

    private void Awake()
    {
        m_CenterPosition = Vector2.zero;
        m_Angle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Angle += s_RotationSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(m_Angle), Mathf.Cos(m_Angle)) * s_Radius;
        transform.position =m_CenterPosition +  offset;
    }
}
