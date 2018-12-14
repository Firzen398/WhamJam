using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    [SerializeField]
    private string word;

    private Vector3 direction;
    private float speed = 1f;

    private float maxX;
    private float maxY;

    void Update()
    {
        transform.position += direction * speed;        
    }

    public void Initialise(Vector3 position, Vector3 direction, string word)
    {
        transform.position = position;
        this.direction = direction;
        this.word = word;
    }
    
    public void BubblePop()
    {

    }
}
