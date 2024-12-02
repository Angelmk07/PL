using _Source.ObjectScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Source.ObjectScripts
{
    public class Rock : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        [SerializeField] private float distance = 5f;
        private Vector2 defaultPos;

        private void OnEnable()
        {


            SetEndLine();
        }
        private void Start()
        {
            GetComponentInParent<LvlReturn>().Gameobjectactive += SetEndLine;
            GetComponentInParent<LvlReturn>().Gameobjectactive += ReturnObj;
            defaultPos = transform.localPosition;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, transform.position);

            SetEndLine();
        }

        private void ReturnObj()
        {
            transform.localPosition = defaultPos;
        }

        private void SetEndLine()
        {
            defaultPos = transform.localPosition;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;

            lineRenderer.SetPosition(0, transform.position);

            Vector2 rayDirection = transform.up;
            Vector2 rayOrigin = transform.position;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, distance);

            if (hit.collider != null)
            {
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                lineRenderer.SetPosition(1, rayOrigin + rayDirection * distance);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, transform.up);
        }
    }
}