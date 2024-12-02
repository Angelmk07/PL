using _Source.PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace _Source.PlayerSystem
{
    public class CheakerCollision : MonoBehaviour
    {
        [SerializeField] private Player pl;
        [field: SerializeField] public float Radius { get; private set; } = 0.2f;
        [field: SerializeField] public float Pos { get; private set; } = 2.5f;
        [field: SerializeField] public float XUp1 { get; private set; } = 2.5f;
        [field: SerializeField] public float XUp2 { get; private set; } = 2.5f;
        [field: SerializeField] public float XDown1 { get; private set; } = 2.5f;
        [field: SerializeField] public float XDown2 { get; private set; } = 2.5f;
        [field: SerializeField] public LayerMask LayersForDown { get; private set; }
        [field: SerializeField] public LayerMask LayersForUp { get; private set; }
        private bool UpCollision;
        private bool DownCollision;
        private void Update()
        {
            transform.position = pl.transform.position;
            transform.localScale = pl.transform.localScale;
            Collider2D[] down = Physics2D.OverlapCircleAll(gameObject.transform.position - new Vector3(transform.up.x + XDown1,
            transform.up.y * transform.localScale.y,
                transform.up.z * transform.localScale.z) / Pos,
                Radius * transform.localScale.x);
            FindCollision(down, ref DownCollision, LayersForDown);

            transform.position = pl.transform.position;
            Collider2D[] down2 = Physics2D.OverlapCircleAll(gameObject.transform.position - new Vector3(transform.up.x + XDown2,
            transform.up.y * transform.localScale.y,
                transform.up.z * transform.localScale.z) / Pos,
                Radius * transform.localScale.x);
            FindCollision(down, ref DownCollision, LayersForDown);

            Collider2D[] up = Physics2D.OverlapCircleAll(gameObject.transform.position + new Vector3(transform.up.x + XUp1,
            transform.up.y * transform.localScale.y,
                transform.up.z * transform.localScale.z) / Pos,
                Radius * transform.localScale.x);
            FindCollision(up, ref UpCollision, LayersForUp);
            Collider2D[] up2 = Physics2D.OverlapCircleAll(gameObject.transform.position + new Vector3(transform.up.x + XUp2,
    transform.up.y * transform.localScale.y,
        transform.up.z * transform.localScale.z) / Pos,
        Radius * transform.localScale.x);
            FindCollision(up, ref UpCollision, LayersForUp);
        }
        public void InvokeDead()
        {
            if (DownCollision && UpCollision)
            {
                pl.kill();
            }
        }
        public void FindCollision(Collider2D[] Colliders, ref bool isColision, LayerMask layer)
        {
            foreach (Collider2D col in Colliders)
            {
                if (LayerMaskUtil.ContainsLayer(layer, col.gameObject))
                {
                    isColision = true;
                    InvokeDead();
                    return;
                }
            }
            isColision = false;
        }
        private void OnDrawGizmos()
        {
            if (pl != null)
            {
                Gizmos.DrawSphere(gameObject.transform.position - new Vector3(transform.up.x + XDown1,
                        transform.up.y * transform.localScale.y,
                            transform.up.z * transform.localScale.z) / Pos,
                            Radius * transform.localScale.x);

                Gizmos.DrawSphere(gameObject.transform.position - new Vector3(transform.up.x + XDown2,
                        transform.up.y * transform.localScale.y,
                            transform.up.z * transform.localScale.z) / Pos,
                            Radius * transform.localScale.x);

                Gizmos.DrawSphere(gameObject.transform.position + new Vector3(transform.up.x + XUp1,
                    transform.up.y * transform.localScale.y,
                        transform.up.z * transform.localScale.z) / Pos,
                        Radius * transform.localScale.x);
                Gizmos.DrawSphere(gameObject.transform.position + new Vector3(transform.up.x + XUp2,
                    transform.up.y * transform.localScale.y,
                    transform.up.z * transform.localScale.z) / Pos,
                    Radius * transform.localScale.x);
            }

        }
    }
}