using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    [SerializeField]
    private string word;

    [SerializeField]
    private float holdTime;

    private Vector3 direction;

    private float speed = 1f;

    void Update()
    {
        transform.position += direction * speed;
    }
}
