
using FlockBehaviours;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject BirdPrefab;
    public Transform InputCenter;
    public Transform FlightArea;
    public float Velocity;
    public int NumberOfBirds;
    [Range(180,360)]
    public int AngleOfView;

    internal int LimitRadius;

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
        AddRunTimeBird();
    }

    /// <summary>
    /// Initiates a flock of birds
    /// </summary>
    private void InitBirds()
    {
        m_Birds = new List<Bird>();

        for (var i = 0; i < NumberOfBirds; i++)
        {
            // Get random initialization values
            var randomPosition = new Vector3(Random.Range(-m_XMax, m_XMax), Random.Range(-m_YMax, m_YMax));
            var randomRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            var bird = InitBird("Bird_" + i, randomPosition, randomRotation);
            m_Birds.Add(bird);
        }   
    }

    /// <summary>
    /// Initiates a bird object and returns its bird script
    /// </summary>
    /// <param name="name"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private Bird InitBird(string name, Vector3 position, Quaternion rotation)
    {
        // Instantiate a new bird
        var birdObject = Instantiate(BirdPrefab, position, rotation);
        birdObject.name = name;

        // Add bird into list
        var birdTransform = birdObject.transform;
        var birdSpriteRenderer = birdObject.GetComponent<SpriteRenderer>();
        Bird birdScript = new Bird(birdTransform, birdSpriteRenderer);

        return birdScript;
    }

    /// <summary>
    /// Updates bird positions
    /// </summary>
    private void UpdateBirdPositions()
    {
        foreach (var bird in m_Birds)
        {
            var neighbours = bird.GetNeighbours(AngleOfView);
            var moveVector = m_Behaviour.CalculateMove(bird.Transform, neighbours, null);
            bird.Move(moveVector, Velocity);
        }
    }

    /// <summary>
    /// Adds birds on run time with a mouse click
    /// </summary>
    private void AddRunTimeBird()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        // Get positon from mouse and a random rotation
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        var randomRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        var bird = InitBird("Bird_" + m_Birds.Count, newPosition, randomRotation);

        // Add new bird into list
        m_Birds.Add(bird);
        NumberOfBirds++;
    }
}
