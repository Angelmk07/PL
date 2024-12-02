using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.PlayerSystem
{
    public class PlayerListener : MonoBehaviour
    {
        public static Action Jump;
        private float horizontal;
        private Player player;
        private Movment movment;

        public void Construct(Player player, Movment movment)
        {
            this.player = player;
            this.movment = movment;
        }
        private void Update()
        {
            if (player.CanMove && !player.Dead)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    movment.Jump(player.PlayerRb, player.jumpPower);
                    Jump?.Invoke();
                }
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.R))
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            movment.Move(player.PlayerRb, Input.GetAxis("Horizontal"), player.speed, player.Maxspeed);
        }
    }
}