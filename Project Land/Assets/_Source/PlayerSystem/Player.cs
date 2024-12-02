using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace _Source.PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public float speed { get; private set; }
        [field: SerializeField] public float Maxspeed { get; private set; }
        [field: SerializeField] public float jumpPower { get; private set; }
        [field: SerializeField] public Rigidbody2D PlayerRb { get; private set; }
        [field: SerializeField] public GameObject PlayerGameobj { get; private set; }
        [field: SerializeField] public GameObject dead { get; private set; }
        [field: SerializeField] public Transform PlayerTransform { get; private set; }

        public bool isOnAir { get; private set; } = true;
        public bool CanMove { get; private set; } = true;
        [field: SerializeField] public bool Dead { get; private set; }

        private void Awake()
        {
            PlayerRb = GetComponent<Rigidbody2D>();
            PlayerTransform = transform;
            PlayerGameobj = gameObject;
        }
        private void OnCollisionEnter2D(Collision2D collision) => isOnAir = false;
        private void OnCollisionExit2D(Collision2D collision) => isOnAir = true;

        public void SetSclale(float value)
        {
            PlayerTransform.localScale = new Vector3(value, value, value);
        }
        public void SetBoolMove(bool value)
        {
            CanMove = value;
        }
        public void kill()
        {
            Dead = true;
            dead.SetActive(true);
        }
    }
}