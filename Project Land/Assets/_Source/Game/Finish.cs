using _Source.Core;
using _Source.PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Source.Game
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private GameObject end;
        private Player player;
        private GeneratorLvl generator;
        private float time;
        private bool finish;
        private float distance = 0.4f;
        private void Update()
        {
            if (finish)
            {
                time += Time.deltaTime;
                MoveToFinish();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            finish = true;
            player.PlayerRb.velocity = Vector3.zero;
            player.PlayerRb.gravityScale = 0;
            player.SetBoolMove(false);
        }
        public void Cunstruct(Player player, GeneratorLvl generate)
        {
            this.player = player;
            generator = generate;
        }
        private void MoveToFinish()
        {
            float curveValue = curve.Evaluate(time);
            player.transform.position = Vector3.Lerp(player.transform.position, end.transform.position, curveValue * Time.deltaTime);

            float x = Mathf.Lerp(player.transform.position.x, end.transform.position.x, curveValue * Time.deltaTime * 0.5f);
            float y = Mathf.Lerp(player.transform.position.y, end.transform.position.y, 1 / (curveValue + 1) * Time.deltaTime * 0.5f);
            player.transform.position = new Vector3(x, y, player.transform.position.z);
            if (Vector2.Distance(player.transform.position, end.transform.position) < distance)
            {
                finish = false;
                player.SetBoolMove(true);
                player.PlayerRb.gravityScale = 1;
                generator.FinishEvent(gameObject);
                time = 0;
            }
        }
    }
}