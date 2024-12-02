using _Source.PlayerSystem;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace _Source.PlayerSystem
{
    public class LookForward : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private float lerpSpeed = 0.5f;
        [SerializeField] private float AVelocity = -90;
        [SerializeField] private float timeToReturn = 1.8f;
        [SerializeField] private float time;
        private const float targetZ = 0f;
        private const float targetThreshold = 0.05f;

        private void Awake() => player = GetComponent<Player>();

        private void OnEnable() => PlayerListener.Jump += StartFixingRotation;

        private void OnDisable() => PlayerListener.Jump -= StartFixingRotation;



        private void StartFixingRotation()
        {
            player.PlayerRb.angularVelocity = 0;
            time = timeToReturn + Time.time;
            StartCoroutine(FixRotation());
        }


        private void Update()
        {
            if (player.isOnAir)
            {
                float targetAngle = Mathf.Atan2(player.PlayerRb.velocity.y, player.PlayerRb.velocity.x) * Mathf.Rad2Deg;

                float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, 2 * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
            }
        }
        IEnumerator FixRotation()
        {
            while (time > Time.time)
            {

                Quaternion targetRotation = Quaternion.Euler(player.PlayerTransform.rotation.x, player.PlayerTransform.rotation.y, targetZ);
                player.PlayerTransform.rotation = Quaternion.Lerp(player.PlayerTransform.rotation, targetRotation, lerpSpeed * Time.deltaTime);

                yield return null;
            }



            yield return null;
        }
    }
}

//private void Update()
//{
//    if (isFixingRotation)
//    {
//        Quaternion targetRotation = Quaternion.Euler(player.PlayerTransform.rotation.x, player.PlayerTransform.rotation.y, targetZ);
//        player.PlayerTransform.rotation = Quaternion.Lerp(player.PlayerTransform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
//        if (math.abs(player.PlayerTransform.rotation.z - targetZ) < targetThreshold)
//        {
//            isFixingRotation = false;
//            player.PlayerRb.angularVelocity = 10;
//        }
//    }
//}

