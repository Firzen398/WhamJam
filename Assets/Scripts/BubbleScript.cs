using UnityEngine;

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

    private float activeTime;
    private float duration;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>() == null ? gameObject.AddComponent<Rigidbody2D>() : GetComponent<Rigidbody2D>(); ;
        rigidbody.AddForce(direction*speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        activeTime += Time.deltaTime;

        if (activeTime >= duration)
            BubblePop();
    }

    public void Initialise(Vector3 position, Vector3 direction, string word, float duration, BubbleManager bubbleManager)
    {
        transform.position = position;
        this.direction = direction;
        this.word = word;
        this.bubbleManager = bubbleManager;
        this.duration = duration;

        activeTime = 0f;
    }
    
    public void BubblePop()
    {
        bubbleManager.PopBubble(this);
    }
}
