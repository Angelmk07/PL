using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace _Source.PlayerSystem
{
    public class Movment
    {
        public void Jump(Rigidbody2D rigidbody2D, float force)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(Vector3.up * force, ForceMode2D.Impulse);
        }
        public void Move(Rigidbody2D playerRb, float direction, float speed, float maxSpeed)
        {
            playerRb.velocity = new Vector2(math.clamp(playerRb.velocity.x + direction * speed * Time.deltaTime, -maxSpeed, maxSpeed), playerRb.velocity.y);
        }
        public void Move(Transform player, float direction, float speed)
        {
            player.position += (Vector3)new Vector2(direction * speed * Time.deltaTime, 0);
        }

    }
}