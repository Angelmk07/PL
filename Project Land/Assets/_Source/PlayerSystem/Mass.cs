using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.PlayerSystem
{
    public class Mass : MonoBehaviour
    {
        public Vector2 positionMass;
        private Rigidbody2D rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb.centerOfMass = new Vector2(positionMass.x * transform.localScale.x, positionMass.y * transform.localScale.y);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(GetComponent<Rigidbody2D>().worldCenterOfMass, 0.1f);
        }
    }
}