using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    [SerializeField]
    private string word;

    private BubbleManager bubbleManager;

    private Rigidbody2D rigidbody;

    [SerializeField]
    private Vector3 direction;
    private float speed = 1f;

    private float maxX;
    private float maxY;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>() == null ? gameObject.AddComponent<Rigidbody2D>() : GetComponent<Rigidbody2D>(); ;
        rigidbody.AddForce(direction, ForceMode2D.Impulse);
    }

    void Update()
    {
      //  transform.position += direction * speed;        
    }

    public void Initialise(Vector3 position, Vector3 direction, string word, BubbleManager bubbleManager)
    {
        transform.position = position;
        this.direction = direction;
        this.word = word;
        this.bubbleManager = bubbleManager;
    }
    
    public void BubblePop()
    {
        bubbleManager.PopBubble(this);
    }
}
