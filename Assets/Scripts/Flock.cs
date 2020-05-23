
using FlockBehaviours;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject BirdPrefab;
    public Transform InputCenter;
    public float Velocity;
    public int NumberOfBirds;
    public int LimitRadius;

    private IFlockBehaviour m_Behaviour;
    private List<Bird> m_Birds;
    private const int m_XMax = 6;
    private const int m_YMax = 5;

    public Vector2 FlockCenter
    {
        get
        {
            Vector2 center = Vector2.zero;
            m_Birds.ForEach(x => center += (Vector2)x.Transform.position);
            center /= m_Birds.Count;
            return center;
        }
    }

    private void Start()
    {
        m_Behaviour = new CompositeBehaviour(this);
        InitBirds();
    }

    private void Update()
    {
        UpdateBirdPositions();
    }

    private void InitBirds()
    {
        m_Birds = new List<Bird>();

        for (var i = 0; i < NumberOfBirds; i++)
        {
            // Get random initialization values
            var randomPosition = new Vector3(Random.Range(-m_XMax, m_XMax), Random.Range(-m_YMax, m_YMax));
            var randomRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

            // Instantiate a new bird
            var birdObject = Instantiate(BirdPrefab, randomPosition, randomRotation);
            birdObject.name = "Bird_" + i;

            // Add bird into list
            var birdTransform = birdObject.transform;
            var birdSpriteRenderer = birdObject.GetComponent<SpriteRenderer>();
            Bird birdScript = new Bird(birdTransform, birdSpriteRenderer);
            m_Birds.Add(birdScript);
        }   
    }

    private void UpdateBirdPositions()
    {
        foreach (var bird in m_Birds)
        {
            var neighbours = bird.GetNeighbours();
            var moveVector = m_Behaviour.CalculateMove(bird.Transform, neighbours);
            bird.Move(moveVector, Velocity);
        }
    }
}
