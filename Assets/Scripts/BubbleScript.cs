using System;
using UnityEngine;
using UnityEngine.UI;
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
    private Text text;

    [SerializeField]
    private GameObject progressBar;

    [SerializeField]
    private GameObject progressQuad;

    [SerializeField]
    private Image cloud;

    private CapsuleCollider2D collider;

    private bool mouseDown = false;

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
        
        if (mouseDown)
        {
            float currentDur = popTime - Time.time;
            float progress = 1f - currentDur / duration;

            if (progress < 1f)
                SetProgress(progress); 
        }

        if (rigidbody.IsSleeping() && rigidbody.bodyType == RigidbodyType2D.Dynamic)
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

        text.text = word;

        collider = GetComponent<CapsuleCollider2D>();
        float scaleX = Math.Max(1, word.Length * 0.08f);
        cloud.transform.localScale = new Vector3(scaleX, 1, 1);
        //collider.size = new Vector2(collider.size.x * scaleX, collider.size.y);
    }
    
    public void BubblePop()
    {
        if (bubbleManager != null)
            OnPopBubble();
    }

    public void SetProgress(float progress)
    {
        float p  = Math.Min(1, Math.Max(0, progress));
        Vector3 target = new Vector3(-1.78f + p * 2 * 1.78f, progressQuad.transform.localPosition.y, progressQuad.transform.localPosition.z);
        progressQuad.transform.localPosition = target;
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
        mouseDown = true;
        rigidbody.bodyType = RigidbodyType2D.Static;
        mouseDownStartTime = Time.time;

        // We clicked inside the threshold time.
        if (Time.time >= popTime - duration)
        {
            progressBar.SetActive(true);
        }
    }

    public void OnMouseUp()
    {
        mouseDown = false;
    }
}

