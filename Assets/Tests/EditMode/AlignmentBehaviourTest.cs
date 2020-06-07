
using FlockBehaviours;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    public class AlignmentBehaviourTest
    {
        [Test]
        public void AlignsWithOneNeighbourSuccessfully()
        {
            // Arrange 
            var neighbourBird = new GameObject();
            neighbourBird.AddComponent<Transform>();
            neighbourBird.transform.up = new Vector3(0, -1, 0);
            var neighbourList = new List<Transform>() { neighbourBird.transform };

            var bird = new GameObject();
            bird.AddComponent<Transform>();

            var alignmentBehaviour = new AlignmentBehaviour();

            // Action
            var calculatedMove = alignmentBehaviour.CalculateMove(bird.transform, neighbourList, null);

            // Assert
            Assert.AreEqual(neighbourBird.transform.up, (Vector3)calculatedMove);
        }

        [Test]
        public void AlignsWithTwoNeighboursSuccessfullly()
        {
            // Arrange
            var rotationVector = new Vector3(0, 0.5f, 0);
            var neighbourBird_0 = new GameObject();
            neighbourBird_0.AddComponent<Transform>();
            neighbourBird_0.transform.up = new Vector3(0, 1, 0);
            var neighbourBird_1 = new GameObject();
            neighbourBird_1.AddComponent<Transform>();
            neighbourBird_1.transform.up = new Vector3(0, -1, 0);

            var neighbourList = new List<Transform>() { neighbourBird_0.transform, neighbourBird_1.transform };

            var bird = new GameObject();
            bird.AddComponent<Transform>();

            var alignmentBehaviour = new AlignmentBehaviour();

            // Action
            var calculatedMove = alignmentBehaviour.CalculateMove(bird.transform, neighbourList, null);

            // Assert
            Assert.AreEqual(Vector3.zero, (Vector3)calculatedMove);
        }
    }
}
