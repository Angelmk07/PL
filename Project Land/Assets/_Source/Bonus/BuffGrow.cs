using _Source.ObjectScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace _Source.Bonus
{
    public class BuffGrow : MonoBehaviour
    {
        [SerializeField] private float value;
        [SerializeField] private float GravityValue;
        [SerializeField] private float Max = 3f;
        [SerializeField] private float Min = 0.15f;
        [SerializeField] private LayerMask mask;
        private void Start()
        {
            GetComponentInParent<LvlReturn>().Gameobjectnonactive += returnBuff;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (LayerMaskUtil.ContainsLayer(mask, collision.gameObject))
            {
                collision.gameObject.transform.localScale = new Vector3(
                        Mathf.Clamp(collision.gameObject.transform.localScale.x + value, Min, Max),
                        Mathf.Clamp(collision.gameObject.transform.localScale.y + value, Min, Max),
                        Mathf.Clamp(collision.gameObject.transform.localScale.z + value, Min, Max));

                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = Mathf.Clamp(GravityValue +
                    collision.gameObject.GetComponent<Rigidbody2D>().gravityScale, Min, Max);
                gameObject.SetActive(false);

            }
        }
        public void returnBuff()
        {
            gameObject.SetActive(true);
        }
    }
}