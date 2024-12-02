using _Source.PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace _Source.DeadSystem
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerMaskUtil.ContainsLayer(mask, collision.gameObject))
            {
                collision.GetComponent<Player>().kill();

            }
        }
    }
}