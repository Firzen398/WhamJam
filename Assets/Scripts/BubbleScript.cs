using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private float intervalIncrease = 0.5f;

    private float maxX;
    private float maxY;

    private float popTime;
    private float duration;

    private float mouseDownStartTime;

    [SerializeField]
    private TextMesh textMesh;

    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private GameObject progressQuad;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>() == null ? gameObject.AddComponent<Rigidbody2D>() : GetComponent<Rigidbody2D>(); ;
        rigidbody.AddForce(direction*speed, ForceMode2D.Impulse);
        progressBar.SetActive(false);
    }

    void Update()
    {
        if (Time.time >=  popTime)
            BubblePop();


        if (mouseDownStartTime > 0)
        {
            float t = ( Time.timeSinceLevelLoad - mouseDownStartTime) * 3.0f;
            t = Math.Min(t, 1.78f * 2);
            Vector3 target = new Vector3(-1.78f + t, progressQuad.transform.localPosition.y, progressQuad.transform.localPosition.z);
            progressQuad.transform.localPosition = target;
        }

        if (rigidbody.IsSleeping())
            ChangeDirection();
    }

    private void OnPopBubble()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        bubbleManager.PopBubble(this);
        progressBar.SetActive(false);
    }

    private void ChangeDirection()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        Vector3 newDirection = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        direction = newDirection.normalized;

        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public void Initialise(Vector3 position, Vector3 direction, string word, float duration, float popTime, BubbleManager bubbleManager)
    {
        transform.position = position;
        this.direction = direction;
        this.word = word;
        this.bubbleManager = bubbleManager;
        this.duration = duration + intervalIncrease;
        this.popTime = popTime + intervalIncrease / 2f;

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
        rigidbody.bodyType = RigidbodyType2D.Static;
        mouseDownStartTime = Time.timeSinceLevelLoad;

        // We clicked inside the threshold time.
        if (Time.time >= popTime - duration)
        {
            progressBar.SetActive(true);
        }

    }
}

