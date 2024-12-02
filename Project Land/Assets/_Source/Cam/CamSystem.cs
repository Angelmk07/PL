using _Source.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace _Source.Cam
{
    public class CamSystem : MonoBehaviour
    {
        [SerializeField] Transform Player;
        [SerializeField] Vector3 Left;
        [SerializeField] Vector3 Right;
        [SerializeField] Vector3 Up;
        [SerializeField] Vector3 Down;
        [SerializeField] float RightMove = 0.1f;
        [SerializeField] float speed = 2;
        [SerializeField] int xdixtance = 2;
        [SerializeField] int maxDistanceToPlayer = 2;
        [SerializeField] int substructDistanceToEnd = 6;
        [SerializeField] GeneratorLvl generator;
        private void Update()
        {
            Right = new Vector3(generator.transform.position.x - substructDistanceToEnd, 0, 0);
            Vector3 newPosition = Player.position + new Vector3(xdixtance, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, Left.x, Right.x);
            newPosition.y = Mathf.Clamp(newPosition.y, Down.y, Up.y);
            newPosition.z = transform.position.z;
            if (newPosition.x > gameObject.transform.position.x + maxDistanceToPlayer)
            {
                transform.position = new Vector3(
                    math.lerp(transform.position.x, newPosition.x, Time.deltaTime),
                    math.lerp(transform.position.y, newPosition.y, Time.deltaTime),
                    newPosition.z);
            }
            else
            {
                transform.position += new Vector3(RightMove * speed * Time.deltaTime, 0, 0);
                transform.position = new Vector3(transform.position.x, math.lerp(transform.position.y, newPosition.y, Time.deltaTime), transform.position.z);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Right, Left);
            Gizmos.DrawLine(Up, Down);
        }
    }
}