using _Source.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace _Source.ObjectScripts
{
    public class LvlReturn : MonoBehaviour
    {
        private GeneratorLvl generate;
        private List<GameObject> pool;
        [SerializeField] private LayerMask layerMask;
        public Action Gameobjectactive;
        public Action Gameobjectnonactive;
        private void OnEnable()
        {
            Gameobjectactive?.Invoke();
        }
        private void OnDisable()
        {
            Gameobjectnonactive?.Invoke();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (LayerMaskUtil.ContainsLayer(layerMask, collision.gameObject))
            {
                generate.ReturnObjectToPool(pool, gameObject);
            }
        }
        public void Construct(ref List<GameObject> list, GeneratorLvl generator)
        {
            generate = generator;
            pool = list;
            layerMask = LayerMask.GetMask("Player");
        }
    }
}