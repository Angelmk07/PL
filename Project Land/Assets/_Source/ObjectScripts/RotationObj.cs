using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObj : MonoBehaviour
{
    [SerializeField] int index =1;
    [SerializeField] int Speed =1;
    [SerializeField] Vector3 direction;

    void Update()
    {
        transform.Rotate(direction * Speed * index);
    }
}
