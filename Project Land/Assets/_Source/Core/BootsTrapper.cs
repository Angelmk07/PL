using _Source.Core;
using _Source.Game;
using _Source.PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Source.Core
{
    public class BootsTrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerListener playerL;
        [SerializeField] private Finish finish;
        [SerializeField] private GeneratorLvl generate;
        private Movment movment;
        private void Awake()
        {
            movment = new();
            playerL.Construct(player, movment);
            finish.Cunstruct(player, generate);

        }
    }
}