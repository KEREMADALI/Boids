
using FlockBehaviours;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GameObject BirdPrefab;
    public Transform InputCenter;
    public Transform FlightArea;
    public float Velocity;
    public int NumberOfBirds;

    private IFlockBehaviour m_Behaviour;
    private List<Bird> m_Birds;
    private const int m_XMax = 6;
    private const int m_YMax = 5;

    public Vector3 FlockCenter
    {
        get
        {
            Vector3 center = Vector3.zero;
            m_Birds.ForEach(x => center += (Vector3)x.Transform.position);
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
        AddBirdInRunTime();
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
            var randomPosition = new Vector3(UnityEngine.Random.Range(-m_XMax, m_XMax), UnityEngine.Random.Range(-m_YMax, m_YMax));
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
        var birdObject = Instantiate(BirdPrefab, position, rotation, transform);
        birdObject.name = name;

        // Add bird into list
        var birdTransform = birdObject.transform;
        Bird birdScript = new Bird(birdTransform);

        return birdScript;
    }

    /// <summary>
    /// Updates bird positions
    /// </summary>
    private void UpdateBirdPositions()
    {
        foreach (var bird in m_Birds)
        {
            var neighbours = bird.GetNeighbours();
            var obstacles = bird.GetObstacles();
            var moveVector = m_Behaviour.CalculateMove(bird.Transform, neighbours, obstacles);
            bird.Move(moveVector, Velocity);
        } 
    }

    /// <summary>
    /// Adds birds on run time with a mouse click
    /// </summary>
    private void AddBirdInRunTime()
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
