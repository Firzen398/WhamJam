﻿using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    [SerializeField]
    private string word;

    private BubbleManager bubbleManager;

    private Rigidbody2D rigidbody;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float speed = 1f;

    private float maxX;
    private float maxY;

    private float startedTime;
    private float popTime;
    private float duration;

    [SerializeField]
    private TextMesh textMesh;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>() == null ? gameObject.AddComponent<Rigidbody2D>() : GetComponent<Rigidbody2D>(); ;
        rigidbody.AddForce(direction*speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (Time.time >=  popTime)
            BubblePop();
    }

    private void OnPopBubble()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        bubbleManager.PopBubble(this);
    }

    private void ChangeDirection()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Vector3 newDirection = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        direction = newDirection.normalized;

        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public void Initialise(Vector3 position, Vector3 direction, string word, float startedTime, float duration, float popTime, BubbleManager bubbleManager)
    {
        transform.position = position;
        this.direction = direction;
        this.word = word;
        this.bubbleManager = bubbleManager;
        this.duration = duration;
        this.startedTime = startedTime;
        this.popTime = popTime;

        textMesh.text = word;
    }
    
    public void BubblePop()
    {
        if (bubbleManager != null)
            OnPopBubble();
    }

    public void OnMouseUpAsButton()
    {
        if (Time.time >= popTime - duration)
        {
            BubblePop();
        }
        else
        {
            ChangeDirection();
        }
        
    }

    public void OnMouseDown()
    {
        // We clicked inside the threshold time.
        //if(Time.time >= popTime - duration)
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}

